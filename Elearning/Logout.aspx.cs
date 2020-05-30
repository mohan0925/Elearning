using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Elearning
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // if no session direct to user login
            if (Session.Count == 0)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
        }

        // On clicking yes button
        // all the sessions will be removed
        protected void btnYes_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("~/UserLogin.aspx");
        }
        // On clicking no button
        // all the direct to user account webpage
        protected void btnNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserAccount.aspx");
        }
    }
}