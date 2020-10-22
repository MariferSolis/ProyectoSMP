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
    public class MantenimientoDeMaquinasController : Controller
    {
        // GET: MantenimientoDeMaquinas
        private SMPEntities4 db = new SMPEntities4();

        // GET: MantenimientoDeMaquinas
        public ActionResult Index()
        {
            var mantenimientoDeMaquina = db.MantenimientoDeMaquina.Include(m => m.InventarioDeRepuestos).Include(m => m.Maquina).Include(m => m.Rol1);
            return View(mantenimientoDeMaquina.ToList());
        }

        // GET: MantenimientoDeMaquinas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MantenimientoDeMaquina mantenimientoDeMaquina = db.MantenimientoDeMaquina.Find(id);
            if (mantenimientoDeMaquina == null)
            {
                return HttpNotFound();
            }
            return View(mantenimientoDeMaquina);
        }

        // GET: MantenimientoDeMaquinas/Create
        public ActionResult Create()
        {
            ViewBag.IdRepuesto = new SelectList(db.InventarioDeRepuestos, "IdRepuesto", "Nombre");
            ViewBag.IdMaquina = new SelectList(db.Maquina, "IdMaquina", "NombreMaquina");
            ViewBag.Rol = new SelectList(db.Rol, "IdRol", "Descripcion");
            return View();
        }

        // POST: MantenimientoDeMaquinas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MantenimientoDeMaquina mantenimientoDeMaquina)
        {
            if (ModelState.IsValid)
            {
                db.AgregarMantenimientoDeMaquina(mantenimientoDeMaquina.NumeroDeOrden, mantenimientoDeMaquina.NombreDeMantenimiento, 
                    mantenimientoDeMaquina.Tipo, mantenimientoDeMaquina.Fecuencia,mantenimientoDeMaquina.Rol,mantenimientoDeMaquina.IdUsuario,
                    mantenimientoDeMaquina.IdMaquina,mantenimientoDeMaquina.IdRepuesto, mantenimientoDeMaquina.URLArchivo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdRepuesto = new SelectList(db.InventarioDeRepuestos, "IdRepuesto", "Nombre", mantenimientoDeMaquina.IdRepuesto);
            ViewBag.IdMaquina = new SelectList(db.Maquina, "IdMaquina", "NombreMaquina", mantenimientoDeMaquina.IdMaquina);
            ViewBag.Rol = new SelectList(db.Rol, "IdRol", "Descripcion", mantenimientoDeMaquina.Rol);
            return View(mantenimientoDeMaquina);
        }

        // GET: MantenimientoDeMaquinas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MantenimientoDeMaquina mantenimientoDeMaquina = db.MantenimientoDeMaquina.Find(id);
            if (mantenimientoDeMaquina == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRepuesto = new SelectList(db.InventarioDeRepuestos, "IdRepuesto", "Nombre", mantenimientoDeMaquina.IdRepuesto);
            ViewBag.IdMaquina = new SelectList(db.Maquina, "IdMaquina", "NombreMaquina", mantenimientoDeMaquina.IdMaquina);
            ViewBag.Rol = new SelectList(db.Rol, "IdRol", "Descripcion", mantenimientoDeMaquina.Rol);
            return View(mantenimientoDeMaquina);
        }

        // POST: MantenimientoDeMaquinas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMantenimiento,NumeroDeOrden,NumeroDeMantenimiento,Tipo,Fecuencia,Rol,IdUsuario,IdMaquina,IdRepuesto,URLArchivo")] MantenimientoDeMaquina mantenimientoDeMaquina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mantenimientoDeMaquina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdRepuesto = new SelectList(db.InventarioDeRepuestos, "IdRepuesto", "Nombre", mantenimientoDeMaquina.IdRepuesto);
            ViewBag.IdMaquina = new SelectList(db.Maquina, "IdMaquina", "NombreMaquina", mantenimientoDeMaquina.IdMaquina);
            ViewBag.Rol = new SelectList(db.Rol, "IdRol", "Descripcion", mantenimientoDeMaquina.Rol);
            return View(mantenimientoDeMaquina);
        }

        // GET: MantenimientoDeMaquinas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MantenimientoDeMaquina mantenimientoDeMaquina = db.MantenimientoDeMaquina.Find(id);
            if (mantenimientoDeMaquina == null)
            {
                return HttpNotFound();
            }
            return View(mantenimientoDeMaquina);
        }

        // POST: MantenimientoDeMaquinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MantenimientoDeMaquina mantenimientoDeMaquina = db.MantenimientoDeMaquina.Find(id);
            db.MantenimientoDeMaquina.Remove(mantenimientoDeMaquina);
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