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
    public class CumplimientoController : Controller
    {
        private SMEntities db = new SMEntities();

        // GET: CumplimientoMantenimientoes
        [Authorize(Roles = "Admin")]
        public ActionResult Index(/*string cadena*/)
        {
            //if (cadena == null)
            //{
            //    cadena = "";
            //}
            var cumpli = db.Cumplimiento.Include(m => m.Mantenimiento).Include(m => m.Maquina).ToList();  
            return View(cumpli);
        }
        [Authorize(Roles = "Admin,Tecnico,Operador")]
        public ActionResult Index2(string cadena)
        {
            var usuario = Session["IdUsuario"];
            if (cadena == null)
            {
                cadena = "";
            }
            var plan = db.BuscarCumplixUsuario(Convert.ToInt32(usuario), cadena).ToList();
            return View(plan);
        }
        [Authorize(Roles = "Admin,Tecnico,Operador")]
        public FileResult Descargar(int? id)
        {

            Mantenimiento mantenimientoDeMaquina = db.Mantenimiento.Find(id);
            var ruta = mantenimientoDeMaquina.URLArchivo;
            return File(ruta, ruta);

        }
        public ActionResult Report()
        {
            return View(db.Cumplimiento.Include(m => m.Mantenimiento).ToList());
        }
        public ActionResult Print()
        {
            return new ActionAsPdf("Report")
            { FileName = "Test.pdf" };
        }
        // GET: CumplimientoMantenimientoes/Details/5
        [Authorize(Roles = "Admin,Tecnico")]
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
        [Authorize(Roles = "Admin,Tecnico,Operador")]
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
            ViewBag.ListaColores = new SelectList(new[] {
                                   new SelectListItem { Value = "verde", Text = "Verde" },
                                   new SelectListItem { Value = "amarillo", Text = "Amarillo" },
                                   new SelectListItem { Value = "rojo", Text = "Rojo" }
                                                               }, "Value", "Text");
            ViewBag.IdMantenimiento = new SelectList(db.Mantenimiento, "IdMantenimiento", "NombreOperacion", cumplimiento.IdMantenimiento);
            ViewBag.ListaEstado = new SelectList(new[] {
                                   new SelectListItem { Value = "true", Text = "Activo" },
                                   new SelectListItem { Value = "false", Text = "Inactivo" }
                                                               }, "Value", "Text");
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
                db.AgregarBitacora("Cumplimiento", "Realiza", "El usuario realiza la acción de crear una taear", Convert.ToInt32(Session["IdUsuario"]), DateTime.Now, "realizar");
                return RedirectToAction("Index2");
            }
            ViewBag.ListaColores = new SelectList(new[] {
                                   new SelectListItem { Value = "verde", Text = "Verde" },
                                   new SelectListItem { Value = "amarillo", Text = "Amarillo" },
                                   new SelectListItem { Value = "rojo", Text = "Rojo" }
                                                               }, "Value", "Text");
            ViewBag.IdMantenimiento = new SelectList(db.Mantenimiento, "IdMantenimiento", "NombreOperacion", cumplimiento.IdMantenimiento);
            ViewBag.ListaEstado = new SelectList(new[] {
                                   new SelectListItem { Value = "true", Text = "Activo" },
                                   new SelectListItem { Value = "false", Text = "Inactivo" }
                                                               }, "Value", "Text");
            return View(cumplimiento);
        }
        // GET: CumplimientoMantenimientoes/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Error = TempData["Message"].ToString();
            }
            ViewBag.ListaColores = new SelectList(new[] {
                                   new SelectListItem { Value = "verde", Text = "Verde" },
                                   new SelectListItem { Value = "amarillo", Text = "Amarillo" },
                                   new SelectListItem { Value = "rojo", Text = "Rojo" }
                                                               }, "Value", "Text");
            ViewBag.IdMaquina = db.ConsultarMaquinaListaTareas();
            //ViewBag.IdMaquina = new SelectList(db.Maquina, "IdMaquina", "NombreMaquina");
            //ViewBag.IdMantenimiento = new SelectList(db.Mantenimiento, "IdMantenimiento", "NombreOperacion");
            //Aqui va el buscar mantenimientos por maquina

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
                if (cumplimiento.Comienza<cumplimiento.Finaliza)
                {
                db.AgregarCumplimiento(cumplimiento.IdMantenimiento,cumplimiento.Comienza,cumplimiento.Finaliza,cumplimiento.Color);
                db.SaveChanges();
                db.AgregarBitacora("Cumplimiento", "Crear", "El usuario realiza la acción de crear una taear", Convert.ToInt32(Session["IdUsuario"]), DateTime.Now, "crear");
                return RedirectToAction("Index");

                }
                else
                {
                    @TempData["Message"] = "Las fechas no coinciden";
                    if (TempData["Message"] != null)
                    {
                        ViewBag.Error = TempData["Message"].ToString();
                    }
                    ViewBag.ListaColores = new SelectList(new[] {
                                   new SelectListItem { Value = "verde", Text = "Verde" },
                                   new SelectListItem { Value = "amarillo", Text = "Amarillo" },
                                   new SelectListItem { Value = "rojo", Text = "Rojo" }
                                                               }, "Value", "Text");
                    ViewBag.IdMaquina = db.ConsultarMaquinaListaTareas();
                    return View(cumplimiento);
                }             
            }
            ViewBag.ListaColores = new SelectList(new[] {
                                   new SelectListItem { Value = "verde", Text = "Verde" },
                                   new SelectListItem { Value = "amarillo", Text = "Amarillo" },
                                   new SelectListItem { Value = "rojo", Text = "Rojo" }
                                                               }, "Value", "Text");
            ViewBag.IdMaquina = db.ConsultarMaquinaListaTareas();
            //ViewBag.IdMantenimiento = new SelectList(db.Mantenimiento, "IdMantenimiento", "NombreOperacion", cumplimiento.IdMantenimiento);
            return View(cumplimiento);
        }
        [Authorize(Roles = "Admin")]
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
            ViewBag.ListaColores = new SelectList(new[] {
                                   new SelectListItem { Value = "verde", Text = "Verde" },
                                   new SelectListItem { Value = "amarillo", Text = "Amarillo" },
                                   new SelectListItem { Value = "rojo", Text = "Rojo" }
                                                               }, "Value", "Text");
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
                if (cumplimiento.Comienza < cumplimiento.Finaliza)
                {
                db.Entry(cumplimiento).State = EntityState.Modified;
                db.SaveChanges();
                db.AgregarBitacora("Cumplimiento", "Editar", "El usuario realiza la acción de editar una taear", Convert.ToInt32(Session["IdUsuario"]), DateTime.Now, "editar");
                return RedirectToAction("Index");
                }
                else
                {
                    @TempData["Message"] = "Las fechas no coinciden";
                    if (TempData["Message"] != null)
                    {
                        ViewBag.Error = TempData["Message"].ToString();
                    }
                    ViewBag.ListaColores = new SelectList(new[] {
                                   new SelectListItem { Value = "verde", Text = "Verde" },
                                   new SelectListItem { Value = "amarillo", Text = "Amarillo" },
                                   new SelectListItem { Value = "rojo", Text = "Rojo" }
                                                               }, "Value", "Text");
                    ViewBag.IdMantenimiento = new SelectList(db.Mantenimiento, "IdMantenimiento", "NombreOperacion", cumplimiento.IdMantenimiento);
                    return View(cumplimiento);
                }
                
            }
            ViewBag.ListaColores = new SelectList(new[] {
                                   new SelectListItem { Value = "verde", Text = "Verde" },
                                   new SelectListItem { Value = "amarillo", Text = "Amarillo" },
                                   new SelectListItem { Value = "rojo", Text = "Rojo" }
                                                               }, "Value", "Text");
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
        public List<ConsultarMantenimientoxMaquina_Result> ConsultarMantenimientoxMaquina(int IdMaquina)
        {
            List<ConsultarMantenimientoxMaquina_Result> usuarios = db.ConsultarMantenimientoxMaquina(IdMaquina).ToList();
            return usuarios;
        }
        public JsonResult ConsultarMantenimientoxMaquinas(int IdMaquina)
        {
            List<ConsultarMantenimientoxMaquina_Result> usuarios = db.ConsultarMantenimientoxMaquina(IdMaquina).ToList();

            return Json(usuarios, JsonRequestBehavior.AllowGet);
        }
    }
}