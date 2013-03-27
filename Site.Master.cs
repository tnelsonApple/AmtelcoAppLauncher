using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AmtelcoAppLauncher
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserInfo sessionUser = (UserInfo)Session["userInfo"];
            //UserInfo user = new UserInfo();

            string URL = Request.ServerVariables["URL"];

            if (!URL.Contains("Error.aspx"))
            {
                if (sessionUser == null)
                {
                    sessionUser = new UserInfo();

                    string username = Request.ServerVariables["LOGON_USER"];
                    string[] Split = username.Split(new Char[] { '\\' });

                    sessionUser.populateInfo(Split[1]);

                    if (sessionUser._username != null)
                    {
                        Session["userInfo"] = sessionUser;
                    }
                    else
                    {
                        Response.Redirect("~/Error.aspx?code=userNotFound");
                    }
                }

                lblUsername.Text = "Welcome <b>" + sessionUser._username + "</b>!";

                MenuItem mi1 = new MenuItem("Home", "Home", "", "~/Default.aspx");
                MenuItem mi2 = new MenuItem("User Profiles", "User Admin", "", "~/UserAdmin.aspx");
                MenuItem mi3 = new MenuItem("Application/Site Settings", "App Admin", "", "~/AppAdmin.aspx");
                //MenuItem mi4 = new MenuItem("Station Admininistration", "Station Admin", "", "~/StationAdmin.aspx");

                Menu navMenu = (Menu)FindControl("NavigationMenu");

                navMenu.Items.Add(mi1);

                if (sessionUser._userAdmin)
                {
                    navMenu.Items.Add(mi2);
                }

                if (sessionUser._appAdmin)
                {
                    navMenu.Items.Add(mi3);
                    //navMenu.Items.Add(mi4);
                }
            }
            
        }
    }
}
