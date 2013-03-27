using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AmtelcoAppLauncher
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["code"] == "userNotFound")
            {
                lblErrorMessage.Text = "Username not found.";
            }
            else
            {
                lblErrorMessage.Text = "An internal error has occurred.  Please close browswer and try again.";
            }

        }
    }
}