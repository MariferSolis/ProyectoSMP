﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ProyectoSMP.Models;
using ProyectoSMP.Tool;
using Rotativa;

namespace ProyectoSMP.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private SMPEntities db = new SMPEntities();

        // GET: Usuarios
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Error = TempData["Message"].ToString();
            }
            var usuario = db.ConsultarUsuarios().Where(x => x.Estado == true).ToList();
            return View(usuario.ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Todos()
        {

            return View(db.Usuario.ToList());
        }
        public ActionResult Report()
        {

            return View(db.Usuario.ToList());
        }
        public ActionResult Print()
        {
            return new ActionAsPdf("Report")
            { FileName = "Test.pdf" };
        }
        // GET: Usuarios/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Usuario usuario = db.Usuario.Find(id);
            ConsultarUnUsuarios_Result consu = db.ConsultarUnUsuarios(id).FirstOrDefault();
            if (consu == null)
            {
                return HttpNotFound();
            }
            return View(consu);
        }


        // GET: Usuarios/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            if (TempData["MessageCorreo"] != null)
            {
                ViewBag.ErrorCorreo = TempData["MessageCorreo"].ToString();
            }
            ViewBag.IdRol = new SelectList(db.Rol, "IdRol", "Descripcion");
            ViewBag.IdTipoDeIdentificacion = new SelectList(db.TipoDeIdentificacion.Where(x => x.Estado == true).ToList(), "IdTipoIdentificacion", "Descripcion");
            ViewBag.ListaProvincias = CargaProvincias();
            ViewBag.ListaEstado = new SelectList(new[] {
                                   new SelectListItem { Value = "true", Text = "Activo" },
                                   new SelectListItem { Value = "false", Text = "Inactivo" }
                                                               }, "Value", "Text");
            return View();

        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                
                var dato = db.ExisteCorreo(usuario.Correo).FirstOrDefault();
                if(dato == null)
                {
                    var dia = DateTime.Today.ToString();
                    usuario.Password = CreatePassword(10);
                    var SecretKey = ConfigurationManager.AppSettings["SecretKey"];
                    var ClaveEncriptada = Seguridad.EncryptString(SecretKey, usuario.Password); 
                    db.AgregarUsuario(usuario.Identificacion, usuario.IdTipoDeIdentificacion, usuario.Nombre, usuario.Apellidos, usuario.Correo,
                    ClaveEncriptada, usuario.TipoCarga, usuario.Provincia, usuario.Canton, usuario.Distrito, usuario.IdRol, usuario.Estado,dia);
                    BtnCorreo(usuario.Correo,usuario.Password,usuario.Nombre,usuario.Apellidos);  
                    db.SaveChanges();
                    db.AgregarBitacora("Usuarios", "Crear", "El usuario realiza la acción de crear un tipo de usuario", Convert.ToInt32(Session["IdUsuario"]), DateTime.Now, "crear");
                    return RedirectToAction("Index");
                }
                else
                {
                    @TempData["MessageCorreo"]= "El correo ya existe debe de ingresar otro";
                    if (TempData["MessageCorreo"] != null)
                    {
                        ViewBag.ErrorCorreo = TempData["MessageCorreo"].ToString();
                    }
                    ViewBag.IdRol = new SelectList(db.Rol, "IdRol", "Descripcion", usuario.IdRol);
                    ViewBag.IdTipoDeIdentificacion = new SelectList(db.TipoDeIdentificacion.Where(x => x.Estado == true).ToList(), "IdTipoIdentificacion", "Descripcion", usuario.IdTipoDeIdentificacion);
                    ViewBag.ListaProvincias = CargaProvincias();
                    ViewBag.ListaEstado = new SelectList(new[] {
                                   new SelectListItem { Value = "true", Text = "Activo" },
                                   new SelectListItem { Value = "false", Text = "Inactivo" }
                                                               }, "Value", "Text");
                    return View(usuario);
                }
            }
            ViewBag.IdRol = new SelectList(db.Rol, "IdRol", "Descripcion", usuario.IdRol);
            ViewBag.IdTipoDeIdentificacion = new SelectList(db.TipoDeIdentificacion.Where(x => x.Estado == true).ToList(), "IdTipoIdentificacion", "Descripcion", usuario.IdTipoDeIdentificacion);
            ViewBag.ListaProvincias = CargaProvincias();
            ViewBag.ListaEstado = new SelectList(new[] {
                                   new SelectListItem { Value = "true", Text = "Activo" },
                                   new SelectListItem { Value = "false", Text = "Inactivo" }
                                                               }, "Value", "Text");
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            int user = Convert.ToInt32(Session["IdUsuario"]);
            if (usuario.IdUsuario == user)
            {
                @TempData["Message"] = "No puedes editar tu mismo usuario";
                if (TempData["Message"] != null)
                {
                    ViewBag.Error = TempData["Message"].ToString();
                }
                return RedirectToAction("Index");
            }
            else
            {
            ViewBag.IdRol = new SelectList(db.Rol, "IdRol", "Descripcion", usuario.IdRol);
            ViewBag.IdTipoDeIdentificacion = new SelectList(db.TipoDeIdentificacion.Where(x => x.Estado == true).ToList(), "IdTipoIdentificacion", "Descripcion", usuario.IdTipoDeIdentificacion);
            ViewBag.ListaProvincias = CargaProvincias();
            ViewBag.ListaCantones = CargaCanton(Convert.ToChar(usuario.Provincia));
            ViewBag.ListaDistritos = CargaDistrito(Convert.ToChar(usuario.Provincia), usuario.Canton);
            ViewBag.ListaEstado = new SelectList(new[] {
                                   new SelectListItem { Value = "true", Text = "Activo" },
                                   new SelectListItem { Value = "false", Text = "Inactivo" }
                                                               }, "Value", "Text",usuario.Estado);
            return View(usuario);
            }
            
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                int user = Convert.ToInt32(Session["IdUsuario"]);

                if (usuario.IdUsuario==user)
                {
                    @TempData["Message"] = "No puedes editar tu mismo usuario";
                    if (TempData["Message"] != null)
                    {
                        ViewBag.Error = TempData["Message"].ToString();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                db.AgregarBitacora("Usuarios", "Editar", "El usuario realiza la acción de editar un tipo de usuario", Convert.ToInt32(Session["IdUsuario"]), DateTime.Now, "editar");
                return RedirectToAction("Index");
                }
                
            }
            ViewBag.IdRol = new SelectList(db.Rol, "IdRol", "Descripcion", usuario.IdRol);
            ViewBag.IdTipoDeIdentificacion = new SelectList(db.TipoDeIdentificacion.Where(x => x.Estado == true).ToList(), "IdTipoIdentificacion", "Descripcion", usuario.IdTipoDeIdentificacion);
            ViewBag.ListaProvincias = CargaProvincias();
            ViewBag.ListaEstado = new SelectList(new[] {
                                   new SelectListItem { Value = "true", Text = "Activo" },
                                   new SelectListItem { Value = "false", Text = "Inactivo" }
                                                               }, "Value", "Text", usuario.Estado);
            return View(usuario);
        }
        public ActionResult Generar(int? id)
        {
            Usuario usuario = db.Usuario.Find(id);
            return View(usuario);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Generar(Usuario usuario)
        {
            try
            {
                var dia = DateTime.Today.ToString();
                usuario.Password = CreatePassword(10);
                var SecretKey = ConfigurationManager.AppSettings["SecretKey"];
                var ClaveEncriptada = Seguridad.EncryptString(SecretKey, usuario.Password);
                usuario.token_recovery = dia;
                usuario.Password = ClaveEncriptada;
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        protected void BtnCorreo(string correo, string contraseña, string nombre, string apellidos)
        {
            string body = "SMPrats \n" +
                " Hola, " + nombre + " " + apellidos + " \n" +
                "Para ingresar al sistema de mantenimiento de Prats sus datos son los siguientes: \n" +
                "Correo: " + correo + ".\n" +
                "Contraseña: " + contraseña + ".\n" +
                "Por su seguridad no mantenga esta información al acceso de terceras personas.\n" +
                "Tenga en cuenta que esta contraseña solo funcionara el dia de hoy, si el dia termina y no ha cambiado su contraseña" +
                " por favor contacte con un Administrador para que le brinde una nueva \n" +
                "Este mensaje es generado automáticamente, favor no responder a esta dirección de correo electrónico.";

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("sistemaprats@gmail.com", "proyecto.1");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;


            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("sistemaprats@gmail.com", "Sistema Prats");
            mail.To.Add(new MailAddress(correo));
            mail.Subject = "Bienvenido al Sistema de Mantenimiento Prats";
            mail.IsBodyHtml = true;
            mail.Body = body;

            smtp.Send(mail);

        }
        public string CreatePassword(int length)
        {
            string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        /// <summary>
        /// Obtiene Provincias
        /// </summary>
        /// <returns></returns>
        public List<Provincias_Result> CargaProvincias()
        {
            List<Provincias_Result> provincias = db.Provincias().ToList();
            return provincias;
        }
        /// <summary>
        /// Obtiene Cantones
        /// </summary>
        /// <param name="provincia"></param>
        /// <returns></returns>
        public List<Cantones_Result> CargaCanton(char provincia)
        {
            List<Cantones_Result> cantones = db.Cantones(Convert.ToString(provincia)).ToList();
            return cantones;
        }
        /// <summary>
        /// Obtiene Distritos
        /// </summary>
        /// <param name="provincia"></param>
        /// <param name="canton"></param>
        /// <returns></returns>
        public List<Distritos_Result> CargaDistrito(char provincia, string canton)
        {
            List<Distritos_Result> distritos = db.Distritos(Convert.ToString(provincia), canton).ToList();
            return distritos;
        }
        /// <summary>
        /// Cargar Cantones hacia la pantalla
        /// </summary>
        /// <param name="provincia"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CargaCantones(char provincia)
        {
            List<Cantones_Result> cantones = db.Cantones(Convert.ToString(provincia)).ToList();
            return Json(cantones, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Cargar Disttritos hacia la pantalla
        /// </summary>
        /// <param name="provincia"></param>
        /// <param name="canton"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CargaDistritos(char provincia, string canton)
        {
            List<Distritos_Result> distritos = db.Distritos(Convert.ToString(provincia), canton).ToList();
            return Json(distritos, JsonRequestBehavior.AllowGet);
        }
    }
}