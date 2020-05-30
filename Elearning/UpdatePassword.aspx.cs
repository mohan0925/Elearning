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
    public partial class UpdatePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // if no session direct to user login
            if (Session.Count == 0)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
        }

        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            Users user = new Users();
            // checking all fields validation
            if (txtCurrentPassword.Text == "" || txtNewPassword.Text == "" || txtConfirmPassword.Text == "")
            {
                lblError.Text = "Enter password fields correctly.";
            }
            else if (txtCurrentPassword.Text.Length < 6)
            {
                lblError.Text = "Entered current password length is less than 6 characters.";
            }
            else if (txtNewPassword.Text.Length < 6)
            {
                lblError.Text = "Entered New password length is less than 6 characters.";
            }
            else if (txtConfirmPassword.Text.Length < 6)
            {
                lblError.Text = "Entered confirm password length is less than 6 characters.";
            }
            else if (!txtNewPassword.Text.Equals(txtConfirmPassword.Text))
            {
                lblError.Text = "Entered new password confirmation not equal to new password input.";
            }
            else
            {

                string password = Session["UserPassword"].ToString();
                // checking entered password and the existing pasword hashes
                if (!verifyMd5Hash(txtCurrentPassword.Text, password))
                {
                    lblError.Text = "Inputted current password does not match that in database for this user.";
                }
                else
                {
                    // pass password as parameters
                    user.UserName = Session["UserName"].ToString();
                    string hash = getMd5Hash(txtConfirmPassword.Text);
                    user.UserPassword = hash;
                    if (user.updatePassword())
                    {
                        Response.Redirect("~/UserAccount.aspx?UpdateSuccess=Password");
                    }
                    else
                    {
                        lblError.Text = "Database connection error";
                    }
                }

            }
        }
        // function to hash password
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