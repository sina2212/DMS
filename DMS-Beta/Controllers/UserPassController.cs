using DMS_Beta.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DMS_Beta.Controllers
{
    public class UserPassController
    {
        public string SaveUsernamePassword(UsernamePassword userpass)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;

            p = new SqlParameter("@username", SqlDbType.NVarChar, 50);
            p.Value = userpass.Username;
            parameters.Add(p);
            p = new SqlParameter("@password", SqlDbType.NVarChar, 50);
            p.Value = userpass.Password;
            parameters.Add(p);
            p = new SqlParameter("@title", SqlDbType.NVarChar, 100);
            p.Value = userpass.Title;
            parameters.Add(p);
            p = new SqlParameter("@titleid", SqlDbType.BigInt);
            p.Value = userpass.TitleID;
            parameters.Add(p);

            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SaveUserPass";
            return Database.Save(cmd, parameters);
        }

        public UsernamePassword Showuserpass(Int64 id)
        {
            UsernamePassword result = new UsernamePassword();
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter parameter;

            parameter = new SqlParameter("@id", SqlDbType.BigInt);
            parameter.Value = id;
            parameters.Add(parameter);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ShowUserPass";
            DataTable dt = Database.Showbyparams(cmd, parameters);

            result.Username = dt.Rows[0][1].ToString();
            result.Password = dt.Rows[0][2].ToString();
            result.Title = dt.Rows[0][3].ToString();
            result.TitleID = Int64.Parse(dt.Rows[0][4].ToString());
            return result;
        }

        public string UpdateUserPass(UsernamePassword userpass)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;

            p = new SqlParameter("@id", SqlDbType.BigInt);
            p.Value = userpass.ID;
            parameters.Add(p);
            p = new SqlParameter("@username", SqlDbType.NVarChar, 50);
            p.Value = userpass.Username;
            parameters.Add(p);
            p = new SqlParameter("@password", SqlDbType.NVarChar, 50);
            p.Value = userpass.Password;
            parameters.Add(p);
            p = new SqlParameter("@title", SqlDbType.NVarChar, 100);
            p.Value = userpass.Title;
            parameters.Add(p);
            p = new SqlParameter("@titleid", SqlDbType.BigInt);
            p.Value = userpass.TitleID;
            parameters.Add(p);

            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UpdateUserPass";
            return Database.Save(cmd, parameters);
        }
    }
}
