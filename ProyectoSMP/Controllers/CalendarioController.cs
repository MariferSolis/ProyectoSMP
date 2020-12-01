using ProyectoSMP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSMP.Controllers
{
    [Authorize]
    public class CalendarioController : Controller
    {
        private SMPEntities db = new SMPEntities();
        // GET: Calendario
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetEvents()
        {
            var events = db.Calendario.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        [HttpPost]
        public JsonResult SaveEvent(Calendario e)
        {
            var status = false;
           
                if (e.IdEvento > 0)
                {
                    //Update the event
                    var v = db.Calendario.Where(a => a.IdEvento == e.IdEvento).FirstOrDefault();
                    if (v != null)
                    {
                        v.Asunto = e.Asunto;
                        v.Inicia = e.Inicia;
                        v.Finaliza = e.Finaliza;
                        v.Descripcion = e.Descripcion;
                        v.TodoElDia = e.TodoElDia;
                        v.Color = e.Color;
                    }                
            }
            else
                {                   
                    db.AgregarCalendario(e.Asunto,e.Descripcion,e.Inicia,e.Finaliza,e.Color,e.TodoElDia);
                db.AgregarBitacora("Calendario", "Crear", "El usuario realiza la acción de crear un evento", Convert.ToInt32(Session["IdUsuario"]), DateTime.Now, "crear");
            }
                db.SaveChanges();
                status = true;
            return new JsonResult { Data = new { status = status } };
        }
        [HttpPost]
        public JsonResult DeleteEvent(int idEvento)
        {
            var status = false;
         
                var v = db.Calendario.Where(a => a.IdEvento == idEvento).FirstOrDefault();
                if (v != null)
                {
                    db.Calendario.Remove(v);
                    db.SaveChanges();
                db.AgregarBitacora("Calendario", "Eliminar", "El usuario realiza la acción de eliminar un evento", Convert.ToInt32(Session["IdUsuario"]), DateTime.Now, "eliminar");
                status = true;
                }            
            return new JsonResult { Data = new { status = status } };
        }
    }
}