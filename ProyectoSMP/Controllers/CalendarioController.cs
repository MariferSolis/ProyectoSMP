using ProyectoSMP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSMP.Controllers
{
    public class CalendarioController : Controller
    {
        private SMPEntities4 db = new SMPEntities4();
        // GET: Calendario
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetEvents()
        {
            var events = db.ConsultarCalendario().ToString();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult SaveEvent(Calendario calendario)
        {
            var status = false;

            if (calendario.IdEvento > 0)
            {

                db.ActualizarCalendario(calendario.IdEvento, calendario.Asunto,
                    calendario.Descripcion, calendario.Comienza, calendario.Fin,
                    calendario.Color, calendario.TodoDia);

                     status = true;                        
            }
            else
            {
                db.AgregarCalendario(calendario.Asunto,
                    calendario.Descripcion, calendario.Comienza, calendario.Fin,
                    calendario.Color, calendario.TodoDia);              
                    status = true;  

            }
            return new JsonResult { Data = new { status = status } };
        }
        [HttpPost]
        public JsonResult DeleteEvent(int IdEvento)
        {
            var status = false;          
             db.EliminarCalendario(IdEvento);
           
                status = true;
          
            return new JsonResult { Data = new { status = status } };
        }
    }
}