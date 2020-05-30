using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Elearning.App_Code
{
    public class CourseModules
    {
        public int CourseId { get; set; }
        public int ModuleId { get; set; }

        private DatabaseConnection databaseconnection;
        public CourseModules()
        {
            databaseconnection = new DatabaseConnection();
        }


    }
}