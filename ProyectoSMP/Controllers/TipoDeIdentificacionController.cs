using ProyectoSMP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSMP.Controllers
{
    [Authorize]
    public class TipoDeIdentificacionController : Controller
    {
        private SMPEntities db = new SMPEntities();

        // GET: TipoDeIdentificacions
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.TipoDeIdentificacion.Where(x => x.Estado == true).ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Todos()
        {
            return View(db.TipoDeIdentificacion.ToList());
        }
        // GET: TipoDeIdentificacions/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDeIdentificacion tipoDeIdentificacion = db.TipoDeIdentificacion.Find(id);
            if (tipoDeIdentificacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoDeIdentificacion);
        }

        // GET: TipoDeIdentificacions/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.ListaEstado = new SelectList(new[] {
                                   new SelectListItem { Value = "true", Text = "Activo" },
                                   new SelectListItem { Value = "false", Text = "Inactivo" }
                                                               }, "Value", "Text");
            return View();
        }

        // POST: TipoDeIdentificacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTipoIdentificacion,Descripcion,Estado")] TipoDeIdentificacion tipoDeIdentificacion)
        {
            if (ModelState.IsValid)
            {
                db.AgregarTipoDeIdentificacion(tipoDeIdentificacion.Descripcion,tipoDeIdentificacion.Estado);
                db.SaveChanges();
                db.AgregarBitacora("TipoDeIdentificacion", "Crear", "El usuario realiza la acción de crear un tipo de identificación", Convert.ToInt32(Session["IdUsuario"]), DateTime.Now, "crear");
                return RedirectToAction("Index");
            }
            ViewBag.ListaEstado = new SelectList(new[] {
                                   new SelectListItem { Value = "true", Text = "Activo" },
                                   new SelectListItem { Value = "false", Text = "Inactivo" }
                                                               }, "Value", "Text");
            return View(tipoDeIdentificacion);
        }

        // GET: TipoDeIdentificacions/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDeIdentificacion tipoDeIdentificacion = db.TipoDeIdentificacion.Find(id);
            if (tipoDeIdentificacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListaEstado = new SelectList(new[] {
                                   new SelectListItem { Value = "true", Text = "Activo" },
                                   new SelectListItem { Value = "false", Text = "Inactivo" }
                                                               }, "Value", "Text", tipoDeIdentificacion.Estado);
            return View(tipoDeIdentificacion);
        }

        // POST: TipoDeIdentificacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTipoIdentificacion,Descripcion,Estado")] TipoDeIdentificacion tipoDeIdentificacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoDeIdentificacion).State = EntityState.Modified;
                db.SaveChanges();
                db.AgregarBitacora("TipoDeIdentificacion", "Editar", "El usuario realiza la acción de editar un tipo de identificación", Convert.ToInt32(Session["IdUsuario"]), DateTime.Now, "editar");
                return RedirectToAction("Index");
            }
            ViewBag.ListaEstado = new SelectList(new[] {
                                   new SelectListItem { Value = "true", Text = "Activo" },
                                   new SelectListItem { Value = "false", Text = "Inactivo" }
                                                               }, "Value", "Text", tipoDeIdentificacion.Estado);
            return View(tipoDeIdentificacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}