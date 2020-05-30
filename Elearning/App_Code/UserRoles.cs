using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Elearning.App_Code
{
    public class UserRoles
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        private DatabaseConnection dataConn;
        public UserRoles()
        {
            dataConn = new DatabaseConnection();
        }
        // Fetch all the roles 
        public DataTable getallroles()
        {
            string command = "Select * FROM UserRoles";
            return dataConn.executeReader(command);
        }
    }
}