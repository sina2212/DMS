using DMS_Beta.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DMS_Beta.Controllers
{
    public class KnowledgeManagementController
    {
        public string SaveKnowledgeManagement(KnowledgeManagement knowledgeManagement)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;

            p = new SqlParameter("@description", SqlDbType.NVarChar, 4000);
            p.Value = knowledgeManagement.Senario;
            parameters.Add(p);
            p = new SqlParameter("@keyword", SqlDbType.NVarChar, 50);
            p.Value = knowledgeManagement.KeyWord;
            parameters.Add(p);
            p = new SqlParameter("@date_", SqlDbType.Date);
            p.Value = knowledgeManagement.Date;
            parameters.Add(p);
            p = new SqlParameter("@organization", SqlDbType.NVarChar, 50);
            p.Value = knowledgeManagement.Client;
            parameters.Add(p);
            p = new SqlParameter("@empcode", SqlDbType.Int);
            p.Value = knowledgeManagement.Employer;
            parameters.Add(p);

            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SaveKnowledgeManagement";
            return Database.Save(cmd, parameters);
        }

        public DataTable ShowKm(UInt64 kmid)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter parameter;
            KnowledgeManagement km = new KnowledgeManagement();

            parameter = new SqlParameter("@kmcode", SqlDbType.BigInt);
            parameter.Value = kmid;
            parameters.Add(parameter);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ShowKM";
            DataTable dt = Database.Showbyparams(cmd, parameters);
            return dt;
        }

        public string UpdateKnowledgeManagement(KnowledgeManagement knowledgeManagement)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;

            p = new SqlParameter("@kmid", SqlDbType.BigInt);
            p.Value = knowledgeManagement.KMID;
            parameters.Add(p);
            p = new SqlParameter("@description", SqlDbType.NVarChar, 4000);
            p.Value = knowledgeManagement.Senario;
            parameters.Add(p);
            p = new SqlParameter("@keyword", SqlDbType.NVarChar, 50);
            p.Value = knowledgeManagement.KeyWord;
            parameters.Add(p);
            p = new SqlParameter("@date_", SqlDbType.Date);
            p.Value = knowledgeManagement.Date;
            parameters.Add(p);
            p = new SqlParameter("@organization", SqlDbType.NVarChar, 50);
            p.Value = knowledgeManagement.Client;
            parameters.Add(p);
            p = new SqlParameter("@empcode", SqlDbType.Int);
            p.Value = knowledgeManagement.Employer;
            parameters.Add(p);

            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UpdateKnowledgeManagement";
            return Database.Save(cmd, parameters);
        }
    }
}
