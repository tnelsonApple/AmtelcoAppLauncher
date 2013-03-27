using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Data.SqlClient;

namespace AmtelcoAppLauncher
{
    public class ISStation
    {
        public int _id { get; set; }
        public string _siteCode { get; set; }
        public string _profileName { get; set; }
        public int _stationNum { get; set; }
        public DateTime _reservedDate { get; set; }
        public string _reservedBy { get; set; }

        public void populate(int id)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = "SELECT profileName, stationNum, reservedDate, reservedBy FROM tblISStations WHERE id = @id";

            SqlParameter p1 = new SqlParameter("@id", id);

            sqlCmd.Parameters.Add(p1);

            SqlDataReader myReader = sqlCmd.ExecuteReader();

            while (myReader.Read())
            {
                this._id = id;
                this._profileName = myReader.GetString(0);
                this._stationNum = myReader.GetInt32(1);
            }

            myReader.Close();

            sqlConn.Close();
            sqlConn.Dispose();
        }

        public void insertNew(string siteCode, string profileName, int stationNum)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            string query = "INSERT INTO tblISStations(siteCode, profileName, stationNum, reservedDate, reservedBy) ";
            query += "VALUES(@siteCode, @profileName, @stationNum, '1/1/2013', '')";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = query;

            SqlParameter p1 = new SqlParameter("@siteCode", siteCode);
            SqlParameter p2 = new SqlParameter("@profileName", profileName);
            SqlParameter p3 = new SqlParameter("@stationNum", stationNum);

            sqlCmd.Parameters.Add(p1);
            sqlCmd.Parameters.Add(p2);
            sqlCmd.Parameters.Add(p3);

            sqlCmd.ExecuteNonQuery();

            sqlConn.Close();
            sqlConn.Dispose();
        }

        public void update(int id, string profileName, int stationNum)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            string query = "UPDATE tblISStations SET profileName = @profileName, stationNum = @stationNum WHERE id = @id";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = query;

            SqlParameter p1 = new SqlParameter("@id", id);
            SqlParameter p2 = new SqlParameter("@profileName", profileName);
            SqlParameter p3 = new SqlParameter("@stationNum", stationNum);

            sqlCmd.Parameters.Add(p1);
            sqlCmd.Parameters.Add(p2);
            sqlCmd.Parameters.Add(p3);

            sqlCmd.ExecuteNonQuery();

            sqlConn.Close();
            sqlConn.Dispose();
        }

        public void delete(int id)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            string query = "DELETE FROM tblISStations WHERE id = @id";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = query;

            SqlParameter p1 = new SqlParameter("@id", id);

            sqlCmd.Parameters.Add(p1);

            sqlCmd.ExecuteNonQuery();

            sqlConn.Close();
            sqlConn.Dispose();
        }

        public void updateReserved(string siteCode, int stationNum, string username)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            string query = "UPDATE tblISStations SET reservedBy = @reservedBy, reservedDate = GETDATE() WHERE siteCode = @siteCode AND stationNum = @stationNum";

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
    }
}