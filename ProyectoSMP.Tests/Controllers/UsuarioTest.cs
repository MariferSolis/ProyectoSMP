using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoSMP.Models;

namespace ProyectoSMP.Tests.Controllers
{

    [TestClass]
    public class UsuarioTest
    {

        [TestMethod]
        public void TestRead()
        {
            using (SMPEntities db = new SMPEntities())
            {
                Usuario usuario = new Usuario();
                usuario = db.Usuario.Find(2);

                Assert.AreEqual(1, db.SaveChanges());
            }
        }
    }
}
