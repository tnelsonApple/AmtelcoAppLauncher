using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Data.SqlClient;

namespace AmtelcoAppLauncher
{
    public class Log
    {
        public int _id { get; set; }
        public string _siteCode { get; set; }
        public DateTime _timestamp { get; set; }
        public string logType { get; set; }
        public string _stationType { get; set; }
        public int _stationNum { get; set; }
        public string username { get; set; }
        public string description { get; set; }

        public void insertNew(string siteCode, string logType, string stationType, int stationNum, string username, string description)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            string query = "INSERT INTO tblLog(siteCode, timestamp, logType, stationType, stationNum, username, description) ";
            query += "VALUES(@siteCode, GETDATE(), @logType, @stationType, @stationNum, @username, @description)";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = query;

            SqlParameter p1 = new SqlParameter("@siteCode", siteCode);
            SqlParameter p2 = new SqlParameter("@logType", logType);
            SqlParameter p3 = new SqlParameter("@stationType", stationType);
            SqlParameter p4 = new SqlParameter("@stationNum", stationNum);
            SqlParameter p5 = new SqlParameter("@username", username);
            SqlParameter p6 = new SqlParameter("@description", description);

            sqlCmd.Parameters.Add(p1);
            sqlCmd.Parameters.Add(p2);
            sqlCmd.Parameters.Add(p3);
            sqlCmd.Parameters.Add(p4);
            sqlCmd.Parameters.Add(p5);
            sqlCmd.Parameters.Add(p6);

            sqlCmd.ExecuteNonQuery();

            sqlConn.Close();
            sqlConn.Dispose();
        }
    }

    

}