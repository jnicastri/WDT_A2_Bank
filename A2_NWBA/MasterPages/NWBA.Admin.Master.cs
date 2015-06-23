using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace A2_NWBA.MasterPages
{
    public partial class NWBA_Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserNameLtr.Text = (string)Session["UserNameTitle"];
            }
        }

        protected void LogOutLb_Click(object sender, EventArgs e)
        {
            Session.Remove("AdminSession");
            Session.Remove("UserNameTitle");
            Session.Remove("UserId");
            Response.Redirect("/Login");
        }
    }
}