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
    public class CumplimientoController : Controller
    {
        private SMPEntities db = new SMPEntities();

        // GET: CumplimientoMantenimientoes
        public ActionResult Index()
        {
            var cumpli = db.Cumplimiento.Include(m => m.Mantenimiento).ToList();  
            return View(cumpli);
        }
        public ActionResult Index2()
        {
            var usuario = Session["IdUsuario"];

            var plan = db.ConsultarCumplixUsuario(Convert.ToInt32(usuario)).ToList();
            return View(plan);
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
        public ActionResult Realizar(int? id)
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
            ViewBag.IdMantenimiento = new SelectList(db.Mantenimiento, "IdMantenimiento", "NombreOperacion", cumplimiento.IdMantenimiento);
            return View(cumplimiento);
        }

        // POST: Cumplimiento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Realizar(Cumplimiento cumplimiento)
        {
            if (ModelState.IsValid)
            {
                cumplimiento.Fecha = DateTime.Now;
                db.Entry(cumplimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index2");
            }
            ViewBag.IdMantenimiento = new SelectList(db.Mantenimiento, "IdMantenimiento", "NombreOperacion", cumplimiento.IdMantenimiento);
            return View(cumplimiento);
        }
        // GET: CumplimientoMantenimientoes/Create
        public ActionResult Create()
        {
            ViewBag.IdMantenimiento = new SelectList(db.Mantenimiento, "IdMantenimiento", "NombreOperacion");
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

                db.AgregarCumplimiento(cumplimiento.IdMantenimiento,cumplimiento.Comienza,cumplimiento.Finaliza,cumplimiento.Color);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMantenimiento = new SelectList(db.Mantenimiento, "IdMantenimiento", "NombreOperacion", cumplimiento.IdMantenimiento);
            return View(cumplimiento);
        }
        public ActionResult Edit(int? id)
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
            ViewBag.IdMantenimiento = new SelectList(db.Mantenimiento, "IdMantenimiento", "NombreOperacion", cumplimiento.IdMantenimiento);
            return View(cumplimiento);
        }

        // POST: Cumplimiento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cumplimiento cumplimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cumplimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMantenimiento = new SelectList(db.Mantenimiento, "IdMantenimiento", "NombreOperacion", cumplimiento.IdMantenimiento);
            return View(cumplimiento);
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