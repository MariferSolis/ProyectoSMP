using ProyectoSMP.Models;
using Rotativa;
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
        private SMEntities db = new SMEntities();

        [Authorize(Roles = "Admin,Tecnico,Almacen")]
        // GET: InventarioDeRepuestos
        public ActionResult Index()
        {
            return View(db.InventarioDeRepuestos.ToList());
        }
        public ActionResult Comprar()
        {
            return View(db.RepuestosPorComprar().ToList());
        }
        public ActionResult Report()
        {
 
            return View(db.InventarioDeRepuestos.ToList());
        }
        public ActionResult Print()
        {
            return new ActionAsPdf("Report")
            { FileName = "Test.pdf" };
        }
        public ActionResult ReportC()
        {

            return View(db.RepuestosPorComprar().ToList());
        }
        public ActionResult PrintC()
        {
            return new ActionAsPdf("ReportC")
            { FileName = "Test.pdf" };
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
                if (inventarioDeRepuestos.Maximos < inventarioDeRepuestos.Minimos)
                {
                    @TempData["Message"] = "La cantidad de maximos y minimos no coinciden";
                    if (TempData["Message"] != null)
                    {
                        ViewBag.Error = TempData["Message"].ToString();
                    }
                    return View(inventarioDeRepuestos);
                }
                else
                {
                db.AgregarInventarioDeRepuestos(inventarioDeRepuestos.Nombre, inventarioDeRepuestos.Cantidad, inventarioDeRepuestos.Requisición, inventarioDeRepuestos.Maximos, inventarioDeRepuestos.Minimos, inventarioDeRepuestos.Tipo, inventarioDeRepuestos.Almacen);
                db.SaveChanges();
                db.AgregarBitacora("InventarioDeRepuestos", "Crear", "El usuario realiza la acción de crear un repuesto", Convert.ToInt32(Session["IdUsuario"]), DateTime.Now, "crear");
                return RedirectToAction("Index");
                }
                
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
                if (inventarioDeRepuestos.Maximos < inventarioDeRepuestos.Minimos)
                {
                    @TempData["Message"] = "La cantidad de maximos y minimos no coinciden";
                    if (TempData["Message"] != null)
                    {
                        ViewBag.Error = TempData["Message"].ToString();
                    }
                    return View(inventarioDeRepuestos);
                }
                else
                {
                    db.Entry(inventarioDeRepuestos).State = EntityState.Modified;
                    db.SaveChanges();
                    db.AgregarBitacora("InventarioDeRepuestos", "Editar", "El usuario realiza la acción de editar un repuesto", Convert.ToInt32(Session["IdUsuario"]), DateTime.Now, "editar");
                    return RedirectToAction("Index");
                }
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