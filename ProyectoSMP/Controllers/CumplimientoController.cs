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
        private SMPEntities2 db = new SMPEntities2();
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
            ViewBag.IdMantenimiento = new SelectList(db.MantenimientoDeMaquina, "IdMantenimiento", "NombreDeMantenimiento");
            return View();
        }

        // POST: CumplimientoMantenimientoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CumplimientoMantenimiento cumplimientoMantenimiento)
        {
            if (ModelState.IsValid)
            {
                cumplimientoMantenimiento.Fecha = DateTime.Now;
                db.AgregarCumplimientoMantenimiento(cumplimientoMantenimiento.IdMantenimiento, cumplimientoMantenimiento.Estado,cumplimientoMantenimiento.Fecha);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMantenimiento = new SelectList(db.MantenimientoDeMaquina, "IdMantenimiento", "NombreDeMantenimiento",cumplimientoMantenimiento.IdMantenimiento);
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