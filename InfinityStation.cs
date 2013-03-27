using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Data.SqlClient;

namespace AmtelcoAppLauncher
{
    public class InfinityStation
    {
        public int _id { get; set; }
        public string _siteCode { get; set; }
        public int _stationNum { get; set; }
        public string _stationType { get; set; }
        public int _officeAudioID { get; set; }
        public DateTime _reservedDate { get; set; }
        public string _reservedBy { get; set; }
        public string _audioInfo { get; set; }
        public string _workstationName { get; set; }

        public void populate(int id)
        { 
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = "SELECT stationNum, stationType, officeAudioID, reservedDate, reservedBy, audioInfo, workstationName FROM tblInfinityStations WHERE id = @id";

            SqlParameter p1 = new SqlParameter("@id", id);

            sqlCmd.Parameters.Add(p1);

            SqlDataReader myReader = sqlCmd.ExecuteReader();

            while (myReader.Read())
            {
                this._id = id;

                this._stationNum = myReader.GetInt32(0);
                this._stationType = myReader.GetString(1);
                this._officeAudioID = myReader.GetInt32(2);
                this._reservedDate = myReader.GetDateTime(3);
                this._reservedBy = myReader.GetString(4);
                this._audioInfo = myReader.GetString(5);
                this._workstationName = myReader.GetString(6);
            }

            myReader.Close();

            sqlConn.Close();
            sqlConn.Dispose();
        }

        public void update(int id, string stationType, int officeAudioID, string audioInfo, string workstationName)
        {
            audioInfo = audioInfo.Replace("\n", "<br />");

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            string query = "UPDATE tblInfinityStations SET stationType = @stationType, officeAudioID = @officeAudioID, audioInfo = @audioInfo, workstationName = @workstationName WHERE id = @id";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = query;

            SqlParameter p1 = new SqlParameter("@id", id);
            SqlParameter p2 = new SqlParameter("@stationType", stationType);
            SqlParameter p3 = new SqlParameter("@officeAudioID", officeAudioID);
            SqlParameter p4 = new SqlParameter("@audioInfo", audioInfo);
            SqlParameter p5 = new SqlParameter("@workstationName", workstationName);

            sqlCmd.Parameters.Add(p1);
            sqlCmd.Parameters.Add(p2);
            sqlCmd.Parameters.Add(p3);
            sqlCmd.Parameters.Add(p4);
            sqlCmd.Parameters.Add(p5);

            sqlCmd.ExecuteNonQuery();

            sqlConn.Close();
            sqlConn.Dispose();
        }

        public void updateMultiple(string stationNums, string stationType, int officeAudioID, string audioInfo)
        {
            audioInfo = audioInfo.Replace("\n", "<br />");

            string query = "";

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;

            SqlParameter p1 = new SqlParameter();
            SqlParameter p2 = new SqlParameter();
            SqlParameter p3 = new SqlParameter();
            SqlParameter p4 = new SqlParameter();

            if (stationType == "TelephoneAgent")
            {
                //Process Checked station numbers
                //Assign station numbers
                if (stationNums != "")
                {
                    query = "UPDATE tblInfinityStations SET stationType = @stationType, officeAudioID = @officeAudioID, audioInfo = @audioInfo, workstationName = 'Station ' + CONVERT(varchar(10), stationNum) ";
                    query += "WHERE siteCode = @siteCode AND officeAudioID != @officeAudioID AND stationNum IN(" + stationNums + ")";

                    sqlCmd.CommandText = query;

                    p1 = new SqlParameter("@stationType", stationType);
                    p2 = new SqlParameter("@officeAudioID", officeAudioID);
                    p3 = new SqlParameter("@audioInfo", audioInfo);
                    p4 = new SqlParameter("@siteCode", ConfigurationManager.AppSettings["siteCode"]);

                    sqlCmd.Parameters.Add(p1);
                    sqlCmd.Parameters.Add(p2);
                    sqlCmd.Parameters.Add(p3);
                    sqlCmd.Parameters.Add(p4);

                    sqlCmd.ExecuteNonQuery();
                }

                //Process unchecked station numbers  
                //Unassign station numbers
                if (stationNums != "")
                {
                    query = "UPDATE tblInfinityStations SET stationType = 'Unassigned', officeAudioID = 0, audioInfo = '', workstationName = '' ";
                    query += "WHERE siteCode = @siteCode AND stationType = @stationType AND officeAudioID = @officeAudioID AND stationNum NOT IN(" + stationNums + ")";
                }
                else
                {
                    query = "UPDATE tblInfinityStations SET stationType = 'Unassigned', officeAudioID = 0, audioInfo = '', workstationName = '' ";
                    query += "WHERE siteCode = @siteCode AND stationType = @stationType AND officeAudioID = @officeAudioID";
                }

                sqlCmd.CommandText = query;

                p1 = new SqlParameter("@stationType", stationType);
                p2 = new SqlParameter("@officeAudioID", officeAudioID);
                p3 = new SqlParameter("@audioInfo", audioInfo);
                p4 = new SqlParameter("@siteCode", ConfigurationManager.AppSettings["siteCode"]);

                sqlCmd.Parameters.Clear();
                sqlCmd.Parameters.Add(p1);
                sqlCmd.Parameters.Add(p2);
                sqlCmd.Parameters.Add(p3);
                sqlCmd.Parameters.Add(p4);

                sqlCmd.ExecuteNonQuery();

            }
            else
            {
                //Process Checked station numbers
                //assign station numbers
                if (stationNums != "")
                {
                    query = "UPDATE tblInfinityStations SET stationType = @stationType, officeAudioID = @officeAudioID, audioInfo = @audioInfo, workstationName = '' ";
                    query += "WHERE siteCode = @siteCode AND stationNum IN(" + stationNums + ")";

                    sqlCmd.CommandText = query;

                    p1 = new SqlParameter("@stationType", stationType);
                    p2 = new SqlParameter("@officeAudioID", officeAudioID);
                    p3 = new SqlParameter("@audioInfo", audioInfo);
                    p4 = new SqlParameter("@siteCode", ConfigurationManager.AppSettings["siteCode"]);

                    sqlCmd.Parameters.Add(p1);
                    sqlCmd.Parameters.Add(p2);
                    sqlCmd.Parameters.Add(p3);
                    sqlCmd.Parameters.Add(p4);

                    sqlCmd.ExecuteNonQuery();
                }

                //Process unchecked station numbers 
                //unassign station numbers
                if (stationNums != "")
                {
                    query = "UPDATE tblInfinityStations SET stationType = 'Unassigned', officeAudioID = 0, audioInfo = '', workstationName = '' ";
                    query += "WHERE siteCode = @siteCode AND stationType = @stationType AND stationNum NOT IN(" + stationNums + ")";
                }
                else
                {
                    query = "UPDATE tblInfinityStations SET stationType = 'Unassigned', officeAudioID = 0, audioInfo = '', workstationName = '' ";
                    query += "WHERE siteCode = @siteCode AND stationType = @stationType";
                }

                sqlCmd.CommandText = query;

                p1 = new SqlParameter("@stationType", stationType);
                p2 = new SqlParameter("@siteCode", ConfigurationManager.AppSettings["siteCode"]);

                sqlCmd.Parameters.Clear();
                sqlCmd.Parameters.Add(p1);
                sqlCmd.Parameters.Add(p2);

                sqlCmd.ExecuteNonQuery();

                
            }
            sqlConn.Close();
            sqlConn.Dispose();
        }

        public int getDynamicStation(string stationType, int officeAudioID, string username)
        {
            int stationNum = 0;

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            string query = "spGetInfStationNum";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = query;
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter p1 = new SqlParameter("@stationType", stationType);
            SqlParameter p2 = new SqlParameter("@officeAudioID", officeAudioID);
            SqlParameter p3 = new SqlParameter("@username", username);
            SqlParameter p4 = new SqlParameter("@siteCode", ConfigurationManager.AppSettings["siteCode"]);

            sqlCmd.Parameters.Add(p1);
            sqlCmd.Parameters.Add(p2);
            sqlCmd.Parameters.Add(p3);
            sqlCmd.Parameters.Add(p4);

            SqlDataReader myReader = sqlCmd.ExecuteReader();
            
            while (myReader.Read())
            {
                stationNum = myReader.GetInt32(0);
            }

            myReader.Close();

            sqlConn.Close();
            sqlConn.Dispose();

            return stationNum;
        }

        public void updateReserved(string siteCode, int stationNum, string username)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            string query = "UPDATE tblInfinityStations SET reservedBy = @reservedBy, reservedDate = GETDATE() WHERE siteCode = @siteCode AND stationNum = @stationNum";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = query;

            SqlParameter p1 = new SqlParameter("@reservedBy", username);
            SqlParameter p2 = new SqlParameter("@siteCode", siteCode);
            SqlParameter p3 = new SqlParameter("@stationNum", stationNum);

            sqlCmd.Parameters.Add(p1);
            sqlCmd.Parameters.Add(p2);
            sqlCmd.Parameters.Add(p3);

            sqlCmd.ExecuteNonQuery();

            sqlConn.Close();
            sqlConn.Dispose();
        }
     
    }
}