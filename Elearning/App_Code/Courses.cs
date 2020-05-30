using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Elearning.App_Code
{
    public class Courses
    {
        // Initialising the  courseid variable
        public int CourseID { get; set; }
        // Initialising the  course name variable
        public string CourseName { get; set; }

        private DatabaseConnection databaseconnection;

        public Courses()
        {
            // Establishing database connections
            databaseconnection = new DatabaseConnection();
        }
        // fetching courses table data
        public DataTable getallcourses()
        {
            string command = "Select * FROM Courses";
            return databaseconnection.executeReader(command);
        }
        // fetch course name on passing course id
        public DataTable getCourseName()
        {
            databaseconnection.addParameter("@CourseID", CourseID);
            string command = "Select CourseName FROM Courses Where CourseID = @CourseID ";
            return databaseconnection.executeReader(command);
        }

    }
}