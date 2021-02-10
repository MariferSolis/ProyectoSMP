using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoSMP.Models;
using System.Linq;
using System.Data.Entity;

namespace Pruebas
{
    [TestClass]
    public class TestAreaDeMaquina
    {
        [TestMethod]
        public void TestAdd()
        {
            using (SMPEntities db = new SMPEntities())
            {
                AreaDeMaquina areaDeMaquina = new AreaDeMaquina();
                areaDeMaquina.IdArea = 8;
                areaDeMaquina.Nombre = "Área 8";
                areaDeMaquina.Descripcion = "Area 8";
                areaDeMaquina.Estado = true;
                db.AreaDeMaquina.Add(areaDeMaquina);
                Assert.AreEqual(1, db.SaveChanges());
            }
        }
        [TestMethod]
        public void TestRead()
        {
            using (SMPEntities db = new SMPEntities())
            {
                List<AreaDeMaquina> areaDeMaquina = new List<AreaDeMaquina>();
                areaDeMaquina = db.AreaDeMaquina.ToList();
                Assert.IsNotNull(areaDeMaquina);
            }
        }
        [TestMethod]
        public void TestUpdate()
        {
            using (SMPEntities db = new SMPEntities())
            {
                AreaDeMaquina areaDeMaquina = new AreaDeMaquina();
                areaDeMaquina = db.AreaDeMaquina.Find(8);
                areaDeMaquina.Descripcion = "Area de sistema de maquina 8";
                bool estado;
                try
                {
                    db.Entry(areaDeMaquina).State = EntityState.Modified;
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
