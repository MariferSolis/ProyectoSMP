﻿using ProyectoSMP.Models;
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
    public class TipoDeSistemaController : Controller
    {
        
        private SMPEntities db = new SMPEntities();

        // GET: TipoDeSistemaDeMaquinas
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var tipoDeSistema = db.TipoDeSistemaDeMaquina.Where(x => x.Estado == true).ToList();
            return View(tipoDeSistema);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Todos()
        {

            return View(db.TipoDeSistemaDeMaquina.ToList());
        }

        // GET: TipoDeSistemaDeMaquinas/Details/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        public ActionResult Edit(TipoDeSistemaDeMaquina tipoDeSistemaDeMaquina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoDeSistemaDeMaquina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoDeSistemaDeMaquina);
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