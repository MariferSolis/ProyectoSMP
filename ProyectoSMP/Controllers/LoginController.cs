using ProyectoSMP.Models;
using ProyectoSMP.Models.ViewModels;
using ProyectoSMP.Tool;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProyectoSMP.Controllers
{
    public class LoginController : Controller
    {

        SMEntities bd = new SMEntities();

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(ExisteUsuario_Result existe)
        {
            var SecretKey = ConfigurationManager.AppSettings["SecretKey"];

            var ClaveEncriptada = Seguridad.EncryptString(SecretKey, existe.Password);
            //var ClaveDesencriptada = Seguridad.DecryptString(SecretKey, ClaveEncriptada);
            var dato = bd.ExisteUsuario(existe.Correo, ClaveEncriptada).FirstOrDefault();
            if (dato == null)
            {
                ViewBag.Error = "Usuario o contraseña invalidos";
                return View(existe);

            }
            else
            {
                if (dato.token_recovery != null)
                {
                    if (dato.token_recovery != DateTime.Today.ToString())
                    {
                        ViewBag.Error = "Debe de cambiar la contraseña diríjase a olvido de contraseña y proceda el cambio";
                        return View(existe);

                    }
                    else
                    {
                        if (dato.Estado==true)
                        {
                        Session["IdUsuario"] = dato.IdUsuario.ToString();
                        System.Web.HttpContext.Current.Session["Name"] = dato.Nombre.ToString() + " " + dato.Apellidos.ToString();
                        var username = existe.Correo;
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMinutes(30), Convert.ToBoolean(existe.Recordarme), FormsAuthentication.FormsCookiePath);
                        string hash = FormsAuthentication.Encrypt(ticket);
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                        Response.Cookies.Add(cookie);
                        bd.AgregarBitacora("Login", "Login", "El usuario realiza la acción de un login", Convert.ToInt32(Session["IdUsuario"]), DateTime.Now, "Login");
                        return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.Error = "Usuario inactivo";
                            return View(existe);
                        }
                        
                    }
                }
                else
                {
                    if (dato.Estado == true)
                    {
                        Session["IdUsuario"] = dato.IdUsuario.ToString();
                    System.Web.HttpContext.Current.Session["Name"] = dato.Nombre.ToString() + " " + dato.Apellidos.ToString();
                    var username = existe.Correo;
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMinutes(30), Convert.ToBoolean(existe.Recordarme), FormsAuthentication.FormsCookiePath);
                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                    Response.Cookies.Add(cookie);
                    bd.AgregarBitacora("Login", "Login", "El usuario realiza la acción de un login", Convert.ToInt32(Session["IdUsuario"]), DateTime.Now, "Login");
                    return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Error = "Usuario inactivo";
                        return View(existe);
                    }
                }
            }            
        }
        public ActionResult Reenviar()
        {


            return View();
        }
        public ActionResult Salir()
        {
            Session.Remove("IdUsuario");
            Session.Remove("Name");
            Session.RemoveAll();
            Response.Cache.SetCacheability(HttpCacheability.Private);
            Session.Clear();
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Cache.SetNoServerCaching();
            Request.Cookies.Clear();
            return RedirectToAction("Login", "Login");
        }
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult StartRecovery()
        {
            
            RecoveryViewModel model = new RecoveryViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult StartRecovery(RecoveryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                string token = GetSha256(Guid.NewGuid().ToString());

                using (bd)
                {
                    var oUser = bd.Usuario.Where(d => d.Correo == model.Email).FirstOrDefault();

                    if (oUser != null)
                    {

                        oUser.token_recovery = token;
                        bd.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        bd.SaveChanges();
                        //enviar mail
                        SendEmail(oUser.Correo, token);
                        ViewBag.Message1 = "Revise su correo";
                    }
                    else
                    {
                        ViewBag.Message = "El Correo no existe";
                        return View(model);
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult Recovery(string token)
        {
            ProyectoSMP.Models.ViewModels.RecoveryPasswordViewModel model = new ProyectoSMP.Models.ViewModels.RecoveryPasswordViewModel();
            model.token = token;
            using (bd)
            {
                if (model.token == null || model.token.Trim().Equals(""))
                {
                    return View("Login");
                }
                var oUser = bd.Usuario.Where(d => d.token_recovery == model.token).FirstOrDefault();
                if (oUser == null)
                {
                    ViewBag.Error = "Tu token ha expirado";
                    return View("Login");

                }
            }


            return View(model);
        }
        [HttpPost]
        public ActionResult Recovery(ProyectoSMP.Models.ViewModels.RecoveryPasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                using (bd)
                {
                    var oUser = bd.Usuario.Where(d => d.token_recovery == model.token).FirstOrDefault();
                    var SecretKey = ConfigurationManager.AppSettings["SecretKey"];

                    var ClaveEncriptada = Seguridad.EncryptString(SecretKey, oUser.Password);
                    var modelClave = Seguridad.EncryptString(SecretKey, model.Password);

                    if (oUser != null)
                    {
                        ClaveEncriptada = modelClave;
                        oUser.token_recovery = null;
                        oUser.Password = ClaveEncriptada;
                        bd.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        bd.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            ViewBag.Message = "Contraseña modificada con éxito";
            return View("Login");
        }
        string urlDomain = "http://smprats.azurewebsites.net/";
        #region HELPERS
        private string GetSha256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        private void SendEmail(string EmailDestino, string token)
        {
            string EmailOrigen = "sistemaprats@gmail.com";
            string Contraseña = "proyecto.1";
            string url = urlDomain + "/Login/Recovery/?token=" + token;
            MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Recuperación de contraseña",
                "<p>Correo para recuperación de contraseña</p><br>" +
                "<a href='" + url + "'>Click para recuperar</a>");

            oMailMessage.IsBodyHtml = true;

            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;
            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contraseña);

            oSmtpClient.Send(oMailMessage);

            oSmtpClient.Dispose();
        }

        #endregion
    }
}