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
        private SMPEntities14 db = new SMPEntities14();
        // GET: CumplimientoMantenimientoes
        public ActionResult Index()
        {
            return View(db.Cumplimiento.ToList());
        }

        // GET: CumplimientoMantenimientoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cumplimiento cumplimiento = db.Cumplimiento.Find(id);
            if (cumplimiento == null)
            {
                return HttpNotFound();
            }
            return View(cumplimiento);
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
        public ActionResult Create(Cumplimiento cumplimiento)
        {
            if (ModelState.IsValid)
            {
                cumplimiento.Fecha = DateTime.Now;
                db.AgregarCumplimientoMantenimiento(cumplimiento.IdPlan, cumplimiento.Fecha,cumplimiento.Estado,cumplimiento.Detalles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cumplimiento);
        }

        // GET: CumplimientoMantenimientoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cumplimiento cumplimientoMantenimiento = db.Cumplimiento.Find(id);
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
            Cumplimiento cumplimiento = db.Cumplimiento.Find(id);
            db.Cumplimiento.Remove(cumplimiento);
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