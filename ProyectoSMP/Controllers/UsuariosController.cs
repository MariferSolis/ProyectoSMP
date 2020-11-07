using System;
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

namespace ProyectoSMP.Controllers
{
    public class UsuariosController : Controller
    {
        private SMPEntities14 db = new SMPEntities14();

        // GET: Usuarios
        public ActionResult Index()
        {

            var usuario = db.ConsultarUsuarios();
            return View(usuario.ToList());
        }
        public ActionResult Todos()
        {

            return View(db.Usuario.ToList());
        }

        // GET: Usuarios/Details/5
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
        public ActionResult Create()
        {
            ViewBag.IdRol = new SelectList(db.Rol, "IdRol", "Descripcion");
            ViewBag.IdTipoDeIdentificacion = new SelectList(db.TipoDeIdentificacion, "IdTipoIdentificacion", "Descripcion");
            ViewBag.ListaProvincias = CargaProvincias();
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
                usuario.Password = CreatePassword(10);
                var SecretKey = ConfigurationManager.AppSettings["SecretKey"];
                var ClaveEncriptada = Seguridad.EncryptString(SecretKey, usuario.Password);
                usuario.Password = ClaveEncriptada;
                var dato = db.ExisteCorreo(usuario.Correo).FirstOrDefault();
                if(dato == null)
                {
                    db.AgregarUsuario(usuario.Identificacion, usuario.IdTipoDeIdentificacion, usuario.Nombre, usuario.Apellidos, usuario.Correo,
                    usuario.Password, usuario.TipoCarga, usuario.Provincia, usuario.Canton, usuario.Distrito, usuario.IdRol, usuario.Estado);
                    db.SaveChanges();
                   
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "El correo ya existe");
                    return RedirectToAction("Create", usuario);
                }
            }

            ViewBag.IdRol = new SelectList(db.Rol, "IdRol", "Descripcion", usuario.IdRol);
            ViewBag.IdTipoDeIdentificacion = new SelectList(db.TipoDeIdentificacion, "IdTipoIdentificacion", "Descripcion", usuario.IdTipoDeIdentificacion);
            ViewBag.ListaProvincias = CargaProvincias();
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
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
            ViewBag.IdRol = new SelectList(db.Rol, "IdRol", "Descripcion", usuario.IdRol);
            ViewBag.IdTipoDeIdentificacion = new SelectList(db.TipoDeIdentificacion, "IdTipoIdentificacion", "Descripcion", usuario.IdTipoDeIdentificacion);
            ViewBag.ListaProvincias = CargaProvincias();
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUsuario,Identificacion,IdTipoDeIdentificacion,Nombre,Apellidos,Correo,Password,TipoCarga,Provincia,Canton,Distrito,IdRol,Estado")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdRol = new SelectList(db.Rol, "IdRol", "Descripcion", usuario.IdRol);
            ViewBag.IdTipoDeIdentificacion = new SelectList(db.TipoDeIdentificacion, "IdTipoIdentificacion", "Descripcion", usuario.IdTipoDeIdentificacion);
            ViewBag.ListaProvincias = CargaProvincias();
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
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
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
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