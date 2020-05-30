using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Elearning.App_Code
{
    public class Users
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string RealName { get; set; }
        public string EmailAddress { get; set; }
        public int RoleID { get; set; }
        public int CourseID { get; set; }

        private DatabaseConnection databaseconnection;

        public Users()
        {
            databaseconnection = new DatabaseConnection();
        }
        // Function to check whether the username exists in the database on passing username
        public bool userNameExists()
        {
            databaseconnection.addParameter("@UserName", UserName);
            string command = "Select COUNT(UserName) FROM Users WHERE UserName=@UserName";
            int result = databaseconnection.executeScalar(command);
            return result > 0 || result == -1;
        }
        // Encrypting password using md5 algorithm
        static string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        // Function to insert the user data in database
        // returns true or false based on the value inserted or not
        public bool insertUserRecord()
        {
            databaseconnection.addParameter("@UserName", UserName);
            string hashed_password= getMd5Hash(UserPassword);
            databaseconnection.addParameter("@UserPassword", hashed_password);
            databaseconnection.addParameter("@RealName", RealName);
            databaseconnection.addParameter("@EmailAddress", EmailAddress);
            databaseconnection.addParameter("@RoleID", RoleID);
            databaseconnection.addParameter("@CourseID", CourseID);
            string command = "INSERT INTO Users (UserName, UserPassword, RealName, EmailAddress, RoleID, CourseID) " +
                            "VALUES (@UserName, @Userpassword, @RealName, @EmailAddress, @RoleID, @CourseID)";
            return databaseconnection.executeNonQuery(command) > 0;
        }
        // fetch user details on passing username
        public DataTable authenticate()
        {
            databaseconnection.addParameter("@UserName", UserName);
            string command = "Select * from Users WHERE UserName = @UserName";
            return databaseconnection.executeReader(command);
        }

        //Fetch all the details of the tutors
        // course id and role id passed as parameters
        public DataTable getalltutors()
        {
            databaseconnection.addParameter("@CourseID", CourseID);
            databaseconnection.addParameter("@RoleID", RoleID);
            string command = "Select UserID,RealName FROM Users Where RoleID = @RoleID AND CourseID = @CourseID";
            return databaseconnection.executeReader(command);
        }
        // fetch the tutor email on passing the user id as parameter
        public DataTable getTutorEmail()
        {
            databaseconnection.addParameter("@UserID", UserID);
            string command = "Select EmailAddress from Users WHERE UserID = @UserID";
            return databaseconnection.executeReader(command);
        }
        // update password on passing new password
        // new password and username are passed as parameters
        public bool updatePassword()
        {
            databaseconnection.addParameter("@UserName", UserName);
            databaseconnection.addParameter("@UserPassword", UserPassword);
            string command = "UPDATE Users SET UserPassword = @UserPassword WHERE UserName = @UserName";
            return databaseconnection.executeNonQuery(command) > 0;
        }
        // fetch all the student details 
        // role id and course id are passed as parameters
        public DataTable getallStudents()
        {
            databaseconnection.addParameter("@RoleID", RoleID);
            databaseconnection.addParameter("@CourseID", CourseID);
            string command = "Select UserID,RealName FROM Users Where RoleID = @RoleID AND CourseID = @CourseID";
            return databaseconnection.executeReader(command);
        }
        // delete user record on passing the user id as parameter
        public bool removeuser()
        {
            databaseconnection.addParameter("@UserID", UserID);
            string command = "DELETE FROM Users WHERE UserID=@UserID";
            return databaseconnection.executeNonQuery(command) > 0;
        }
        // update the course of the tutor
        // course id and username are the parameters
        public bool updateCourse()
        {
            databaseconnection.addParameter("@UserName", UserName);
            databaseconnection.addParameter("@CourseID", CourseID);
            string command = "UPDATE Users SET CourseID = @CourseID WHERE UserName = @UserName";
            return databaseconnection.executeNonQuery(command) > 0;
        }
        // fetch the row of user details on passing username as parameter
        public DataTable Studentdetails()
        {
            databaseconnection.addParameter("@UserName", UserName);
            string command = "Select * from Users WHERE UserName = @UserName";
            return databaseconnection.executeReader(command);
        }
    }
}