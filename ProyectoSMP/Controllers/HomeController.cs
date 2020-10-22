using ProyectoSMP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace ProyectoSMP.Controllers
{
    public class HomeController : Controller
    {
        private SMPEntities4 db = new SMPEntities4();
        public ActionResult Index()
        {
            
          
            return View();
        }

        public ActionResult About()
        {
           

            return View();
        }

        public ActionResult Contact()
        {
           

            return View();
        }
        public ActionResult Dashboard()
        {

            return View();
        }
    }
}