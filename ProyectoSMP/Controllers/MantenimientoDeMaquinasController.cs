using ProyectoSMP.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSMP.Controllers
{
    [Authorize]
    public class MantenimientoDeMaquinasController : Controller
    {

        private SMPEntities db = new SMPEntities();

        // GET: MantenimientoDeMaquinas
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }
            var mantenimientoDeMaquina = db.Mantenimiento.Include(m => m.InventarioDeRepuestos).Include(m => m.Maquina).Include(m => m.Rol);
            return View(mantenimientoDeMaquina.ToList());
        }
        [Authorize(Roles = "Admin")]
        public FileResult Descargar(int? id)
        {

            Mantenimiento mantenimientoDeMaquina = db.Mantenimiento.Find(id);
            var ruta = mantenimientoDeMaquina.URLArchivo;
            return File(ruta, ruta);

        }
        public ActionResult Report()
        {

            return View(db.Mantenimiento.Include(m => m.InventarioDeRepuestos).Include(m => m.Maquina).Include(m => m.Rol));
        }
        public ActionResult Print()
        {
            return new ActionAsPdf("Report")
            { FileName = "Test.pdf" };
        }
        // GET: MantenimientoDeMaquinas/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mantenimiento mantenimientoDeMaquina = db.Mantenimiento.Find(id);
            if (mantenimientoDeMaquina == null)
            {
                return HttpNotFound();
            }
            return View(mantenimientoDeMaquina);
        }

        // GET: MantenimientoDeMaquinas/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.IdRepuesto = new SelectList(db.InventarioDeRepuestos, "IdRepuesto", "Nombre");
            ViewBag.IdMaquina = new SelectList(db.Maquina, "IdMaquina", "NombreMaquina");
            ViewBag.IdRol = new SelectList(db.Rol, "IdRol", "Descripcion");
            return View();
        }

        // POST: MantenimientoDeMaquinas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Mantenimiento mantenimiento)
        {
            if (ModelState.IsValid)
            {
                string path = Server.MapPath("~/Content/Archivos/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string nombre = path + Path.GetFileName(mantenimiento.Archivo.FileName);
                mantenimiento.Archivo.SaveAs(path + Path.GetFileName(mantenimiento.Archivo.FileName));
                mantenimiento.URLArchivo = nombre;
                db.AgregarMantenimiento(mantenimiento.IdMaquina,mantenimiento.Seccion,mantenimiento.NumeroOperacion,mantenimiento.NombreOperacion,
                    mantenimiento.Frecuencia,mantenimiento.IdRol,mantenimiento.IdUsuario,mantenimiento.IdRepuesto,mantenimiento.Detalles,mantenimiento.URLArchivo);
                db.SaveChanges();
                @TempData["Message"] = "Se cargaron los archivos";
                return RedirectToAction("Index");
            }

            ViewBag.IdRepuesto = new SelectList(db.InventarioDeRepuestos, "IdRepuesto", "Nombre", mantenimiento.IdRepuesto);
            ViewBag.IdMaquina = new SelectList(db.Maquina.Where(x => x.Estado == true).ToList(), "IdMaquina", "NombreMaquina", mantenimiento.IdMaquina);
            ViewBag.IdRol = new SelectList(db.Rol, "IdRol", "Descripcion", mantenimiento.Rol);
            
            return View(mantenimiento);
        }

        // GET: MantenimientoDeMaquinas/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Mantenimiento mantenimientoDeMaquina = db.Mantenimiento.Find(id);

            if (mantenimientoDeMaquina == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRepuesto = new SelectList(db.InventarioDeRepuestos, "IdRepuesto", "Nombre", mantenimientoDeMaquina.IdRepuesto);
            ViewBag.IdMaquina = new SelectList(db.Maquina.Where(x => x.Estado == true).ToList(), "IdMaquina", "NombreMaquina", mantenimientoDeMaquina.IdMaquina);
            ViewBag.IdRol = new SelectList(db.Rol, "IdRol", "Descripcion", mantenimientoDeMaquina.Rol);
            ViewBag.ListaIdUsuario = CargaUsuario(Convert.ToInt32(mantenimientoDeMaquina.IdUsuario)).ToList();
            return View(mantenimientoDeMaquina);
        }

        // POST: MantenimientoDeMaquinas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Mantenimiento mantenimientoDeMaquina)
        {
            if (ModelState.IsValid)
            {
                if (mantenimientoDeMaquina.Archivo != null)
                {
                string path = Server.MapPath("~/Content/Archivos/");
                string nombre = path + Path.GetFileName(mantenimientoDeMaquina.Archivo.FileName);
                mantenimientoDeMaquina.Archivo.SaveAs(path + Path.GetFileName(mantenimientoDeMaquina.Archivo.FileName));
                mantenimientoDeMaquina.URLArchivo = nombre;
                }             
                db.Entry(mantenimientoDeMaquina).State = EntityState.Modified;
                db.SaveChanges();
                @TempData["Message"] = "Se editaron los datos";
                return RedirectToAction("Index");
            }
            ViewBag.IdRepuesto = new SelectList(db.InventarioDeRepuestos, "IdRepuesto", "Nombre", mantenimientoDeMaquina.IdRepuesto);
            ViewBag.IdMaquina = new SelectList(db.Maquina.Where(x => x.Estado == true).ToList(), "IdMaquina", "NombreMaquina", mantenimientoDeMaquina.IdMaquina);
            ViewBag.Rol = new SelectList(db.Rol, "IdRol", "Descripcion", mantenimientoDeMaquina.Rol);
            ViewBag.ListaIdUsuario = CargaUsuario(Convert.ToInt32(mantenimientoDeMaquina.IdUsuario)).ToList();
            return View(mantenimientoDeMaquina);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public List<ConsultarUsuariosxRol_Result> CargaUsuario(int IdRol)
        {
            List<ConsultarUsuariosxRol_Result> usuarios = db.ConsultarUsuariosxRol(IdRol).Where(x => x.Estado == true).ToList();
            return usuarios;
        }
        public JsonResult CargaUsuarios(int IdRol)
        {
            List<ConsultarUsuariosxRol_Result> usuarios = db.ConsultarUsuariosxRol(IdRol).Where(x => x.Estado == true).ToList();

            return Json(usuarios, JsonRequestBehavior.AllowGet);
        }
    }
}