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
    public class TipoDeSistemaController : Controller
    {
        private SMPEntities14 db = new SMPEntities14();

        // GET: TipoDeSistemaDeMaquinas
        public ActionResult Index()
        {
            return View(db.TipoDeSistemaDeMaquina.ToList());
        }

        // GET: TipoDeSistemaDeMaquinas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDeSistemaDeMaquina tipoDeSistemaDeMaquina = db.TipoDeSistemaDeMaquina.Find(id);
            if (tipoDeSistemaDeMaquina == null)
            {
                return HttpNotFound();
            }
            return View(tipoDeSistemaDeMaquina);
        }

        // GET: TipoDeSistemaDeMaquinas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoDeSistemaDeMaquinas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoDeSistemaDeMaquina tipoDeSistemaDeMaquina)
        {
            if (ModelState.IsValid)
            {
                db.AgregarTipoDeSistemaDeMaquina(tipoDeSistemaDeMaquina.Nombre,tipoDeSistemaDeMaquina.Descripcion,tipoDeSistemaDeMaquina.Estado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoDeSistemaDeMaquina);
        }

        // GET: TipoDeSistemaDeMaquinas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDeSistemaDeMaquina tipoDeSistemaDeMaquina = db.TipoDeSistemaDeMaquina.Find(id);
            if (tipoDeSistemaDeMaquina == null)
            {
                return HttpNotFound();
            }
            return View(tipoDeSistemaDeMaquina);
        }

        // POST: TipoDeSistemaDeMaquinas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTipoSistema,Nombre,Descripcion,Estado")] TipoDeSistemaDeMaquina tipoDeSistemaDeMaquina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoDeSistemaDeMaquina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoDeSistemaDeMaquina);
        }

        // GET: TipoDeSistemaDeMaquinas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDeSistemaDeMaquina tipoDeSistemaDeMaquina = db.TipoDeSistemaDeMaquina.Find(id);
            if (tipoDeSistemaDeMaquina == null)
            {
                return HttpNotFound();
            }
            return View(tipoDeSistemaDeMaquina);
        }

        // POST: TipoDeSistemaDeMaquinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoDeSistemaDeMaquina tipoDeSistemaDeMaquina = db.TipoDeSistemaDeMaquina.Find(id);
            db.TipoDeSistemaDeMaquina.Remove(tipoDeSistemaDeMaquina);
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