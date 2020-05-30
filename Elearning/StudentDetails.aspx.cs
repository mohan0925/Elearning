using Elearning.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Elearning
{
    public partial class StudentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblEmail.Text = "";
            // if no session direct to user login
            if (Session.Count == 0)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                Courses course = new Courses();
                if (!Page.IsPostBack)
                {
                    // passing parameter course id 
                    course.CourseID = Convert.ToInt32(Session["CourseID"].ToString());
                    // get course name
                    DataTable dt_course = course.getCourseName();
                    // if data is fetched append value
                    if (dt_course.Rows.Count > 0)
                    {
                        txtCourse.Text = dt_course.Rows[0]["CourseName"].ToString();
                    }
                    Users user = new Users();
                    user.CourseID = Convert.ToInt32(Session["CourseID"].ToString());
                    user.RoleID = 2;
                    // get all tutors 
                    DataTable dt_alltutors = user.getalltutors();
                    // if data is fetched append to listbox
                    if (dt_alltutors.Rows.Count > 0)
                    {
                        lstTutors.DataSource = dt_alltutors;
                        lstTutors.DataTextField = "RealName";
                        lstTutors.DataValueField = "UserID";
                        lstTutors.DataBind();
                    }
                    else
                    {
                        lblError.Text = "No tutors are present in this course.";
                    }
                    Modules modules = new Modules();
                    modules.CourseId = Int32.Parse(Session["CourseID"].ToString());
                    // get all modules of the course sent as parameter
                    DataTable dt_allmodules = modules.getallModules();
                    // if data is present then append to the repeater
                    if (dt_allmodules.Rows.Count > 0)
                    {
                        rptModules.DataSource = dt_allmodules;
                        rptModules.DataBind();
                    }
                    else
                    {
                        lblError.Text = "Database connection error.";
                    }
                }
            }

        }
        protected void btnShowEmail_Click(object sender, EventArgs e)
        {
            // condition to check tutor not selected
            if (lstTutors.SelectedIndex == -1)
            {
                lblError.Text = "You must select a Tutor to view email.";
            }
            else
            {
                // fetching the details of the tutor
                string curItem = lstTutors.SelectedItem.ToString();
                string curItem_val = lstTutors.SelectedValue.ToString();

                Users users = new Users();
                int UserID = Convert.ToInt32(curItem_val.ToString());
                users.UserID = UserID;
                DataTable dt_TutorEmail = users.getTutorEmail();
                if (dt_TutorEmail.Rows.Count > 0)
                {
                    lblEmail.Text = dt_TutorEmail.Rows[0]["EmailAddress"].ToString();
                }
            }
        }
    }
}