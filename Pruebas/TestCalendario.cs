using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoSMP.Models;

namespace Pruebas
{
    [TestClass]
    public class TestCalendario
    {
        [TestMethod]
        public void TestAdd()
        {
            using (SMPEntities db = new SMPEntities())
            {
                Calendario calendario = new Calendario();
                calendario.IdEvento = 15;
                calendario.Asunto = "Reunión personal";
                calendario.Descripcion = "En sala de conferencias todo el personal se debe de reunir";
                calendario.Inicia = Convert.ToDateTime("10/02/2021");
                calendario.Finaliza = Convert.ToDateTime("11/02/2021");
                calendario.TodoElDia = false;
                calendario.Color = "#fd9797";
                db.Calendario.Add(calendario);
                Assert.AreEqual(1, db.SaveChanges());
            }
        }
        [TestMethod]
        public void TestRead()
        {
            using (SMPEntities db = new SMPEntities())
            {
                List<Calendario> calendario = new List<Calendario>();
                calendario = db.Calendario.ToList();
                Assert.IsNotNull(calendario);
            }
        }
        [TestMethod]
        public void TestUpdate()
        {
            using (SMPEntities db = new SMPEntities())
            {
                Calendario calendario = new Calendario();
                calendario = db.Calendario.Find(9);
                calendario.Asunto = "Reunión General";
                bool estado;
                try
                {
                    db.Entry(calendario).State = EntityState.Modified;
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
        [TestMethod]
        public void TestDelete()
        {
            using (SMPEntities db = new SMPEntities())
            {
                Calendario calendario = new Calendario();
                calendario = db.Calendario.Find(9);
                db.Calendario.Remove(calendario);
                Assert.AreEqual(1, db.SaveChanges());
            }
        }
    }
}
