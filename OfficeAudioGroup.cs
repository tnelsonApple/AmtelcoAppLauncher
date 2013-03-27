using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Data.SqlClient;

namespace AmtelcoAppLauncher
{
    public class OfficeAudioGroup
    {
        public int _id { get; set; }
        public string _siteCode { get; set; }
        public string _name { get; set; }
        public string _defaultAudioInfo { get; set; }
        public bool _staticStations { get; set; }

        public void insertNew(string siteCode, string name, string defaultAudioInfo, bool staticStations)
        {
            defaultAudioInfo = defaultAudioInfo.Replace("\n", "<br />");

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            string query = "INSERT INTO tblOfficeAudioGroups(siteCode, name, defaultAudioInfo, stationsAreStatic) ";
            query += "VALUES(@siteCode, @name, @defaultAudioInfo, @staticStations)";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = query;

            SqlParameter p1 = new SqlParameter("@siteCode", siteCode);
            SqlParameter p2 = new SqlParameter("@name", name);
            SqlParameter p3 = new SqlParameter("@defaultAudioInfo", defaultAudioInfo);
            SqlParameter p4 = new SqlParameter("@staticStations", staticStations);

            sqlCmd.Parameters.Add(p1);
            sqlCmd.Parameters.Add(p2);
            sqlCmd.Parameters.Add(p3);
            sqlCmd.Parameters.Add(p4);

            sqlCmd.ExecuteNonQuery();

            sqlConn.Close();
            sqlConn.Dispose();
        }

        public void updateDetails(int id, string name, string defaultAudioInfo, bool staticStations)
        {
            defaultAudioInfo = defaultAudioInfo.Replace("\n", "<br />");

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            string query = "UPDATE tblOfficeAudioGroups SET name = @name, defaultAudioInfo = @defaultAudioInfo, stationsAreStatic = @staticStations WHERE id = @id";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = query;

            SqlParameter p1 = new SqlParameter("@name", name);
            SqlParameter p2 = new SqlParameter("@defaultAudioInfo", defaultAudioInfo);
            SqlParameter p3 = new SqlParameter("@staticStations", staticStations);
            SqlParameter p4 = new SqlParameter("@id", id);

            sqlCmd.Parameters.Add(p1);
            sqlCmd.Parameters.Add(p2);
            sqlCmd.Parameters.Add(p3);
            sqlCmd.Parameters.Add(p4);

            sqlCmd.ExecuteNonQuery();

            sqlConn.Close();
            sqlConn.Dispose();
        }

        public void updateStations(int id, string stationNums)
        { 
            
        }

        public void delete(int id)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            string query = "DELETE FROM tblOfficeAudioGroups WHERE id = @id";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = query;

            SqlParameter p1 = new SqlParameter("@id", id);

            sqlCmd.Parameters.Add(p1);

            sqlCmd.ExecuteNonQuery();

            query = "UPDATE tblInfinityStations SET stationType = 'Unassigned', officeAudioID = 0, audioInfo = '' WHERE officeAudioID = @officeAudioID";
            sqlCmd.Parameters.Clear();
            p1 = new SqlParameter("@officeAudioID", id);
            sqlCmd.Parameters.Add(p1);
            sqlCmd.CommandText = query;

            sqlCmd.ExecuteNonQuery();

            sqlConn.Close();
            sqlConn.Dispose();
        }

        public void populate(int id)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            sqlConn.Open();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = "SELECT name, defaultAudioInfo, stationsAreStatic FROM tblOfficeAudioGroups WHERE id = @id";

            SqlParameter p1 = new SqlParameter("@id", id);

            sqlCmd.Parameters.Add(p1);

            SqlDataReader myReader = sqlCmd.ExecuteReader();

            while (myReader.Read())
            {
                this._id = id;
                this._name = myReader.GetString(0);
                this._defaultAudioInfo = myReader.GetString(1);
                this._staticStations = myReader.GetBoolean(2);
            }

            myReader.Close();

            sqlConn.Close();
            sqlConn.Dispose();
        }
    }
}