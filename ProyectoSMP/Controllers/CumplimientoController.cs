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
    public class CumplimientoController : Controller
    {
        private SMPEntities db = new SMPEntities();
        // GET: CumplimientoMantenimientoes
        public ActionResult Index()
        {
            return View(db.CumplimientoMantenimiento.ToList());
        }

        // GET: CumplimientoMantenimientoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CumplimientoMantenimiento cumplimientoMantenimiento = db.CumplimientoMantenimiento.Find(id);
            if (cumplimientoMantenimiento == null)
            {
                return HttpNotFound();
            }
            return View(cumplimientoMantenimiento);
        }

        // GET: CumplimientoMantenimientoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CumplimientoMantenimientoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCumplimiento,IdMantenimiento,Estado,Fecha")] CumplimientoMantenimiento cumplimientoMantenimiento)
        {
            if (ModelState.IsValid)
            {
                db.CumplimientoMantenimiento.Add(cumplimientoMantenimiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cumplimientoMantenimiento);
        }

        // GET: CumplimientoMantenimientoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CumplimientoMantenimiento cumplimientoMantenimiento = db.CumplimientoMantenimiento.Find(id);
            if (cumplimientoMantenimiento == null)
            {
                return HttpNotFound();
            }
            return View(cumplimientoMantenimiento);
        }

        // POST: CumplimientoMantenimientoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCumplimiento,IdMantenimiento,Estado,Fecha")] CumplimientoMantenimiento cumplimientoMantenimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cumplimientoMantenimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cumplimientoMantenimiento);
        }

        // GET: CumplimientoMantenimientoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CumplimientoMantenimiento cumplimientoMantenimiento = db.CumplimientoMantenimiento.Find(id);
            if (cumplimientoMantenimiento == null)
            {
                return HttpNotFound();
            }
            return View(cumplimientoMantenimiento);
        }

        // POST: CumplimientoMantenimientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CumplimientoMantenimiento cumplimientoMantenimiento = db.CumplimientoMantenimiento.Find(id);
            db.CumplimientoMantenimiento.Remove(cumplimientoMantenimiento);
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
    }
}