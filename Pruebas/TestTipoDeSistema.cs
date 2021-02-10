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
    public class TestTipoDeSistema
    {
        [TestMethod]
        public void TestAdd()
        {
            using (SMPEntities db = new SMPEntities())
            {
                TipoDeSistemaDeMaquina tipoDeSistemaDeMaquina = new TipoDeSistemaDeMaquina();

                tipoDeSistemaDeMaquina.IdTipoSistema = 5;
                tipoDeSistemaDeMaquina.Nombre = "Tipo 5";
                tipoDeSistemaDeMaquina.Descripcion = "Tipo 5";
                tipoDeSistemaDeMaquina.Estado = true;
                db.TipoDeSistemaDeMaquina.Add(tipoDeSistemaDeMaquina);
                Assert.AreEqual(1, db.SaveChanges());
            }
        }
        [TestMethod]
        public void TestRead()
        {
            using (SMPEntities db = new SMPEntities())
            {
                List<TipoDeSistemaDeMaquina> tipoDeSistemaDeMaquina = new List<TipoDeSistemaDeMaquina>();
                tipoDeSistemaDeMaquina = db.TipoDeSistemaDeMaquina.ToList();
                Assert.IsNotNull(tipoDeSistemaDeMaquina);
            }
        }
        [TestMethod]
        public void TestUpdate()
        {
            using (SMPEntities db = new SMPEntities())
            {
                TipoDeSistemaDeMaquina tipoDeSistemaDeMaquina = new TipoDeSistemaDeMaquina();
                tipoDeSistemaDeMaquina = db.TipoDeSistemaDeMaquina.Find(5);
                tipoDeSistemaDeMaquina.Descripcion = "Tipo de sistema de maquina 5";
                bool estado;
                try
                {
                    db.Entry(tipoDeSistemaDeMaquina).State = EntityState.Modified;
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
