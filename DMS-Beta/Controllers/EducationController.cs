using DMS_Beta.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DMS_Beta.Controllers
{
    public class EducationController
    {
        public string SaveKnowledge(Education knowledge, UsernamePassword userpass)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;

            p = new SqlParameter("@course", SqlDbType.NVarChar, 100);
            p.Value = knowledge.CourseName;
            parameters.Add(p);
            p = new SqlParameter("@empcode", SqlDbType.Int);
            p.Value = knowledge.EmployeeId;
            parameters.Add(p);
            p = new SqlParameter("@dep_name", SqlDbType.NVarChar, 50);
            p.Value = knowledge.DepartmanName;
            parameters.Add(p);
            p = new SqlParameter("@institute", SqlDbType.NVarChar, 100);
            p.Value = knowledge.Institute;
            parameters.Add(p);
            p = new SqlParameter("@begindate", SqlDbType.Date);
            p.Value = knowledge.BeginDate;
            parameters.Add(p);
            p = new SqlParameter("@enddate", SqlDbType.Date);
            p.Value = knowledge.EndDate;
            parameters.Add(p);
            p = new SqlParameter("@goal", SqlDbType.NVarChar, 100);
            p.Value = knowledge.Goal;
            parameters.Add(p);
            p = new SqlParameter("@state", SqlDbType.NVarChar, 50);
            p.Value = knowledge.Status;
            parameters.Add(p);
            p = new SqlParameter("@cost", SqlDbType.BigInt);
            p.Value = knowledge.Cost;
            parameters.Add(p);
            p = new SqlParameter("@username", SqlDbType.NVarChar, 50);
            p.Value = userpass.Username;
            parameters.Add(p);
            p = new SqlParameter("@password", SqlDbType.NVarChar, 50);
            p.Value = userpass.Password;
            parameters.Add(p);
            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SaveKnowledge";
            return Database.Save(cmd, parameters);
        }

        public string UpdateKnowledge(Education knowledge, UsernamePassword userpass)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;

            p = new SqlParameter("@knowledgecode", SqlDbType.BigInt);
            p.Value = knowledge.ID;
            parameters.Add(p);
            p = new SqlParameter("@course", SqlDbType.NVarChar, 100);
            p.Value = knowledge.CourseName;
            parameters.Add(p);
            p = new SqlParameter("@empcode", SqlDbType.Int);
            p.Value = knowledge.EmployeeId;
            parameters.Add(p);
            p = new SqlParameter("@dep_name", SqlDbType.NVarChar, 50);
            p.Value = knowledge.DepartmanName;
            parameters.Add(p);
            p = new SqlParameter("@institute", SqlDbType.NVarChar, 100);
            p.Value = knowledge.Institute;
            parameters.Add(p);
            p = new SqlParameter("@begindate", SqlDbType.Date);
            p.Value = knowledge.BeginDate;
            parameters.Add(p);
            p = new SqlParameter("@enddate", SqlDbType.Date);
            p.Value = knowledge.EndDate;
            parameters.Add(p);
            p = new SqlParameter("@goal", SqlDbType.NVarChar, 100);
            p.Value = knowledge.Goal;
            parameters.Add(p);
            p = new SqlParameter("@state", SqlDbType.NVarChar, 50);
            p.Value = knowledge.Status;
            parameters.Add(p);
            p = new SqlParameter("@cost", SqlDbType.BigInt);
            p.Value = knowledge.Cost;
            parameters.Add(p);
            p = new SqlParameter("@username", SqlDbType.NVarChar, 50);
            p.Value = userpass.Username;
            parameters.Add(p);
            p = new SqlParameter("@password", SqlDbType.NVarChar, 50);
            p.Value = userpass.Password;
            parameters.Add(p);
            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UpdateKnowledge";
            return Database.Save(cmd, parameters);
        }

        public DataTable ShowKnowledge(Int64 knowledgeid)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter parameter;
            Education res = new Education();
            UsernamePassword userpass = new UsernamePassword();

            parameter = new SqlParameter("@knowledgecode", SqlDbType.BigInt);
            parameter.Value = knowledgeid;
            parameters.Add(parameter);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ShowKnoledge";
            DataTable dt = Database.Showbyparams(cmd, parameters);
            return dt;
        }
    }
}
