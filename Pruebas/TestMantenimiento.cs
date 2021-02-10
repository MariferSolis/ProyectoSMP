using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoSMP.Models;

namespace Pruebas
{
    [TestClass]
    public class TestMantenimiento
    {
        [TestMethod]
        public void TestAdd()
        {
            using (SMPEntities db = new SMPEntities())
            {
                Mantenimiento mantenimiento = new Mantenimiento();
                mantenimiento.IdMantenimiento = 13;
                mantenimiento.IdMaquina = 4;
                mantenimiento.Seccion = "TA";
                mantenimiento.NumeroOperacion = 3;
                mantenimiento.NombreOperacion = "Control General";
                mantenimiento.Frecuencia = 5;
                mantenimiento.IdRol = 3;
                mantenimiento.IdUsuario = 3;
                mantenimiento.IdRepuesto = 6;
                mantenimiento.Detalles = "Revizar todas las funcionalidades de la maquina";
                mantenimiento.URLArchivo = "ddfdfd";
                db.Mantenimiento.Add(mantenimiento);
                Assert.AreEqual(1, db.SaveChanges());
            }
        }
        [TestMethod]
        public void TestRead()
        {
            using (SMPEntities db = new SMPEntities())
            {
                List<Mantenimiento> mantenimiento = new List<Mantenimiento>();
                mantenimiento = db.Mantenimiento.ToList();
                Assert.IsNotNull(mantenimiento);
            }
        }
        [TestMethod]
        public void TestUpdate()
        {
            using (SMPEntities db = new SMPEntities())
            {
                Mantenimiento mantenimiento = new Mantenimiento();
                mantenimiento = db.Mantenimiento.Find(8);
                mantenimiento.Seccion = "TR";
                bool estado;
                try
                {
                    db.Entry(mantenimiento).State = EntityState.Modified;
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
