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
        public ActionResult Report()
        {
            var paroDeMaquina = db.ParoDeMaquina.Include(p => p.Mantenimiento).Include(p => p.Maquina);
            return View(paroDeMaquina.ToList());
        }
        public ActionResult Print()
        {
            return new ActionAsPdf("Report")
            { FileName = "Test.pdf" };
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
            if (TempData["Message"] != null)
            {
                ViewBag.Error = TempData["Message"].ToString();
            }
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
                if (paroDeMaquina.FechaFin!=null)
                {
                    if(paroDeMaquina.FechaFin<paroDeMaquina.FechaComienza){

                        @TempData["Message"] = "Las fechas no coinciden";

                        return RedirectToAction("Create", paroDeMaquina);
                    }
                    else
                    {
                        db.AgregarParoDeMaquina(paroDeMaquina.IdMaquina, paroDeMaquina.IdMantenimiento,
                        paroDeMaquina.Tipo, paroDeMaquina.Descripcion, paroDeMaquina.FechaComienza, paroDeMaquina.FechaFin);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                }
                else
                {
                db.AgregarParoDeMaquina(paroDeMaquina.IdMaquina,paroDeMaquina.IdMantenimiento,
                paroDeMaquina.Tipo,paroDeMaquina.Descripcion,paroDeMaquina.FechaComienza,paroDeMaquina.FechaFin);
                db.SaveChanges();
                return RedirectToAction("Index");
                }
               
                
            }
            if (TempData["Message"] != null)
            {
                ViewBag.Error = TempData["Message"].ToString();
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
            if (TempData["Message"] != null)
            {
                ViewBag.Error = TempData["Message"].ToString();
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
                if (paroDeMaquina.FechaFin != null)
                {
                    if (paroDeMaquina.FechaFin < paroDeMaquina.FechaComienza)
                    {

                        @TempData["Message"] = "Las fechas no coinciden";

                        return RedirectToAction("Edit", paroDeMaquina);
                    }
                    else
                    {
                        db.Entry(paroDeMaquina).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                db.Entry(paroDeMaquina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (TempData["Message"] != null)
            {
                ViewBag.Error = TempData["Message"].ToString();
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