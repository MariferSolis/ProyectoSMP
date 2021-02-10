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
    public class TestMaquina
    {
        [TestMethod]
        public void TestAdd()
        {
            using (SMPEntities db = new SMPEntities())
            {
                Maquina maquina = new Maquina();
                maquina.IdMaquina = 8;
                maquina.NombreMaquina = "Cortadora fina";
                maquina.IdTipoSistema = 4;
                maquina.IdArea = 3;
                maquina.Codigo = "3-GHS9";
                maquina.Modelo = "500";
                maquina.Proceso = "Cortar Lentes";
                maquina.Cadencia = 2;
                maquina.Descripcion = "Realiza cortes finos a los lentes";
                maquina.Estado = true;
                db.Maquina.Add(maquina);
                Assert.AreEqual(1, db.SaveChanges());
            }
        }
        [TestMethod]
        public void TestRead()
        {
            using (SMPEntities db = new SMPEntities())
            {
                List<Maquina> maquina = new List<Maquina>();
                maquina = db.Maquina.ToList();
                Assert.IsNotNull(maquina);
            }
        }
        [TestMethod]
        public void TestUpdate()
        {
            using (SMPEntities db = new SMPEntities())
            {
                Maquina maquina = new Maquina();
                maquina = db.Maquina.Find(8);
                maquina.Modelo = "300L2";
                bool estado;
                try
                {
                    db.Entry(maquina).State = EntityState.Modified;
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
