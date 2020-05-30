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
    public partial class TutorDetails : System.Web.UI.Page
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
                    // fetching course name
                    DataTable dt_coursename = course.getCourseName();
                    // if data is fetched append to textbox
                    if (dt_coursename.Rows.Count > 0)
                    {
                        txtCourse.Text = dt_coursename.Rows[0]["CourseName"].ToString();
                    }
                    Users user = new Users();
                    user.RoleID = 1;
                    user.CourseID = Convert.ToInt32(Session["CourseID"].ToString());
                    // fetch all the details of the students
                    DataTable dt_allStudents = user.getallStudents();
                    // append data to the listbox
                    if (dt_allStudents.Rows.Count > 0)
                    {
                        lstStudents.DataSource = dt_allStudents;
                        lstStudents.DataTextField = "RealName";
                        lstStudents.DataValueField = "UserID";
                        lstStudents.DataBind();
                    }
                    else
                    {
                        lblError.Text = "No students in this course.";
                    }
                }
            }
        }
        // to load new data after student is removed 
        private void LoadStudents()
        {
            Courses course = new Courses();
            course.CourseID = Convert.ToInt32(Session["CourseID"].ToString());
            DataTable dt_coursename = course.getCourseName();
            if (dt_coursename.Rows.Count > 0)
            {
                txtCourse.Text = dt_coursename.Rows[0]["CourseName"].ToString();
            }
            Users user = new Users();
            user.RoleID = 1;
            user.CourseID = Convert.ToInt32(Session["CourseID"].ToString());
            DataTable dt_allStudents = user.getallStudents();
            if (dt_allStudents.Rows.Count > 0)
            {
                lstStudents.DataSource = dt_allStudents;
                lstStudents.DataTextField = "RealName";
                lstStudents.DataValueField = "UserID";
                lstStudents.DataBind();
            }
            else
            {
                lblError.Text = "No students in this course.";
            }

        }
        protected void btnRemoveStudent_Click(object sender, EventArgs e)
        {
            // condition to check if student is not selected
            if (lstStudents.SelectedIndex == -1)
            {
                lblError.Text = "No item selected in ListBox.";
            }
            else
            {
                // remove student on passing the details
                // user id is used as parameter
                string curItem = lstStudents.SelectedItem.ToString();
                string curItem_val = lstStudents.SelectedValue.ToString();
                int UserID = Convert.ToInt32(curItem_val.ToString());
                Users user = new Users();
                user.UserID = UserID;
                // removing user
                if (user.removeuser())
                {
                    lblSuccess.Text = "Student Successfully Removed";
                    LoadStudents();
                }
                else
                {
                    lblError.Text = "Database Connection Error!!";
                }
            }
        }
    }
}