﻿using ProyectoSMP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ProyectoSMP.Tool
{
    public class CustomRoleProvider : RoleProvider
    {
        private SMEntities db = new SMEntities();
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
      
            var roles = db.ConsultarRolxUsuario(username);
            return roles.ToArray();
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