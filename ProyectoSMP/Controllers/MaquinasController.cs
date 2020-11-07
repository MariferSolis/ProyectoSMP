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
    public class MaquinasController : Controller
    {     
        private SMPEntities14 db = new SMPEntities14();

        // GET: Maquinas
        public ActionResult Index()
        {
            var maquina = db.Maquina.Include(m => m.AreaDeMaquina).Include(m => m.TipoDeSistemaDeMaquina);
            return View(maquina.ToList());
        }

        // GET: Maquinas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maquina maquina = db.Maquina.Find(id);
            if (maquina == null)
            {
                return HttpNotFound();
            }
            return View(maquina);
        }

        // GET: Maquinas/Create
        public ActionResult Create()
        {
            ViewBag.IdArea = new SelectList(db.AreaDeMaquina, "IdArea", "Nombre");
            ViewBag.IdTipoSistema = new SelectList(db.TipoDeSistemaDeMaquina, "IdTipoSistema", "Nombre");
            return View();
        }

        // POST: Maquinas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Maquina maquina)
        {
            if (ModelState.IsValid)
            {
                db.AgregarMaquina(maquina.NombreMaquina,maquina.IdTipoSistema,maquina.IdArea,maquina.Codigo,maquina.Modelo,maquina.Proceso,maquina.Cadencia,maquina.Descripcion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdArea = new SelectList(db.AreaDeMaquina, "IdArea", "Nombre", maquina.IdArea);
            ViewBag.IdTipoSistema = new SelectList(db.TipoDeSistemaDeMaquina, "IdTipoSistema", "Nombre", maquina.IdTipoSistema);
            return View(maquina);
        }

        // GET: Maquinas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maquina maquina = db.Maquina.Find(id);
            if (maquina == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdArea = new SelectList(db.AreaDeMaquina, "IdArea", "Nombre", maquina.IdArea);
            ViewBag.IdTipDeSistema = new SelectList(db.TipoDeSistemaDeMaquina, "IdTipoSistema", "Nombre", maquina.IdTipoSistema);
            return View(maquina);
        }

        // POST: Maquinas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMaquina,NombreMaquina,IdTipDeSistema,IdArea,Codigo,Modelo,Proceso,Cadencia,Descripcion")] Maquina maquina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maquina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdArea = new SelectList(db.AreaDeMaquina, "IdArea", "Nombre", maquina.IdArea);
            ViewBag.IdTipDeSistema = new SelectList(db.TipoDeSistemaDeMaquina, "IdTipoSistema", "Nombre", maquina.IdTipoSistema);
            return View(maquina);
        }

        // GET: Maquinas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maquina maquina = db.Maquina.Find(id);
            if (maquina == null)
            {
                return HttpNotFound();
            }
            return View(maquina);
        }

        // POST: Maquinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Maquina maquina = db.Maquina.Find(id);
            db.Maquina.Remove(maquina);
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