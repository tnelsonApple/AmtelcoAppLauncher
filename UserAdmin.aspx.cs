using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AmtelcoAppLauncher
{
    public partial class UserAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbUsers.SelectedIndex != -1)
            {
                UserInfo sessionUser = (UserInfo)Session["userInfo"];
                UserInfo selectedUser = new UserInfo();

                selectedUser.populateInfo(lbUsers.SelectedValue);

                txtDisplayUsername.Text = selectedUser._username;
                cbDisplayUserAdmin.Checked = selectedUser._userAdmin;
                cbDisplayAppAdmin.Checked = selectedUser._appAdmin;
                cbDisplayTelephoneAgent.Checked = selectedUser._telephoneAgent;
                cbDisplaySupervisor.Checked = selectedUser._supervisor;
                cbDisplayOCSupervisor.Checked = selectedUser._ocSupervisor;
                cbDisplayISSupervisor.Checked = selectedUser._isSupervisor;

                if (sessionUser._username != selectedUser._username)
                {
                    if (sessionUser._appAdmin)
                    {
                        btnEdit.Enabled = true;
                        btnDelete.Enabled = true;
                    }
                    else if (sessionUser._userAdmin && !selectedUser._appAdmin)
                    {
                        btnEdit.Enabled = true;
                        btnDelete.Enabled = true;
                    }
                    else
                    {
                        btnEdit.Enabled = false;
                        btnDelete.Enabled = false;
                    }
                }
                else
                {
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
            else
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            UserInfo sessionUser = (UserInfo)Session["userInfo"];

            hfAddOrEdit.Value = "Add";
            txtUsername.Enabled = true;

            txtUsername.Text = "";
            cbTelephoneAgent.Checked = false;
            cbSupervisor.Checked = false;
            cbOCSupervisor.Checked = false;
            cbISSupervisor.Checked = false;
            if (!sessionUser._userAdmin)
            {
                cbUserAdmin.Enabled = false;
            }
            if (!sessionUser._appAdmin)
            {
                cbAppAdmin.Enabled = false;
            }
            cbUserAdmin.Checked = false;
            cbAppAdmin.Checked = false;

            mpeAddEditUser.Show();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            UserInfo sessionUser = (UserInfo)Session["userInfo"];

            hfAddOrEdit.Value = "Edit";
            txtUsername.Enabled = false;
            txtUsername.Text = txtDisplayUsername.Text;
            cbTelephoneAgent.Checked = cbDisplayTelephoneAgent.Checked;
            cbSupervisor.Checked = cbDisplaySupervisor.Checked;
            cbOCSupervisor.Checked = cbDisplayOCSupervisor.Checked;
            cbISSupervisor.Checked = cbDisplayISSupervisor.Checked;
            cbUserAdmin.Checked = cbDisplayUserAdmin.Checked;
            cbAppAdmin.Checked = cbDisplayAppAdmin.Checked;

            if (!sessionUser._userAdmin)
            {
                cbUserAdmin.Enabled = false;
            }
            if (!sessionUser._appAdmin)
            {
                cbAppAdmin.Enabled = false;
            }

            mpeAddEditUser.Show();
        }

        protected void btnAddEditSave_Click(object sender, EventArgs e)
        {
            UserInfo addEditUser = new UserInfo();

            if (hfAddOrEdit.Value == "Add")
            {
                addEditUser.addNew(txtUsername.Text, cbUserAdmin.Checked, cbAppAdmin.Checked, cbTelephoneAgent.Checked, cbSupervisor.Checked, cbOCSupervisor.Checked, cbISSupervisor.Checked);
            }
            else
            {
                addEditUser.editUser(txtUsername.Text, cbUserAdmin.Checked, cbAppAdmin.Checked, cbTelephoneAgent.Checked, cbSupervisor.Checked, cbOCSupervisor.Checked, cbISSupervisor.Checked);
            }

            refreshUsersList();
        }

        public void refreshUsersList()
        {
            lbUsers.DataBind();
            txtDisplayUsername.Text = "";
            cbDisplayTelephoneAgent.Checked = false;
            cbDisplaySupervisor.Checked = false;
            cbDisplayOCSupervisor.Checked = false;
            cbDisplayISSupervisor.Checked = false;
            cbDisplayUserAdmin.Checked = false;
            cbDisplayAppAdmin.Checked = false;

            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            UserInfo user = new UserInfo();

            user.deleteUser(lbUsers.SelectedValue);

            refreshUsersList();
        }
    }
}