using Elearning.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Elearning
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                // checking field validations
                if (txtUsername.Text.Length < 5 || txtUsername.Text.Length > 20)
                {
                    lblError.Text = " Entered username length is less than 6 or greater than 20 characters.";
                }
                else if (txtPassword.Text.Length < 6)
                {
                    lblError.Text = " Entered password length is less than 5 characters .";
                }
                else
                {
                    Users user = new Users();
                    user.UserName = txtUsername.Text;
                    user.UserPassword = txtPassword.Text;
                    // get user details on passing username and password
                    DataTable dt_user_login = user.authenticate();
                    if (dt_user_login.Rows.Count>0)
                    {
                        string hash = dt_user_login.Rows[0]["UserPassword"].ToString();
                        string source = txtPassword.Text;
                        // checking if the actual and the text password are correct
                        // both the hashes are checked
                        // if correct sessions are created
                        if (verifyMd5Hash(source, hash))
                        {
                            Session["UserName"] = dt_user_login.Rows[0]["UserName"].ToString();
                            Session["UserPassword"] = dt_user_login.Rows[0]["UserPassword"].ToString();
                            Session["RealName"] = dt_user_login.Rows[0]["RealName"].ToString();
                            Session["EmailAddress"] = dt_user_login.Rows[0]["EmailAddress"].ToString();
                            Session["RoleID"] = dt_user_login.Rows[0]["RoleID"].ToString();
                            Session["CourseID"] = dt_user_login.Rows[0]["CourseID"].ToString();
                            Response.Redirect("~/UserAccount.aspx");
                        }
                        else
                        {
                            lblError.Text = "Password is incorrect.";
                        }
                    }
                    else
                    {
                        lblError.Text = "Username is incorrect.";
                    }
                }
            }  
        }


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

        // Verify a hash against a string.
        static bool verifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = getMd5Hash(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}