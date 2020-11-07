using ProyectoSMP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ProyectoSMP.Tool
{
    public class CustomerRoleProvider : RoleProvider
    {
        private SMPEntities14 db = new SMPEntities14();
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            throw new NotImplementedException();
            //List<Rol> lista = db.ConsultarRolxUsuario(username);

            //string[] arreglo = new string[lista.Count];

            //for (int i = 0; i < arreglo.Length; i++)
            //{
            //    arreglo[i] = lista.ElementAt(i).Descripcion;
            //}
            //return arreglo;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {

            throw new NotImplementedException();
            //return clsRol.ConsultaUR(username, roleName);


            /*userDAL = new UserDALImpl();
            return userDAL.isUserInRole(username, roleName);*/
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}