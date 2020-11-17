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
    public class MaquinasController : Controller
    {     
        private SMPEntities db = new SMPEntities();

        // GET: Maquinas
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }
            var maquina = db.Maquina.Include(m => m.AreaDeMaquina).Include(m => m.TipoDeSistemaDeMaquina).Where(x => x.Estado == true).ToList();
            return View(maquina.ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Todos()
        {
            return View(db.Maquina.Include(m => m.AreaDeMaquina).Include(m => m.TipoDeSistemaDeMaquina).ToList());
        }

        // GET: Maquinas/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maquina maquina = db.Maquina.Find(id);
            if (maquina == null)
            {
                return HttpNotFound();
            }
            return View(maquina);
        }

        // GET: Maquinas/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            if (TempData["MessageCodigo"] != null)
            {
                ViewBag.ErrorCodigo = TempData["MessageCodigo"].ToString();
            }
            ViewBag.IdArea = new SelectList(db.AreaDeMaquina, "IdArea", "Nombre");
            ViewBag.IdTipoSistema = new SelectList(db.TipoDeSistemaDeMaquina, "IdTipoSistema", "Nombre");
            return View();
        }

        // POST: Maquinas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Maquina maquina)
        {
            if (ModelState.IsValid)
            {
                var dato = db.ExisteCodigo(maquina.Codigo).FirstOrDefault();
                if (dato==null)
                {
                db.AgregarMaquina(maquina.NombreMaquina,maquina.IdTipoSistema,maquina.IdArea,maquina.Codigo,maquina.Modelo,maquina.Proceso,maquina.Cadencia,maquina.Descripcion);
                db.SaveChanges();

                    @TempData["Message"] = "Máquina ingresada con exito";
                    return RedirectToAction("Index");
                }
                else
                {
                    @TempData["MessageCodigo"] = "El código ya existe debe de ingresar otro";

                    return RedirectToAction("Create", maquina);
                }
                
            }
            if (TempData["MessageCodigo"] != null)
            {
                ViewBag.ErrorCodigo = TempData["MessageCodigo"].ToString();
            }
            ViewBag.IdArea = new SelectList(db.AreaDeMaquina, "IdArea", "Nombre", maquina.IdArea);
            ViewBag.IdTipoSistema = new SelectList(db.TipoDeSistemaDeMaquina, "IdTipoSistema", "Nombre", maquina.IdTipoSistema);
            return View(maquina);
        }

        // GET: Maquinas/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maquina maquina = db.Maquina.Find(id);
            if (maquina == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdArea = new SelectList(db.AreaDeMaquina, "IdArea", "Nombre", maquina.IdArea);
            ViewBag.IdTipDeSistema = new SelectList(db.TipoDeSistemaDeMaquina, "IdTipoSistema", "Nombre", maquina.IdTipoSistema);
            return View(maquina);
        }

        // POST: Maquinas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMaquina,NombreMaquina,IdTipDeSistema,IdArea,Codigo,Modelo,Proceso,Cadencia,Descripcion")] Maquina maquina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maquina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdArea = new SelectList(db.AreaDeMaquina, "IdArea", "Nombre", maquina.IdArea);
            ViewBag.IdTipDeSistema = new SelectList(db.TipoDeSistemaDeMaquina, "IdTipoSistema", "Nombre", maquina.IdTipoSistema);
            return View(maquina);
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