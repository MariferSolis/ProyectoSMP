using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoSMP.Models;

namespace Pruebas
{
    [TestClass]
    public class TestParoDeMaquina
    {
        [TestMethod]
        public void TestAdd()
        {
            using (SMPEntities db = new SMPEntities())
            {
                ParoDeMaquina paroDeMaquina = new ParoDeMaquina();
                paroDeMaquina.IdParo = 9;
                paroDeMaquina.IdMaquina = 4;
                paroDeMaquina.IdMantenimiento = 13;
                paroDeMaquina.Tipo = "Correctivo";
                paroDeMaquina.Descripcion = "Fallo en roles y necesita cambio";
                paroDeMaquina.FechaComienza = Convert.ToDateTime("10/02/2021");
                paroDeMaquina.FechaFin = Convert.ToDateTime("10/02/2021");
                db.ParoDeMaquina.Add(paroDeMaquina);
                Assert.AreEqual(1, db.SaveChanges());
            }
        }
        [TestMethod]
        public void TestRead()
        {
            using (SMPEntities db = new SMPEntities())
            {
                List<ParoDeMaquina> paroDeMaquina = new List<ParoDeMaquina>();
                paroDeMaquina = db.ParoDeMaquina.ToList();
                Assert.IsNotNull(paroDeMaquina);
            }
        }
        [TestMethod]
        public void TestUpdate()
        {
            using (SMPEntities db = new SMPEntities())
            {
                ParoDeMaquina paroDeMaquina = new ParoDeMaquina();
                paroDeMaquina = db.ParoDeMaquina.Find(7);
                paroDeMaquina.FechaFin = Convert.ToDateTime("11/02/2021");
                bool estado;
                try
                {
                    db.Entry(paroDeMaquina).State = EntityState.Modified;
                    estado = true;
                }
                catch (Exception)
                {
                    estado = false;
                }
                db.SaveChanges();
                Assert.AreEqual(true, estado);
            }
        }
    }
}
