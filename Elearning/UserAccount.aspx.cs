using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Elearning
{
    public partial class UserAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // if no session direct to user login
            lblUpdateSuccess.Text = "";
            if (Session.Count == 0)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                // condition to check if role is a student
                if (Int32.Parse(Session["RoleID"].ToString()) == 1)
                {
                    // setting visibility of the buttons based on the user role
                    lblUserAccount.Text = "Student Account";
                    lblTutorChangePassword.Visible = false;
                    btnLogout.Visible = true;
                    btnUserDetails.Visible = true;
                    btnTutorDetails.Visible = false;
                    btnUpdatePassword.Visible = true;
                }
                else
                {
                    // setting visibility of the buttons based on the user role
                    lblUserAccount.Text = "Tutor Account";
                    lblTutorChangePassword.Visible = true;
                    btnLogout.Visible = true;
                    btnUserDetails.Visible = false;
                    btnTutorDetails.Visible = true;
                    btnUpdatePassword.Visible = true;
                    btnUpdateTutorCourse.Visible = true;
                }

                lblWelcome.Text = "Welcome  " + Session["RealName"].ToString() + "   to your account";

                // Get value from the query string
                string update = Request.QueryString["UpdateSuccess"];

                // if query string value is not null 
                // display success label according to the value fetched
                if (update!=null && update.Equals("Course"))
                {
                    lblUpdateSuccess.Text = "Course updated successfully.";
                }
                if (update != null && update.Equals("Password"))
                {
                    lblUpdateSuccess.Text = " Password updated successfully.";
                }
            }
            
        }
    }
}