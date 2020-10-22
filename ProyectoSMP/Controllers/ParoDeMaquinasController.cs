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
    public class ParoDeMaquinasController : Controller
    {
        private SMPEntities4 db = new SMPEntities4();

        // GET: ParoDeMaquinas
        public ActionResult Index()
        {
            var paroDeMaquina = db.ParoDeMaquina.Include(p => p.MantenimientoDeMaquina).Include(p => p.Maquina);
            return View(paroDeMaquina.ToList());
        }

        // GET: ParoDeMaquinas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParoDeMaquina paroDeMaquina = db.ParoDeMaquina.Find(id);
            if (paroDeMaquina == null)
            {
                return HttpNotFound();
            }
            return View(paroDeMaquina);
        }

        // GET: ParoDeMaquinas/Create
        public ActionResult Create()
        {
            ViewBag.IdMantenimiento = new SelectList(db.MantenimientoDeMaquina, "IdMantenimiento", "NumeroDeOrden");
            ViewBag.IdMaquina = new SelectList(db.Maquina, "IdMaquina", "NombreMaquina");
            return View();
        }

        // POST: ParoDeMaquinas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ParoDeMaquina paroDeMaquina)
        {
            if (ModelState.IsValid)
            {
                db.AgregarParoDeMaquina(paroDeMaquina.NombreParo,paroDeMaquina.Tipo,paroDeMaquina.Descripcion,paroDeMaquina.FechaComienza,
                    paroDeMaquina.FechaFin,paroDeMaquina.IdMaquina,paroDeMaquina.IdMantenimiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdMantenimiento = new SelectList(db.MantenimientoDeMaquina, "IdMantenimiento", "NumeroDeOrden", paroDeMaquina.IdMantenimiento);
            ViewBag.IdMaquina = new SelectList(db.Maquina, "IdMaquina", "NombreMaquina", paroDeMaquina.IdMaquina);
            return View(paroDeMaquina);
        }

        // GET: ParoDeMaquinas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParoDeMaquina paroDeMaquina = db.ParoDeMaquina.Find(id);
            if (paroDeMaquina == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMantenimiento = new SelectList(db.MantenimientoDeMaquina, "IdMantenimiento", "NumeroDeOrden", paroDeMaquina.IdMantenimiento);
            ViewBag.IdMaquina = new SelectList(db.Maquina, "IdMaquina", "NombreMaquina", paroDeMaquina.IdMaquina);
            return View(paroDeMaquina);
        }

        // POST: ParoDeMaquinas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdParo,NombreParo,Tipo,Descripcion,FechaComienza,FechaFin,IdMaquina,IdMantenimiento")] ParoDeMaquina paroDeMaquina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paroDeMaquina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMantenimiento = new SelectList(db.MantenimientoDeMaquina, "IdMantenimiento", "NumeroDeOrden", paroDeMaquina.IdMantenimiento);
            ViewBag.IdMaquina = new SelectList(db.Maquina, "IdMaquina", "NombreMaquina", paroDeMaquina.IdMaquina);
            return View(paroDeMaquina);
        }

        // GET: ParoDeMaquinas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParoDeMaquina paroDeMaquina = db.ParoDeMaquina.Find(id);
            if (paroDeMaquina == null)
            {
                return HttpNotFound();
            }
            return View(paroDeMaquina);
        }

        // POST: ParoDeMaquinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParoDeMaquina paroDeMaquina = db.ParoDeMaquina.Find(id);
            db.ParoDeMaquina.Remove(paroDeMaquina);
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