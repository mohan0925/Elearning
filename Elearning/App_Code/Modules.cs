using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Elearning.App_Code
{
    public class Modules
    {
        public int ModuleId { get; set; }
        public int CourseId { get; set; }
        public int ModuleCode { get; set; }
        public string ModuleName { get; set; }

        private DatabaseConnection databaseconnection;
        public Modules()
        {
            databaseconnection = new DatabaseConnection();
        }
        // Fetch all the modules of the particular course
        public DataTable getallModules()
        {
            databaseconnection.addParameter("@CourseID", CourseId);
            string cmd = "select * from Modules where ModuleID in (select ModuleID from CourseModules where CourseID=@CourseID)";
            return databaseconnection.executeReader(cmd);
        }

    }
}