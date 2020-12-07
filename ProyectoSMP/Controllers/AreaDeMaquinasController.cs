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
    [Authorize]
    public class AreaDeMaquinasController : Controller
    {
        private SMPEntities db = new SMPEntities();

        // GET: AreaDeMaquinas
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.AreaDeMaquina.ToList().Where(x => x.Estado == true).ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Todos()
        {
            return View(db.AreaDeMaquina.ToList());
        }

        // GET: AreaDeMaquinas/Details/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.ListaEstado = new SelectList(new[] {
                                   new SelectListItem { Value = "true", Text = "Activo" },
                                   new SelectListItem { Value = "false", Text = "Inactivo" }
                                                               }, "Value", "Text");
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
                db.AgregarBitacora("AreaDeMaquinas","Crear","El usuario realiza la acción de crear un área",Convert.ToInt32(Session["IdUsuario"]),DateTime.Now, "crear");
                return RedirectToAction("Index");
            }
            ViewBag.ListaEstado = new SelectList(new[] {
                                   new SelectListItem { Value = "true", Text = "Activo" },
                                   new SelectListItem { Value = "false", Text = "Inactivo" }
                                                               }, "Value", "Text");
            return View(areaDeMaquina);
        }

        // GET: AreaDeMaquinas/Edit/5
        [Authorize(Roles = "Admin")]
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
            ViewBag.ListaEstado = new SelectList(new[] {
                                   new SelectListItem { Value = "true", Text = "Activo" },
                                   new SelectListItem { Value = "false", Text = "Inactivo" }
                                                               }, "Value", "Text", areaDeMaquina.Estado);
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
                db.AgregarBitacora("AreaDeMaquinas", "Editar", "El usuario realiza la acción de editar un área", Convert.ToInt32(Session["IdUsuario"]), DateTime.Now, "editar");
                return RedirectToAction("Index");
            }
            ViewBag.ListaEstado = new SelectList(new[] {
                                   new SelectListItem { Value = "true", Text = "Activo" },
                                   new SelectListItem { Value = "false", Text = "Inactivo" }
                                                               }, "Value", "Text", areaDeMaquina.Estado);
            return View(areaDeMaquina);
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