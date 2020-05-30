using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Elearning.App_Code;
using System.Data;
using System.Text.RegularExpressions;

namespace Elearning
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Courses course = new Courses();
                UserRoles user_roles = new UserRoles();
                // Fetching all the courses
                DataTable datatable_getallcourses = course.getallcourses();
                // fetching all the roles
                DataTable datatable_getallroles = user_roles.getallroles();
                // if data is fetched then the data is bind to asp controls
                if (datatable_getallcourses.Rows.Count>0 && datatable_getallroles.Rows.Count > 0)
                {
                    ddlCourses.DataSource = datatable_getallcourses;
                    ddlCourses.DataValueField = "CourseID";
                    ddlCourses.DataTextField = "CourseName";
                    ddlCourses.DataBind();

                    ddlRoles.DataSource = datatable_getallroles;
                    ddlRoles.DataValueField = "RoleID";
                    ddlRoles.DataTextField = "RoleName";
                    ddlRoles.DataBind();
                   
                }
            }
            else
            {
                lblError.Text = "Database connection error.";
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                // checking if all the fields are valid or not
                if (txtUsername.Text.Length < 5 || txtUsername.Text.Length > 20)
                {
                    lblError.Text = " Entered username length is less than 6 or greater than 20 characters.";
                }
                else if (txtPassword.Text.Length < 6)
                {
                    lblError.Text = "Entered password length is less than 5 characters.";
                }
                else if (!txtConfirmPassword.Text.Equals(txtPassword.Text))
                {
                    lblError.Text = "Entered password confirmation not equal to password input.";
                }
                else if (txtRealName.Text.Equals(""))
                {
                    lblError.Text = " Entered real name field is empty.";
                }
                else if (txtEmailAddress.Text.Equals(""))
                {
                    lblError.Text = "Entered email field is empty.";
                }
                else if (!txtEmailAddress.Text.Contains("@dmu1.ac.uk"))
                {
                    lblError.Text = "Email does not contain the string 'dmu1.ac.uk'";
                }
                else
                {
                    lblError.Text = "";
                    Users user = new Users();
                    user.UserName = txtUsername.Text;
                    // checking if the username exists or not
                    if (user.userNameExists())
                    {
                        lblError.Text = "Username must already exist.";
                    }
                    else
                    {
                        // passing parameters
                        user.UserName = txtUsername.Text;
                        user.UserPassword = txtPassword.Text;
                        user.RealName = txtRealName.Text;
                        user.EmailAddress = txtEmailAddress.Text;
                        user.RoleID = Int32.Parse(ddlRoles.SelectedValue);
                        user.CourseID = Int32.Parse(ddlCourses.SelectedValue);

                        // inserting the data into the database
                        if (user.insertUserRecord())
                        {
                            Response.Redirect("~/UserLogin.aspx");
                        }
                        else
                        {
                            lblError.Text = "Database connection error.";
                        }
                    }
                }
            }
            catch
            {
                lblError.Text = "Database connection error";
            }
        }
}
}