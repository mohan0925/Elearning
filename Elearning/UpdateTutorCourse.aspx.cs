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
    public partial class UpdateTutorCourse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // if no session direct to user login
            if (Session.Count == 0)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    Courses course = new Courses();
                    course.CourseID = Convert.ToInt32(Session["CourseID"].ToString());
                    // Fetch course name
                    DataTable dt_CourseName = course.getCourseName();
                    if (dt_CourseName.Rows.Count > 0)
                    {
                        txtCourse.Text = dt_CourseName.Rows[0]["CourseName"].ToString();
                    }

                    Courses course1 = new Courses();
                    // fetch all courses
                    DataTable dt_allcourses = course1.getallcourses();
                    if (dt_allcourses.Rows.Count > 0)
                    {
                        lstCourses.DataSource = dt_allcourses;
                        lstCourses.DataValueField = "CourseID";
                        lstCourses.DataTextField = "CourseName";
                        lstCourses.DataBind();
                    }
                }
            }
        }
        protected void btnUpdateCourse_Click(object sender, EventArgs e)
        {
            // condition to check course is selected
            if (lstCourses.SelectedIndex == -1)
            {
                lblError.Text = "Select a course.";
            }
            else
            {
                // update course on passing the course id
                Users user = new Users();
                string curItem_val = lstCourses.SelectedValue.ToString();
                user.CourseID = Convert.ToInt32(curItem_val.ToString());
                user.UserName = Session["UserName"].ToString();
                if (user.updateCourse())
                {
                    Session["CourseID"] = Convert.ToInt32(curItem_val.ToString());
                    lblError.Text = "Course Updated.";
                    Response.Redirect("~/UserAccount.aspx?UpdateSuccess=Course");
                }
                else
                {
                    lblError.Text = "Database connection error.";
                }
            }
        }
    }
}