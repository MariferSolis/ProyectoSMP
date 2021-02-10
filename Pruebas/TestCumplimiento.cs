using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoSMP.Models;

namespace Pruebas
{
    [TestClass]
    public class TestCumplimiento
    {
        [TestMethod]
        public void TestAdd()
        {
            using (SMPEntities db = new SMPEntities())
            {
                Cumplimiento cumplimiento = new Cumplimiento();
                cumplimiento.IdCumplimiento = 7;
                cumplimiento.IdMantenimiento = 13;
                cumplimiento.Comienza = Convert.ToDateTime("11/02/2021");
                cumplimiento.Finaliza = Convert.ToDateTime("20/02/2021");
                cumplimiento.Fecha = Convert.ToDateTime("10/02/2021");
                cumplimiento.Estado = true;
                cumplimiento.Detalles = "Realizar el matenimiento";
                cumplimiento.Color = "Verde";
                db.Cumplimiento.Add(cumplimiento);
                Assert.AreEqual(1, db.SaveChanges());
            }
        }
        [TestMethod]
        public void TestRead()
        {
            using (SMPEntities db = new SMPEntities())
            {
                List<Cumplimiento> cumplimiento = new List<Cumplimiento>();
                cumplimiento = db.Cumplimiento.ToList();
                Assert.IsNotNull(cumplimiento);
            }
        }
        [TestMethod]
        public void TestUpdate()
        {
            using (SMPEntities db = new SMPEntities())
            {
                Cumplimiento cumplimiento = new Cumplimiento();
                cumplimiento = db.Cumplimiento.Find(7);
                cumplimiento.Finaliza = Convert.ToDateTime("22/02/2021");
                bool estado;
                try
                {
                    db.Entry(cumplimiento).State = EntityState.Modified;
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
