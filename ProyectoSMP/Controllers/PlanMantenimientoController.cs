using ProyectoSMP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSMP.Controllers
{
    public class PlanMantenimientoController : Controller
    {
        private SMPEntities14 db = new SMPEntities14();
        // GET: PlanMantenimiento
        public ActionResult Index()
        {
            return View(db.PlanMantenimiento.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanMantenimiento planMantenimiento = db.PlanMantenimiento.Find(id);
            if (planMantenimiento == null)
            {
                return HttpNotFound();
            }
            return View(planMantenimiento);
        }
        public ActionResult Create()
        {
            ViewBag.IdMantenimiento = new SelectList(db.Mantenimiento, "IdMantenimiento", "NombreOperacion");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlanMantenimiento planMantenimiento)
        {
            if (ModelState.IsValid)
            {
                db.AgregarPlanMantenimiento(planMantenimiento.IdMantenimiento,planMantenimiento.Duracion,planMantenimiento.FechaDeInicio,planMantenimiento.FechaDeCreacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdMantenimiento = new SelectList(db.Mantenimiento, "IdMantenimiento", "NombreOperacion", planMantenimiento.IdMantenimiento);
            return View(planMantenimiento);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanMantenimiento planMantenimiento = db.PlanMantenimiento.Find(id);
            if (planMantenimiento == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMantenimiento = new SelectList(db.Mantenimiento, "IdMantenimiento", "NombreOperacion", planMantenimiento.IdMantenimiento);
            return View(planMantenimiento);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlanMantenimiento planMantenimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planMantenimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMantenimiento = new SelectList(db.Mantenimiento, "IdMantenimiento", "NombreOperacion", planMantenimiento.IdMantenimiento);
 
            return View(planMantenimiento);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanMantenimiento planMantenimiento = db.PlanMantenimiento.Find(id);
            if (planMantenimiento == null)
            {
                return HttpNotFound();
            }
            return View(planMantenimiento);
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