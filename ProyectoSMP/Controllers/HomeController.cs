using ProyectoSMP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.Services.Description;

using System.Data;
using System.Data.Entity;

using System.Net;

using System.Data.SqlClient;
using System.Collections;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace ProyectoSMP.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private SMPEntities db = new SMPEntities();
        [Authorize(Roles = "Admin")]
        public ActionResult Index(MantenimientoxMaquina_Result man)
        {


            List<String> NombreMaq = new List<String>();
            List<int> Mantenimiento = new List<int>();
            List<String> NombreMaquina = new List<String>();
            List<int> Paro = new List<int>();
            List<int> Cump = new List<int>();
            List<int> NoCump = new List<int>();
            man.cmd = new SqlCommand("MantenimientoxMaquina", man.Conexion);

            man.cmd.CommandType = CommandType.StoredProcedure;

            man.Conexion.Open();
            man.dr = man.cmd.ExecuteReader();

            while (man.dr.Read())
            {

                Mantenimiento.Add(man.dr.GetInt32(0));
                NombreMaq.Add(man.dr.GetString(1));

            }
            man.dr.Close();
            man.cmd = new SqlCommand("CumpNoCump", man.Conexion);
            man.cmd.CommandType = CommandType.StoredProcedure;


            man.dr = man.cmd.ExecuteReader();
            while (man.dr.Read())
            {

                Cump.Add(man.dr.GetInt32(1));
                NoCump.Add(man.dr.GetInt32(0));

            }
            man.dr.Close();
            man.cmd = new SqlCommand("ParoxMaquina", man.Conexion);
            man.cmd.CommandType = CommandType.StoredProcedure;
            man.dr = man.cmd.ExecuteReader();
            while (man.dr.Read())
            {

                Paro.Add(man.dr.GetInt32(0));
                NombreMaquina.Add(man.dr.GetString(1));

            }
            man.dr.Close();
            ViewBag.NombreMaq = NombreMaq;
            ViewBag.Mantenimiento = Mantenimiento;
            ViewBag.Cump = Cump;
            ViewBag.NoCump = NoCump;
            ViewBag.NombreMaquina = NombreMaquina;
            ViewBag.Paro = Paro;
            man.Conexion.Close();

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