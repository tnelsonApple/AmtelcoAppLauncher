using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;

namespace AmtelcoAppLauncher
{
    public partial class _Default : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                string test = "";
                test = "";

            }
            else
            {
                UserInfo sessionUser = (UserInfo)Session["userInfo"];
                AppSettings appSettings = new AppSettings();

                string siteCode = ConfigurationManager.AppSettings["siteCode"];

                appSettings.populate(siteCode);

                if (sessionUser._telephoneAgent)
                {
                    apTelephoneAgent.Visible = true;
                    Accordion1.SelectedIndex = 0;
                }

                if (sessionUser._supervisor)
                { apSupervisor.Visible = true; }

                if (sessionUser._ocSupervisor)
                { apOCSupervisor.Visible = true; }

                if (sessionUser._isSupervisor)
                { apISSupervisor.Visible = true; }
            }
        }

        protected void ddlOfficeAudioGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOfficeAudioGroup.SelectedValue != "")
            {
                OfficeAudioGroup oag = new OfficeAudioGroup();
                oag.populate(Convert.ToInt32(ddlOfficeAudioGroup.SelectedValue));

                if (oag._staticStations)
                {
                    divDynamicInfStations.Visible = false;
                    divStaticInfStations.Visible = true;
                    ddlChooseStaticStation.DataBind();
                }
                else
                {
                    divStaticInfStations.Visible = false;
                    divDynamicInfStations.Visible = true;
                }
            }
            else
            {
                divStaticInfStations.Visible = false;
                divDynamicInfStations.Visible = false;
            }

            divShowInfStationLaunch.Visible = false;
            divGetNewTAStation.Visible = false;
        }

        protected void ddlChooseStaticStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChooseStaticStation.SelectedValue != "")
            {
                InfinityStation stn = new InfinityStation();
                UserInfo sessionUser = (UserInfo)Session["userInfo"];

                int stationID = Convert.ToInt32(ddlChooseStaticStation.SelectedValue);
                int stationNum = 0;

                stn.populate(stationID);
                stationNum = stn._stationNum;

                divShowInfStationLaunch.Visible = true;
                lblInfStationNum.Text = "Infinity Station / Port #" + stationNum;
                lblAudioInfo.Text = stn._audioInfo.Replace(Environment.NewLine, "<br />");

                btnLaunchInfinity.CommandName = sessionUser._username;
                btnLaunchInfinity.CommandArgument = stationNum.ToString();

                //btnLaunchInfinity.OnClientClick = "LaunchTelephoneAgent(" + stationNum + "); this.enabled=false";
                divDynamicInfStations.Visible = false;
                btnLaunchInfinity.Text = "Launch Infinity Telephone Agent";
                btnLaunchInfinity.Enabled = true;
            }
        }

        protected void btnGetDynamicInfStation_Click(object sender, EventArgs e)
        {
            InfinityStation stn = new InfinityStation();
            UserInfo sessionUser = (UserInfo)Session["userInfo"];

            int stationID = stn.getDynamicStation("TelephoneAgent", Convert.ToInt32(ddlOfficeAudioGroup.SelectedValue), sessionUser._username);
            int stationNum = 0;

            stn.populate(stationID);
            stationNum = stn._stationNum;

            divShowInfStationLaunch.Visible = true;
            lblInfStationNum.Text = "Infinity Station / Port #" + stationNum;
            lblAudioInfo.Text = stn._audioInfo.Replace(Environment.NewLine, "<br />");

            btnLaunchInfinity.CommandName = sessionUser._username;
            btnLaunchInfinity.CommandArgument = stationNum.ToString();

            //btnLaunchInfinity.OnClientClick = "LaunchTelephoneAgent(" + stationNum + "); this.enabled=false";
            divDynamicInfStations.Visible = false;
            btnLaunchInfinity.Text = "Launch Infinity Telephone Agent";
            btnLaunchInfinity.Enabled = true;
        }

        protected void btnLaunchInfinity_Click(object sender, EventArgs e)
        {
            string stationNum = ((Button)sender).CommandArgument;
            string username = ((Button)sender).CommandName;

            InfinityStation stn = new InfinityStation();
            AppSettings appSettings = new AppSettings();
            Log logEntry = new Log();

            string siteCode = ConfigurationManager.AppSettings["siteCode"];

            appSettings.populate(siteCode);
            //string replace = @"\\";
            string exe = appSettings._telephoneAgentExe.Replace(@"\", @"\\");

            hfInfstation.Value = stationNum;

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Launch", "LaunchInfApp('" + stationNum + "', '" + exe + "')", true);

            btnLaunchInfinity.Text = "Processing.  Please wait...";
            btnLaunchInfinity.Enabled = false;

            //stn.updateReserved(siteCode, Convert.ToInt32(stationNum), username);
            logEntry.insertNew(siteCode, "Information", "TelephoneAgent", Convert.ToInt32(stationNum), username, "User launched application.");

            //divShowInfStationLaunch.Visible = false;
            ddlOfficeAudioGroup.Enabled = false;
            ddlChooseStaticStation.Enabled = false;
            divGetNewTAStation.Visible = true;
        }

        protected void btnGetNewTelephoneAgent_Click(object sender, EventArgs e)
        {
            hfStationType.Value = "TelephoneAgent";
            hfStationNum.Value = hfInfstation.Value;

            txtInfErrorMessage.Text = "";

            mpeGetNewInfStation.Show();
        }

        protected void btnGetNewInfOK_Click(object sender, EventArgs e)
        {
            string siteCode = ConfigurationManager.AppSettings["siteCode"];
            UserInfo sessionUser = (UserInfo)Session["userInfo"];
            InfinityStation stn = new InfinityStation();
            
            ddlOfficeAudioGroup.SelectedIndex = -1;
            Log logEntry = new Log();

            if (txtInfErrorMessage.Text != "")
            {
                logEntry.insertNew(siteCode, "Error", hfStationType.Value, Convert.ToInt32(hfStationNum.Value), sessionUser._username, "User chose new station number.  Error Message: " + txtInfErrorMessage.Text);
            }
            else
            {
                logEntry.insertNew(siteCode, "Information", hfStationType.Value, Convert.ToInt32(hfStationNum.Value), sessionUser._username, "User chose new station number.  No Error message entered.");
            }

            

            if (hfStationType.Value == "TelephoneAgent")
            {
                stn.updateReserved(siteCode, Convert.ToInt32(hfStationNum.Value), "Released");
                divGetNewTAStation.Visible = false;
                divShowInfStationLaunch.Visible = false;
                ddlChooseStaticStation.Enabled = true;
                ddlOfficeAudioGroup.Enabled = true;
            }
            else if (hfStationType.Value == "Supervisor")
            {
                stn.updateReserved(siteCode, Convert.ToInt32(hfStationNum.Value), "Released");
                divGetNewSupStation.Visible = false;
                divGetSupStation.Visible = true;
                divLaunchSupStation.Visible = false;
            }
            else if (hfStationType.Value == "OCSupervisor")
            {
                stn.updateReserved(siteCode, Convert.ToInt32(hfStationNum.Value), "Released");
                divGetNewOCSupStation.Visible = false;
                divGetOCSupStation.Visible = true;
                divLaunchOCSupStation.Visible = false;
            }
            else if (hfStationType.Value == "ISSupervisor")
            {
                ISStation isStation = new ISStation();
                isStation.updateReserved(siteCode, Convert.ToInt32(hfStationNum.Value), "Released");

                divGetNewISSupStation.Visible = false;
                divGetISSupStation.Visible = true;
                divLaunchISSupStation.Visible = false;
            }
        }

        protected void btnGetSupStation_Click(object sender, EventArgs e)
        {
            InfinityStation stn = new InfinityStation();
            UserInfo sessionUser = (UserInfo)Session["userInfo"];

            int stationID = stn.getDynamicStation("Supervisor", 0, sessionUser._username);
            int stationNum = 0;

            stn.populate(stationID);
            stationNum = stn._stationNum;

            divLaunchSupStation.Visible = true;
            lblSupStationNum.Text = "Infinity Station / Port #" + stationNum;
            
            btnLaunchSup.CommandName = sessionUser._username;
            btnLaunchSup.CommandArgument = stationNum.ToString();

            //btnLaunchInfinity.OnClientClick = "LaunchTelephoneAgent(" + stationNum + "); this.enabled=false";
            divGetSupStation.Visible = false;

            btnLaunchSup.Text = "Launch Infinity Supervisor";
            btnLaunchSup.Enabled = true;
        }

        protected void btnGetOCSupStation_Click(object sender, EventArgs e)
        {
            InfinityStation stn = new InfinityStation();
            UserInfo sessionUser = (UserInfo)Session["userInfo"];

            int stationID = stn.getDynamicStation("OCSupervisor", 0, sessionUser._username);
            int stationNum = 0;

            stn.populate(stationID);
            stationNum = stn._stationNum;

            divLaunchOCSupStation.Visible = true;
            lblOCSupStationNum.Text = "Infinity Station / Port #" + stationNum;

            btnLaunchOCSup.CommandName = sessionUser._username;
            btnLaunchOCSup.CommandArgument = stationNum.ToString();

            //btnLaunchInfinity.OnClientClick = "LaunchTelephoneAgent(" + stationNum + "); this.enabled=false";
            divGetOCSupStation.Visible = false;

            btnLaunchOCSup.Text = "Launch On-Call Supervisor";
            btnLaunchOCSup.Enabled = true;
        }

        protected void btnGetISSupStation_Click(object sender, EventArgs e)
        {
            ISStation stn = new ISStation();
            UserInfo sessionUser = (UserInfo)Session["userInfo"];

            int stationID = stn.getDynamicStation("ISSupervisor", 0, sessionUser._username);
            string profileName = "";

            stn.populate(stationID);
            profileName = stn._profileName;

            divLaunchISSupStation.Visible = true;
            lblISSupStationNum.Text = "Profile: " + profileName + " (" + stn._stationNum + ")"; ;

            btnLaunchISSup.CommandName = sessionUser._username;
            btnLaunchISSup.CommandArgument = stationID.ToString();

            //btnLaunchInfinity.OnClientClick = "LaunchTelephoneAgent(" + stationNum + "); this.enabled=false";
            divGetISSupStation.Visible = false;

            btnLaunchISSup.Text = "Launch IS Supervisor";
            btnLaunchISSup.Enabled = true;
        }

        protected void btnLaunchSup_Click(object sender, EventArgs e)
        {
            string stationNum = ((Button)sender).CommandArgument;
            string username = ((Button)sender).CommandName;

            InfinityStation stn = new InfinityStation();
            AppSettings appSettings = new AppSettings();
            Log logEntry = new Log();

            string siteCode = ConfigurationManager.AppSettings["siteCode"];

            appSettings.populate(siteCode);
            //string replace = @"\\";
            string exe = appSettings._supervisorExe.Replace(@"\", @"\\");

            hfInfstation.Value = stationNum;

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Launch", "LaunchInfApp('" + stationNum + "', '" + exe + "')", true);

            btnLaunchSup.Text = "Processing.  Please wait...";
            btnLaunchSup.Enabled = false;

            //stn.updateReserved(siteCode, Convert.ToInt32(stationNum), username);
            logEntry.insertNew(siteCode, "Information", "Supervisor", Convert.ToInt32(stationNum), username, "User launched application.");

            //divShowInfStationLaunch.Visible = false;
            divGetNewSupStation.Visible = true;
        }

        protected void btnLaunchOCSup_Click(object sender, EventArgs e)
        {
            string stationNum = ((Button)sender).CommandArgument;
            string username = ((Button)sender).CommandName;

            InfinityStation stn = new InfinityStation();
            AppSettings appSettings = new AppSettings();
            Log logEntry = new Log();

            string siteCode = ConfigurationManager.AppSettings["siteCode"];

            appSettings.populate(siteCode);
            //string replace = @"\\";
            string exe = appSettings._ocSupervisorExe.Replace(@"\", @"\\");

            hfInfstation.Value = stationNum;

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Launch", "LaunchInfApp('" + stationNum + "', '" + exe + "')", true);

            btnLaunchOCSup.Text = "Processing.  Please wait...";
            btnLaunchOCSup.Enabled = false;

            //stn.updateReserved(siteCode, Convert.ToInt32(stationNum), username);
            logEntry.insertNew(siteCode, "Information", "OCSupervisor", Convert.ToInt32(stationNum), username, "User launched application.");

            //divShowInfStationLaunch.Visible = false;
            divGetNewOCSupStation.Visible = true;
        }

        protected void btnLaunchISSup_Click(object sender, EventArgs e)
        {
            int stationID = Convert.ToInt32(((Button)sender).CommandArgument);
            string username = ((Button)sender).CommandName;

            ISStation stn = new ISStation();
            AppSettings appSettings = new AppSettings();
            Log logEntry = new Log();

            stn.populate(Convert.ToInt32(stationID));

            string siteCode = ConfigurationManager.AppSettings["siteCode"];

            appSettings.populate(siteCode);
            //string replace = @"\\";
            string exe = appSettings._isSupervisorExe.Replace(@"\", @"\\");

            hfInfstation.Value = stn._stationNum.ToString();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Launch", "LaunchISSup('" + stn._profileName + "', '" + exe + "')", true);

            btnLaunchISSup.Text = "Processing.  Please wait...";
            btnLaunchISSup.Enabled = false;

            //stn.updateReserved(siteCode, Convert.ToInt32(stn._stationNum.ToString()), username);
            logEntry.insertNew(siteCode, "Information", "ISSupervisor", Convert.ToInt32(stn._stationNum.ToString()), username, "User launched application.");

            //divShowInfStationLaunch.Visible = false;
            divGetNewISSupStation.Visible = true;
        }

        protected void btnGetNewSup_Click(object sender, EventArgs e)
        {
            hfStationType.Value = "Supervisor";
            hfStationNum.Value = hfInfstation.Value;

            txtInfErrorMessage.Text = "";

            mpeGetNewInfStation.Show();
        }

        protected void btnGetNewOCSup_Click(object sender, EventArgs e)
        {
            hfStationType.Value = "OCSupervisor";
            hfStationNum.Value = hfInfstation.Value;

            txtInfErrorMessage.Text = "";

            mpeGetNewInfStation.Show();
        }

        protected void btnGetNewISSup_Click(object sender, EventArgs e)
        {
            hfStationType.Value = "ISSupervisor";
            hfStationNum.Value = hfInfstation.Value;

            txtInfErrorMessage.Text = "";

            mpeGetNewInfStation.Show();
        }
    }
}
