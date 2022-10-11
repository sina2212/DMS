using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace DMS_Beta.Models
{
    class Database
    {
        static SqlConnection connection = new SqlConnection(DMS_Beta.Properties.Settings.Default.connectionstring);
        static void connect()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public static void disconnect()
        {
            connection.Close();
        }
        public bool checkconnection()
        {
            bool isValid = false;
            try
            {
                connection.Open();
                isValid = true;
            }
            catch (SqlException)
            {
                isValid = false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return isValid;
        }
        public static string Save(SqlCommand cmd, List<SqlParameter> parameters)
        {
            string cout;
            try
            {
                connect();
                cmd.Connection = connection;
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }
                cmd.ExecuteNonQuery();
                cout = cmd.Parameters["@ret"].Value.ToString();
                disconnect();
                return cout;
            }
            finally
            {
                disconnect();
            }
        }
        public static DataTable Show(SqlCommand cmd)
        {
            try
            {
                connect();
                cmd.Connection = connection;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                disconnect();
                return dt;
            }
            finally
            {
                disconnect();
            }
        }
        public static DataTable Showbyparams(SqlCommand cmd, List<SqlParameter> parameters)
        {
            try
            {
                connect();
                cmd.Connection = connection;
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                disconnect();
                return dt;
            }
            finally
            {
                disconnect();
            }
        }
    }
}
