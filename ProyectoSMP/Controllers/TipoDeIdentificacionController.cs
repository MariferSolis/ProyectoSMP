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
        public ActionResult Index()
        {
            return View(db.TipoDeIdentificacion.Where(x => x.Estado == true).ToList());
        }
        public ActionResult Todos()
        {
            return View(db.TipoDeIdentificacion.ToList());
        }
        // GET: TipoDeIdentificacions/Details/5
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
        public ActionResult Create()
        {
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
                return RedirectToAction("Index");
            }

            return View(tipoDeIdentificacion);
        }

        // GET: TipoDeIdentificacions/Edit/5
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
                return RedirectToAction("Index");
            }
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