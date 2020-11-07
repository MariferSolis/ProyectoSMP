using ProyectoSMP.Models;
using ProyectoSMP.Tool;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProyectoSMP.Controllers
{
    public class LoginController : Controller
    {

        SMPEntities14 bd = new SMPEntities14();

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
                Session["IdUsuario"] = dato.IdUsuario.ToString();
                System.Web.HttpContext.Current.Session["Name"] = dato.Nombre.ToString()+" "+dato.Apellidos.ToString();
                var username = existe.Correo;
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMinutes(30), Convert.ToBoolean(existe.Recordarme), FormsAuthentication.FormsCookiePath);
                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                Response.Cookies.Add(cookie);

                return RedirectToAction("Index", "Home");
            }

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
    }
}