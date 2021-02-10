using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoSMP.Models;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Linq;

namespace Pruebas
{

    [TestClass]
    public class TestUsuario
    {


        [TestMethod]
        public void TestAdd()
        {
            using (SMPEntities db = new SMPEntities())
            {
                Usuario usuario = new Usuario();
                usuario.IdUsuario = 8;
                usuario.Identificacion = "12345678";
                usuario.IdTipoDeIdentificacion = 1;
                usuario.Nombre = "Andres";
                usuario.Apellidos = "Rodriguez";
                usuario.Correo = "andres@test.com";
                usuario.Password = "abc";
                usuario.TipoCarga = "Administrator";
                usuario.Provincia = "1";
                usuario.Canton = "01";
                usuario.Distrito = "02";
                usuario.IdRol = 1;
                usuario.Estado = true;
                usuario.token_recovery = null;

                db.Usuario.Add(usuario);
                Assert.AreEqual(1, db.SaveChanges());
            }
        }

        [TestMethod]
        public void TestDelete()
        {
            using (SMPEntities db = new SMPEntities())
            {
                Usuario usuario = new Usuario();
                usuario = db.Usuario.Find(8);
                db.Usuario.Remove(usuario);
                Assert.AreEqual(1, db.SaveChanges());
            }
        }
        [TestMethod]
        public void TestRead()
        {
            using (SMPEntities db = new SMPEntities())
            {
                List<ConsultarUsuarios_Result> usuario = new List<ConsultarUsuarios_Result>();
                usuario = db.ConsultarUsuarios().Where(x => x.Estado == true).ToList();
                Assert.IsNotNull(usuario);
            }
        }



        [TestMethod]
        public void TestUpdate()
        { 
            using (SMPEntities db = new SMPEntities())
            {
                Usuario usuario = new Usuario();
                usuario = db.Usuario.Find(8);
                usuario.Nombre = "Juan";
                bool estado;
                try
                {
                    db.Entry(usuario).State = EntityState.Modified;
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
