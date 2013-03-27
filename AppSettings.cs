using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Data.SqlClient;

namespace AmtelcoAppLauncher
{
    public class AppSettings
    {
        public string _siteCode { get; set; }
        public string _telephoneAgentExe { get; set; }
        public string _supervisorExe { get; set; }
        public string _ocSupervisorExe { get; set; }
        public string _isSupervisorExe { get; set; }

        public void updateTAExe(string siteCode, string telephoneAgentExe)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            string query = "UPDATE tblAppSettings SET telephoneAgentExe = @telephoneAgentExe WHERE siteCode = @siteCode";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = query;

            SqlParameter p1 = new SqlParameter("@telephoneAgentExe", telephoneAgentExe);
            SqlParameter p2 = new SqlParameter("@siteCode", siteCode);

            sqlCmd.Parameters.Add(p1);
            sqlCmd.Parameters.Add(p2);

            sqlCmd.ExecuteNonQuery();

            sqlConn.Close();
            sqlConn.Dispose();
        }

        public void updateSupExe(string siteCode, string supervisorExe)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            string query = "UPDATE tblAppSettings SET supervisorExe = @supervisorExe WHERE siteCode = @siteCode";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = query;

            SqlParameter p1 = new SqlParameter("@supervisorExe", supervisorExe);
            SqlParameter p2 = new SqlParameter("@siteCode", siteCode);

            sqlCmd.Parameters.Add(p1);
            sqlCmd.Parameters.Add(p2);

            sqlCmd.ExecuteNonQuery();

            sqlConn.Close();
            sqlConn.Dispose();
        }

        public void updateOCSupExe(string siteCode, string ocSupervisorExe)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            string query = "UPDATE tblAppSettings SET ocSupervisorExe = @ocSupervisorExe WHERE siteCode = @siteCode";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = query;

            SqlParameter p1 = new SqlParameter("@ocSupervisorExe", ocSupervisorExe);
            SqlParameter p2 = new SqlParameter("@siteCode", siteCode);

            sqlCmd.Parameters.Add(p1);
            sqlCmd.Parameters.Add(p2);

            sqlCmd.ExecuteNonQuery();

            sqlConn.Close();
            sqlConn.Dispose();
        }

        public void updateISSupExe(string siteCode, string isSupervisorExe)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            string query = "UPDATE tblAppSettings SET isSupervisorExe = @isSupervisorExe WHERE siteCode = @siteCode";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = query;

            SqlParameter p1 = new SqlParameter("@isSupervisorExe", isSupervisorExe);
            SqlParameter p2 = new SqlParameter("@siteCode", siteCode);

            sqlCmd.Parameters.Add(p1);
            sqlCmd.Parameters.Add(p2);

            sqlCmd.ExecuteNonQuery();

            sqlConn.Close();
            sqlConn.Dispose();
        }

        
        public void populate(string siteCode)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = "SELECT telephoneAgentExe, supervisorExe, ocSupervisorExe, isSupervisorExe FROM tblAppSettings WHERE siteCode = @siteCode";

            SqlParameter p1 = new SqlParameter("@siteCode", siteCode);

            sqlCmd.Parameters.Add(p1);

            SqlDataReader myReader = sqlCmd.ExecuteReader();

            while (myReader.Read())
            {
                this._siteCode = siteCode;

                this._telephoneAgentExe = myReader.GetString(0);
                this._supervisorExe = myReader.GetString(1);
                this._ocSupervisorExe = myReader.GetString(2);
                this._isSupervisorExe = myReader.GetString(3);
            }

            myReader.Close();

            sqlConn.Close();
            sqlConn.Dispose();
        }
    }
}