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
    public class InventarioDeRepuestosController : Controller
    {
        private SMPEntities db = new SMPEntities();

        [Authorize(Roles = "Admin,Tecnico,Almacen")]
        // GET: InventarioDeRepuestos
        public ActionResult Index()
        {
            return View(db.InventarioDeRepuestos.ToList());
        }
        [Authorize(Roles = "Admin,Tecnico,Almacen")]
        // GET: InventarioDeRepuestos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventarioDeRepuestos inventarioDeRepuestos = db.InventarioDeRepuestos.Find(id);
            if (inventarioDeRepuestos == null)
            {
                return HttpNotFound();
            }
            return View(inventarioDeRepuestos);
        }
        [Authorize(Roles = "Admin,Tecnico,Almacen")]
        // GET: InventarioDeRepuestos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InventarioDeRepuestos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InventarioDeRepuestos inventarioDeRepuestos)
        {
            if (ModelState.IsValid)
            {
                db.AgregarInventarioDeRepuestos(inventarioDeRepuestos.Nombre, inventarioDeRepuestos.Cantidad, inventarioDeRepuestos.Requisición, inventarioDeRepuestos.Maximos, inventarioDeRepuestos.Minimos, inventarioDeRepuestos.Tipo, inventarioDeRepuestos.Almacen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inventarioDeRepuestos);
        }
        [Authorize(Roles = "Admin,Tecnico,Almacen")]
        // GET: InventarioDeRepuestos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventarioDeRepuestos inventarioDeRepuestos = db.InventarioDeRepuestos.Find(id);
            if (inventarioDeRepuestos == null)
            {
                return HttpNotFound();
            }
            return View(inventarioDeRepuestos);
        }

        // POST: InventarioDeRepuestos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRepuesto,Nombre,Cantidad,Requisición,Maximos,Minimos,Tipo,Almacen")] InventarioDeRepuestos inventarioDeRepuestos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventarioDeRepuestos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inventarioDeRepuestos);
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