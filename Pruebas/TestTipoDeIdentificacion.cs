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
    public class TestTipoDeIdentificacion
    {
        [TestMethod]
        public void TestAdd()
        {
            using (SMPEntities db = new SMPEntities())
            {
                TipoDeIdentificacion tipoDeIdentificacion = new TipoDeIdentificacion();

                tipoDeIdentificacion.IdTipoIdentificacion = 4;
                tipoDeIdentificacion.Descripcion = "Pasante";
                tipoDeIdentificacion.Estado = true;
                db.TipoDeIdentificacion.Add(tipoDeIdentificacion);
                Assert.AreEqual(1, db.SaveChanges());
            }
        }
        [TestMethod]
        public void TestRead()
        {
            using (SMPEntities db = new SMPEntities())
            {
                List<TipoDeIdentificacion> tipoDeIdentificacion = new List<TipoDeIdentificacion>();
                tipoDeIdentificacion = db.TipoDeIdentificacion.Where(x => x.Estado == true).ToList();
                Assert.IsNotNull(tipoDeIdentificacion);
            }
        }
        [TestMethod]
        public void TestUpdate()
        {
            using (SMPEntities db = new SMPEntities())
            {
                TipoDeIdentificacion tipoDeIdentificacion = new TipoDeIdentificacion();
                tipoDeIdentificacion = db.TipoDeIdentificacion.Find(4);
                tipoDeIdentificacion.Descripcion = "Identificación Física";
                bool estado;
                try
                {
                    db.Entry(tipoDeIdentificacion).State = EntityState.Modified;
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
