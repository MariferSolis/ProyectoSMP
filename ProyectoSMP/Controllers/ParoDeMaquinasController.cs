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
    public class ParoDeMaquinasController : Controller
    {
        
        private SMPEntities db = new SMPEntities();

        // GET: ParoDeMaquinas
        [Authorize(Roles = "Admin,Tecnico,Operador")]
        public ActionResult Index()
        {
            var paroDeMaquina = db.ParoDeMaquina.Include(p => p.Mantenimiento).Include(p => p.Maquina);
            return View(paroDeMaquina.ToList());
        }

        // GET: ParoDeMaquinas/Details/5
        [Authorize(Roles = "Admin,Tecnico,Operador")]
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
        [Authorize(Roles = "Admin,Tecnico,Operador")]
        public ActionResult Create()
        {
            ViewBag.IdMantenimiento = new SelectList(db.Mantenimiento, "IdMantenimiento", "NombreOperacion");
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
                db.AgregarParoDeMaquina(paroDeMaquina.IdMaquina,paroDeMaquina.IdMantenimiento,
                    paroDeMaquina.Tipo,paroDeMaquina.Descripcion,paroDeMaquina.FechaComienza,paroDeMaquina.FechaFin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdMantenimiento = new SelectList(db.Mantenimiento, "IdMantenimiento", "NombreOperacion", paroDeMaquina.IdMantenimiento);
            ViewBag.IdMaquina = new SelectList(db.Maquina, "IdMaquina", "NombreMaquina", paroDeMaquina.IdMaquina);
            return View(paroDeMaquina);
        }

        // GET: ParoDeMaquinas/Edit/5
        [Authorize(Roles = "Admin,Tecnico,Operador")]
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
            ViewBag.IdMantenimiento = new SelectList(db.Mantenimiento, "IdMantenimiento", "NombreOperacion", paroDeMaquina.IdMantenimiento);
            ViewBag.IdMaquina = new SelectList(db.Maquina, "IdMaquina", "NombreMaquina", paroDeMaquina.IdMaquina);
            return View(paroDeMaquina);
        }

        // POST: ParoDeMaquinas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ParoDeMaquina paroDeMaquina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paroDeMaquina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMantenimiento = new SelectList(db.Mantenimiento, "IdMantenimiento", "NombreOperacion", paroDeMaquina.IdMantenimiento);
            ViewBag.IdMaquina = new SelectList(db.Maquina, "IdMaquina", "NombreMaquina", paroDeMaquina.IdMaquina);
            return View(paroDeMaquina);
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