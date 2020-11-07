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
    public class AreaDeMaquinasController : Controller
    {
        private SMPEntities14 db = new SMPEntities14();
        // GET: AreaDeMaquinas
        public ActionResult Index()
        {
            return View(db.AreaDeMaquina.ToList());
        }

        // GET: AreaDeMaquinas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaDeMaquina areaDeMaquina = db.AreaDeMaquina.Find(id);
            if (areaDeMaquina == null)
            {
                return HttpNotFound();
            }
            return View(areaDeMaquina);
        }

        // GET: AreaDeMaquinas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AreaDeMaquinas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdArea,Nombre,Descripcion,Estado")] AreaDeMaquina areaDeMaquina)
        {
            if (ModelState.IsValid)
            {

                db.AgregarAreaDeMaquina(areaDeMaquina.Nombre, areaDeMaquina.Descripcion, areaDeMaquina.Estado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(areaDeMaquina);
        }

        // GET: AreaDeMaquinas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaDeMaquina areaDeMaquina = db.AreaDeMaquina.Find(id);
            if (areaDeMaquina == null)
            {
                return HttpNotFound();
            }
            return View(areaDeMaquina);
        }

        // POST: AreaDeMaquinas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdArea,Nombre,Descripcion,Estado")] AreaDeMaquina areaDeMaquina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(areaDeMaquina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(areaDeMaquina);
        }

        // GET: AreaDeMaquinas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaDeMaquina areaDeMaquina = db.AreaDeMaquina.Find(id);
            if (areaDeMaquina == null)
            {
                return HttpNotFound();
            }
            return View(areaDeMaquina);
        }

        // POST: AreaDeMaquinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AreaDeMaquina areaDeMaquina = db.AreaDeMaquina.Find(id);
            db.AreaDeMaquina.Remove(areaDeMaquina);
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