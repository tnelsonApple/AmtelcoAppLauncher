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
    public partial class AppAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
               
            }
            else
            {
                refreshExes();
            }


            populateInfinityStations();
        }

        public void refreshExes()
        {
            AppSettings appSettings = new AppSettings();

            string siteCode = ConfigurationManager.AppSettings["siteCode"];

            appSettings.populate(siteCode);

            txtDisplayTelephoneAgentExe.Text = appSettings._telephoneAgentExe;
            txtDisplaySupervisorExe.Text = appSettings._supervisorExe;
            txtDisplayOCSupervisorExe.Text = appSettings._ocSupervisorExe;
            txtDisplayISSupervisorExe.Text = appSettings._isSupervisorExe;

            txtTelephoneAgentExe.Text = appSettings._telephoneAgentExe;
            txtSupervisorExe.Text = appSettings._supervisorExe;
            txtOCSupervisorExe.Text = appSettings._ocSupervisorExe;
            txtISSupervisorExe.Text = appSettings._isSupervisorExe;

            //FileUpload1.
        }

        protected void btnSaveExes_Click(object sender, EventArgs e)
        {
            AppSettings appSettings = new AppSettings();

            //txtTelephoneAgentExe.

            //string temp = 

            if (txtTelephoneAgentExe.Text != null && txtTelephoneAgentExe.Text != "")
            {
                appSettings.updateTAExe(ConfigurationManager.AppSettings["siteCode"], txtTelephoneAgentExe.Text);
            }

            if (txtSupervisorExe.Text != null && txtSupervisorExe.Text != "")
            {
                appSettings.updateSupExe(ConfigurationManager.AppSettings["siteCode"], txtSupervisorExe.Text);
            }

            if (txtOCSupervisorExe.Text != null && txtOCSupervisorExe.Text != "")
            {
                appSettings.updateOCSupExe(ConfigurationManager.AppSettings["siteCode"], txtOCSupervisorExe.Text);
            }

            if (txtISSupervisorExe.Text != null && txtISSupervisorExe.Text != "")
            {
                appSettings.updateISSupExe(ConfigurationManager.AppSettings["siteCode"], txtISSupervisorExe.Text);
            }

            //appSettings.updateExes(ConfigurationManager.AppSettings["siteCode"], txtTelephoneAgentExe.FileName, txtSupervisorExe.FileName, txtOCSupervisorExe.FileName, txtISSupervisorExe.FileName);

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Executable file locations updated successfully.')", true);

            refreshExes();
        }

        protected void lbOfficeAudioGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            OfficeAudioGroup oag = new OfficeAudioGroup();
            oag.populate(Convert.ToInt32(lbOfficeAudioGroups.SelectedValue));

            txtDisplayGroupName.Text = oag._name;
            txtDisplayDefaultAudioInfo.Text = oag._defaultAudioInfo.Replace("<br />", "\n");
            cbDisplayStaticStations.Checked = oag._staticStations;

            btnEditOfficeAudioGroup.Enabled = true;
            btnDeleteOfficeAudioGroup.Enabled = true;
        }

        protected void btnAddOfficeAudioGroup_Click(object sender, EventArgs e)
        {
            hfAddorEditGroup.Value = "Add";
            hfCurrentGroup.Value = "0";

            txtGroupName.Text = "";
            txtDefaultAudioInfo.Text = "1.  Plug in your headset.";
            txtDefaultAudioInfo.Text += Environment.NewLine + "2.  Turn your dialpad on.";
            txtDefaultAudioInfo.Text += Environment.NewLine + "3.  Dial '###'";
            txtDefaultAudioInfo.Text += Environment.NewLine + "4.  You should hear a recording confirming the operator audio connection.";
            txtDefaultAudioInfo.Text += Environment.NewLine + "5.  Click 'Launch Infinity Telephone Agent'.";
            txtDefaultAudioInfo.Text += Environment.NewLine + "6.  Login with your assigned username and password.";
            txtDefaultAudioInfo.Text += Environment.NewLine + "7.  Done!  You're ready to turn on and take calls.";

            cbStaticStations.Checked = false;

            mpeEditOfficeAudioGroups.Show();
        }

        protected void btnEditOfficeAudioGroup_Click(object sender, EventArgs e)
        {
            hfAddorEditGroup.Value = "Edit";
            hfCurrentGroup.Value = lbOfficeAudioGroups.SelectedValue;

            OfficeAudioGroup oag = new OfficeAudioGroup();
            oag.populate(Convert.ToInt32(lbOfficeAudioGroups.SelectedValue));

            txtGroupName.Text = oag._name;
            txtDefaultAudioInfo.Text = oag._defaultAudioInfo.Replace("<br />", "\n"); ;
            cbStaticStations.Checked = oag._staticStations;

            mpeEditOfficeAudioGroups.Show();
        }

        protected void btnDeleteOfficeAudioGroup_Click(object sender, EventArgs e)
        {
            OfficeAudioGroup oag = new OfficeAudioGroup();
            oag.delete(Convert.ToInt32(lbOfficeAudioGroups.SelectedValue));

            lbOfficeAudioGroups.SelectedIndex = -1;
            lbOfficeAudioGroups.DataBind();

            btnEditOfficeAudioGroup.Enabled = false;
            btnDeleteOfficeAudioGroup.Enabled = false;

            txtDisplayGroupName.Text = "";
            txtDisplayDefaultAudioInfo.Text = "";
            cbDisplayStaticStations.Checked = false;
        }

        protected void btnAddEditGroupsSave_Click(object sender, EventArgs e)
        {
            OfficeAudioGroup oag = new OfficeAudioGroup();

            if (hfAddorEditGroup.Value == "Edit")
            {
                oag.updateDetails(Convert.ToInt32(hfCurrentGroup.Value), txtGroupName.Text, txtDefaultAudioInfo.Text, cbStaticStations.Checked);

            }
            else
            {
                oag.insertNew(ConfigurationManager.AppSettings["siteCode"], txtGroupName.Text, txtDefaultAudioInfo.Text, cbStaticStations.Checked);

            }

            txtDisplayGroupName.Text = "";
            txtDisplayDefaultAudioInfo.Text = "";
            cbDisplayStaticStations.Checked = false;

            lbOfficeAudioGroups.SelectedIndex = -1;
            lbOfficeAudioGroups.DataBind();
            
            btnEditOfficeAudioGroup.Enabled = false;
            btnDeleteOfficeAudioGroup.Enabled = false;
        }

        protected void rblInfinityStations_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                InfinityStation infStation = new InfinityStation();
                infStation.populate(Convert.ToInt32(rb.ID));

                hfStationID.Value = rb.ID;
                txtStationNumber.Text = infStation._stationNum.ToString();
                ddlStationType.SelectedValue = infStation._stationType;
                txtInfStationAudioInfo.Text = "";

                if (infStation._stationType == "TelephoneAgent")
                {
                    divTelephoneAgentDetails.Visible = true;
                    if (infStation._officeAudioID != 0)
                    {
                        ddlOfficeAudioGroups.DataBind();
                        ddlOfficeAudioGroups.SelectedValue = infStation._officeAudioID.ToString();
                        txtInfStationAudioInfo.Text = infStation._audioInfo.Replace("<br />", "\n");

                        OfficeAudioGroup oag = new OfficeAudioGroup();
                        oag.populate(Convert.ToInt32(ddlOfficeAudioGroups.SelectedValue));

                        if (oag._staticStations)
                        {
                            divDisplayWorkstationName.Visible = true;
                            txtInfWorkstationName.Text = infStation._workstationName;
                        }
                        else
                        {
                            divDisplayWorkstationName.Visible = false;
                            txtInfWorkstationName.Text = "";
                        }
                    }
                    else
                    {
                        ddlOfficeAudioGroups.SelectedIndex = -1;
                    }
                }
                else
                {
                    divTelephoneAgentDetails.Visible = false;
                }

                mpeEditInfinityStation.Show();
            }
        }

        public void populateInfinityStations()
        {
            Table stationsTable = new Table();
            TableRow tr = new TableRow();

            //stationsTable.Width = Unit.Percentage(100);

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = "SELECT id, stationNum, stationType FROM tblInfinityStations WHERE siteCode = @siteCode";

            SqlParameter p1 = new SqlParameter("@siteCode", ConfigurationManager.AppSettings["siteCode"]);

            sqlCmd.Parameters.Add(p1);

            SqlDataReader myReader = sqlCmd.ExecuteReader();
            int count = 0;

            while (myReader.Read())
            {
                int id = myReader.GetInt32(0);
                int stationNum = myReader.GetInt32(1);
                string stationType = myReader.GetString(2);
                string stationNumText = "";

                if (stationNum < 10)
                {
                    stationNumText = "00" + stationNum;
                }
                else if (stationNum < 100)
                {
                    stationNumText = "0" + stationNum;
                }
                else
                {
                    stationNumText = stationNum.ToString();
                }

                TableCell tc = new TableCell();
                RadioButton rb1 = new RadioButton();
                rb1.Text = stationNumText;
                rb1.ID = id.ToString();
                rb1.GroupName = "InfStations";
                rb1.AutoPostBack = true;
                //rb1.Attributes.Add("OnSelectedIndexChange", "rblInfinityStations_SelectedIndexChanged");
                rb1.CheckedChanged += new EventHandler(rblInfinityStations_SelectedIndexChanged);

                switch (stationType)
                { 
                    case "Unassigned":
                        rb1.BackColor = System.Drawing.Color.Gray;
                        rb1.ForeColor = System.Drawing.Color.White;
                        break;
                    case "TelephoneAgent":
                        rb1.BackColor = System.Drawing.Color.Yellow;
                        rb1.ForeColor = System.Drawing.Color.Black;
                        break;
                    case "Supervisor":
                        rb1.BackColor = System.Drawing.Color.Red;
                        rb1.ForeColor = System.Drawing.Color.White;
                        break;
                    case "OCSupervisor":
                        rb1.BackColor = System.Drawing.Color.Blue;
                        rb1.ForeColor = System.Drawing.Color.White;
                        break;
                    case "ISSupervisor":
                        rb1.BackColor = System.Drawing.Color.Green;
                        rb1.ForeColor = System.Drawing.Color.White;
                        break;
                    default:
                        break;
                }

                tc.Controls.Add(rb1);

                if (count % 20 == 0)
                {
                    stationsTable.Rows.Add(tr);
                    tr = new TableRow();
                }

                tr.Cells.Add(tc);
                count++;
            }

            myReader.Close();

            sqlConn.Close();
            sqlConn.Dispose();

            stationsTable.Rows.Add(tr);

            phInfinityStations.Controls.Add(stationsTable);
        }

        protected void btnSaveInfinityStation_Click(object sender, EventArgs e)
        {
            InfinityStation infStation = new InfinityStation();
            infStation.populate(Convert.ToInt32(hfStationID.Value));

            if (ddlStationType.SelectedValue == "TelephoneAgent")
            {
                string workstationName = "Station " + infStation._stationNum;

                if (txtInfWorkstationName.Text != null && txtInfWorkstationName.Text != "")
                {
                    workstationName = txtInfWorkstationName.Text;
                }

                infStation.update(Convert.ToInt32(hfStationID.Value), ddlStationType.SelectedValue, Convert.ToInt32(ddlOfficeAudioGroups.SelectedValue), txtInfStationAudioInfo.Text, workstationName);
            }
            else
            {
                infStation.update(Convert.ToInt32(hfStationID.Value), ddlStationType.SelectedValue, 0, "", "");
            }
            
            phInfinityStations.Controls.Clear();
            populateInfinityStations();
        }

        protected void ddlStationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStationType.SelectedValue == "TelephoneAgent")
            {
                divTelephoneAgentDetails.Visible = true;
                ddlOfficeAudioGroups.Items.Clear();
                ddlOfficeAudioGroups.AppendDataBoundItems = true;
                ddlOfficeAudioGroups.Items.Add(new ListItem("-- Choose Office / Audio Group --", ""));
                ddlOfficeAudioGroups.DataBind();
                ddlOfficeAudioGroups.SelectedIndex = -1;
            }
            else
            {
                divTelephoneAgentDetails.Visible = false;
            }

            mpeEditInfinityStation.Show();
        }

        protected void ddlOfficeAudioGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOfficeAudioGroups.SelectedValue != "")
            {
                OfficeAudioGroup oag = new OfficeAudioGroup();
                oag.populate(Convert.ToInt32(ddlOfficeAudioGroups.SelectedValue));

                txtInfStationAudioInfo.Text = oag._defaultAudioInfo.Replace("<br />", "\n");

                if (oag._staticStations)
                {
                    divDisplayWorkstationName.Visible = true;
                }
                else
                {
                    divDisplayWorkstationName.Visible = false;
                }
            }

            mpeEditInfinityStation.Show();
        }

        protected void btnAssignMultipleStations_Click(object sender, EventArgs e)
        {
            phMultipleStations.Visible = false;
            ddlMultipleStationType.SelectedIndex = -1;
            ddlMultipleOfficeAudioGroup.SelectedIndex = -1;
            divMultipleTelephoneAgent.Visible = false;
            //populateMultipleStations("TelephoneAgent");
            mpeAssignMultipleStations.Show();
        }

        protected void btnSaveMultiple_Click(object sender, EventArgs e)
        {
            int count = 0;
            string stationNums = "";

            for (int i = 1; i <= 200; i++ )
            {
                CheckBox cb = (CheckBox)phMultipleStations.FindControl("CheckBox" + i);
                if (cb.Checked)
                {
                    if (count == 0)
                    {
                        stationNums = i.ToString();
                    }
                    else
                    {
                        stationNums += ", " + i;
                    }
                    count++;
                }
            }

            InfinityStation infStations = new InfinityStation();

            if (ddlMultipleStationType.SelectedValue == "TelephoneAgent")
            {
                OfficeAudioGroup oag = new OfficeAudioGroup();
                oag.populate(Convert.ToInt32(ddlMultipleOfficeAudioGroup.SelectedValue));

                infStations.updateMultiple(stationNums, ddlMultipleStationType.SelectedValue, Convert.ToInt32(ddlMultipleOfficeAudioGroup.SelectedValue), oag._defaultAudioInfo);
            }
            else
            {
                infStations.updateMultiple(stationNums, ddlMultipleStationType.SelectedValue, 0, "");
            }

            phInfinityStations.Controls.Clear();
            populateInfinityStations();
        }

        protected void ddlMultipleStationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpeAssignMultipleStations.Show();
            if (ddlMultipleStationType.SelectedValue == "TelephoneAgent")
            {
                divMultipleTelephoneAgent.Visible = true;
                phMultipleStations.Controls.Clear();
            }
            else
            {
                divMultipleTelephoneAgent.Visible = false;
                if (ddlMultipleStationType.SelectedValue != "")
                {
                    populateMultipleStations(ddlMultipleStationType.SelectedValue);
                }
                else
                {
                    phMultipleStations.Controls.Clear();
                }
            }

            /*
            if (ddlMultipleStationType.SelectedIndex > 0)
            {
                switch (ddlMultipleStationType.SelectedValue)
                { 
                    case "TelephoneAgent":
                        ddlMultipleStationType.BackColor = System.Drawing.Color.Yellow;
                        ddlMultipleStationType.ForeColor = System.Drawing.Color.White;
                        break;
                    case "Supervisor":
                        ddlMultipleStationType.BackColor = System.Drawing.Color.Red;
                        break;
                    case "OCSupervisor":
                        ddlMultipleStationType.BackColor = System.Drawing.Color.Blue;
                        break;
                    case "ISSupervisor":
                        ddlMultipleStationType.BackColor = System.Drawing.Color.Green;
                        break;
                }
            }
            */
        }

        protected void ddlMultipleOfficeAudioGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpeAssignMultipleStations.Show();
            if (ddlMultipleOfficeAudioGroup.SelectedValue != "")
            {
                populateMultipleStations("TelephoneAgent");
            }
            else
            {
                phMultipleStations.Controls.Clear();
            }
        }

        public void populateMultipleStations(string selectedStationType)
        {
            phMultipleStations.Visible = true;
            //phMultipleStations.Controls.Clear();
            
            //Table multipleStationsTable = new Table();
            //TableRow multipleTr = new TableRow();
            //stationsTable.Width = Unit.Percentage(100);
            
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = "SELECT id, stationNum, stationType, officeAudioID FROM tblInfinityStations WHERE siteCode = @siteCode";

            SqlParameter p1 = new SqlParameter("@siteCode", ConfigurationManager.AppSettings["siteCode"]);

            sqlCmd.Parameters.Add(p1);

            SqlDataReader myReader = sqlCmd.ExecuteReader();
            int count = 0;

            while (myReader.Read())
            {
                int id = myReader.GetInt32(0);
                int stationNum = myReader.GetInt32(1);
                string stationType = myReader.GetString(2);
                int officeAudioGroup = myReader.GetInt32(3);
                string stationNumText = "";

                if (stationNum < 10)
                {
                    stationNumText = "00" + stationNum;
                }
                else if (stationNum < 100)
                {
                    stationNumText = "0" + stationNum;
                }
                else
                {
                    stationNumText = stationNum.ToString();
                }

                CheckBox cb1 = new CheckBox();
                cb1 = (CheckBox)phMultipleStations.FindControl("CheckBox" + (count + 1));

                //TableCell multipleTc = new TableCell();
                //CheckBox cb1 = new CheckBox();
                
                //cb1.ID = "multi:" + id.ToString();
                //cb1.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                //cb1.Enabled = false;
                cb1.Checked = false;
                cb1.Enabled = false;
                cb1.Text = "N/A";

                if (stationType == "Unassigned")
                {
                    cb1.Enabled = true;
                    cb1.Text = stationNumText;
                }
                else if (stationType == selectedStationType)
                {
                    if (selectedStationType == "TelephoneAgent")
                    {
                        if (officeAudioGroup == Convert.ToInt32(ddlMultipleOfficeAudioGroup.SelectedValue))
                        {
                            cb1.Enabled = true;
                            cb1.Checked = true;
                            cb1.Text = stationNumText;
                        }
                    }
                    else
                    {
                        cb1.Enabled = true;
                        cb1.Checked = true;
                        cb1.Text = stationNumText;
                    }
                }

                //rb1.AutoPostBack = true;
                //rb1.Attributes.Add("OnSelectedIndexChange", "rblInfinityStations_SelectedIndexChanged");
                //rb1.CheckedChanged += new EventHandler(rblInfinityStations_SelectedIndexChanged);
                
                switch (stationType)
                {
                    case "Unassigned":
                        cb1.BackColor = System.Drawing.Color.Gray;
                        cb1.ForeColor = System.Drawing.Color.White;
                        break;
                    case "TelephoneAgent":
                        cb1.BackColor = System.Drawing.Color.Yellow;
                        cb1.ForeColor = System.Drawing.Color.Black;
                        break;
                    case "Supervisor":
                        cb1.BackColor = System.Drawing.Color.Red;
                        cb1.ForeColor = System.Drawing.Color.White;
                        break;
                    case "OCSupervisor":
                        cb1.BackColor = System.Drawing.Color.Blue;
                        cb1.ForeColor = System.Drawing.Color.White;
                        break;
                    case "ISSupervisor":
                        cb1.BackColor = System.Drawing.Color.Green;
                        cb1.ForeColor = System.Drawing.Color.White;
                        break;
                    default:
                        break;
                }
                /*
                multipleTc.Controls.Add(cb1);

                if (count % 20 == 0)
                {
                    multipleStationsTable.Rows.Add(multipleTr);
                    multipleTr = new TableRow();
                }

                multipleTr.Cells.Add(multipleTc);
                 * */
                count++;
            }

            myReader.Close();

            sqlConn.Close();
            sqlConn.Dispose();
            
            //multipleStationsTable.Rows.Add(multipleTr);

            //phMultipleStations.Controls.Add(multipleStationsTable);
        
        }

        public static Control FindChildControl(Control start, string id)
        {
            if (start != null)
            {
                Control foundControl;
                foundControl = start.FindControl(id);


                if (foundControl != null)
                {
                    return foundControl;
                }


                foreach (Control c in start.Controls)
                {
                    foundControl = FindChildControl(c, id);
                    if (foundControl != null)
                    {
                        return foundControl;
                    }
                }
            }
            return null;
        }

        protected void btnSaveEditISStation_Click(object sender, EventArgs e)
        {
            ISStation stn = new ISStation();

            if (hfISAddorEdit.Value == "Edit")
            {
                stn.update(Convert.ToInt32(lbISStations.SelectedValue), txtISProfileName.Text, Convert.ToInt32(txtISStationNum.Text));
            }
            else
            {
                stn.insertNew(ConfigurationManager.AppSettings["siteCode"], txtISProfileName.Text, Convert.ToInt32(txtISStationNum.Text));
            }

            lbISStations.DataBind();
            btnEditISStation.Enabled = false;
            btnDeleteISStation.Enabled = false;
        }

        protected void btnAddISStation_Click(object sender, EventArgs e)
        {
            txtISProfileName.Text = "";
            txtISStationNum.Text = "";
            hfISAddorEdit.Value = "Add";
            mpeEditISStations.Show();
        }

        protected void btnEditISStation_Click(object sender, EventArgs e)
        {
            hfISAddorEdit.Value = "Edit";
            ISStation stn = new ISStation();
            stn.populate(Convert.ToInt32(lbISStations.SelectedValue));

            txtISProfileName.Text = stn._profileName;
            txtISStationNum.Text = stn._stationNum.ToString();
            mpeEditISStations.Show();
        }

        protected void btnDeleteISStation_Click(object sender, EventArgs e)
        {
            ISStation stn = new ISStation();
            stn.delete(Convert.ToInt32(lbISStations.SelectedValue));

            lbISStations.DataBind();
            btnEditISStation.Enabled = false;
            btnDeleteISStation.Enabled = false;
        }

        protected void ddlISStations_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnEditISStation.Enabled = true;
            btnDeleteISStation.Enabled = true;
        }
    }
}