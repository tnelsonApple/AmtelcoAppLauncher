using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Data.SqlClient;

namespace AmtelcoAppLauncher
{
    public class UserInfo
    {
        public string _username { get; set; }
        public bool _userAdmin { get; set; }
        public bool _appAdmin { get; set; }
        public bool _telephoneAgent { get; set; }
        public bool _supervisor { get; set; }
        public bool _ocSupervisor { get; set; }
        public bool _isSupervisor { get; set; }

        public void addNew(string username, bool userAdmin, bool appAdmin, bool telephoneAgent, bool supervisor, bool ocSupervisor, bool isSupervisor)
        {
            username = username.ToLower();

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            string query = "INSERT INTO tblUsers(adUsername, userAdmin, appAdmin, telephoneAgent, supervisor, ocSupervisor, isSupervisor) ";
            query += "VALUES(@username, @userAdmin, @appAdmin, @telephoneAgent, @supervisor, @ocSupervisor, @isSupervisor)";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = query;

            SqlParameter p1 = new SqlParameter("@username", username);
            SqlParameter p2 = new SqlParameter("@userAdmin", userAdmin);
            SqlParameter p3 = new SqlParameter("@appAdmin", appAdmin);
            SqlParameter p4 = new SqlParameter("@telephoneAgent", telephoneAgent);
            SqlParameter p5 = new SqlParameter("@supervisor", supervisor);
            SqlParameter p6 = new SqlParameter("@ocSupervisor", ocSupervisor);
            SqlParameter p7 = new SqlParameter("@isSupervisor", isSupervisor);

            sqlCmd.Parameters.Add(p1);
            sqlCmd.Parameters.Add(p2);
            sqlCmd.Parameters.Add(p3);
            sqlCmd.Parameters.Add(p4);
            sqlCmd.Parameters.Add(p5);
            sqlCmd.Parameters.Add(p6);
            sqlCmd.Parameters.Add(p7);

            sqlCmd.ExecuteNonQuery();

            sqlConn.Close();
            sqlConn.Dispose();
        }

        public void editUser(string username, bool userAdmin, bool appAdmin, bool telephoneAgent, bool supervisor, bool ocSupervisor, bool isSupervisor)
        {
            username = username.ToLower();

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            string query = "UPDATE tblUsers SET userAdmin = @userAdmin, appAdmin = @appAdmin, telephoneAgent = @telephoneAgent, supervisor = @supervisor, ocSupervisor = @ocSupervisor, isSupervisor = @isSupervisor WHERE adUsername = @username";
            
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = query;

            SqlParameter p1 = new SqlParameter("@username", username);
            SqlParameter p2 = new SqlParameter("@userAdmin", userAdmin);
            SqlParameter p3 = new SqlParameter("@appAdmin", appAdmin);
            SqlParameter p4 = new SqlParameter("@telephoneAgent", telephoneAgent);
            SqlParameter p5 = new SqlParameter("@supervisor", supervisor);
            SqlParameter p6 = new SqlParameter("@ocSupervisor", ocSupervisor);
            SqlParameter p7 = new SqlParameter("@isSupervisor", isSupervisor);

            sqlCmd.Parameters.Add(p1);
            sqlCmd.Parameters.Add(p2);
            sqlCmd.Parameters.Add(p3);
            sqlCmd.Parameters.Add(p4);
            sqlCmd.Parameters.Add(p5);
            sqlCmd.Parameters.Add(p6);
            sqlCmd.Parameters.Add(p7);

            sqlCmd.ExecuteNonQuery();

            sqlConn.Close();
            sqlConn.Dispose();
        }

        public void deleteUser(string username)
        {
            username = username.ToLower();

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            string query = "DELETE FROM tblUsers WHERE adUsername = @username";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = query;

            SqlParameter p1 = new SqlParameter("@username", username);

            sqlCmd.Parameters.Add(p1);

            sqlCmd.ExecuteNonQuery();

            sqlConn.Close();
            sqlConn.Dispose();
        }

        public void populateInfo(string username)
        {
            username = username.ToLower();

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = "SELECT userAdmin, appAdmin, telephoneAgent, supervisor, ocSupervisor, isSupervisor FROM tblUsers WHERE adUsername = @username";

            SqlParameter p1 = new SqlParameter("@username", username);

            sqlCmd.Parameters.Add(p1);

            SqlDataReader myReader = sqlCmd.ExecuteReader();

            while (myReader.Read())
            {
                this._username = username;

                this._userAdmin = myReader.GetBoolean(0);
                this._appAdmin = myReader.GetBoolean(1);
                this._telephoneAgent = myReader.GetBoolean(2);
                this._supervisor = myReader.GetBoolean(3);
                this._ocSupervisor = myReader.GetBoolean(4);
                this._isSupervisor = myReader.GetBoolean(5);
            }

            myReader.Close();

            sqlConn.Close();
            sqlConn.Dispose();
        }
    }
}