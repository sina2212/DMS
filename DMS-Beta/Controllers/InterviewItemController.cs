using DMS_Beta.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DMS_Beta.Controllers
{
    class InterviewItemController
    {
        public string SaveInterviewer(InterviewItem interviewer)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;
            p = new SqlParameter("@interviewref", SqlDbType.BigInt);
            p.Value = interviewer.InterViewref;
            parameters.Add(p);
            p = new SqlParameter("@interviewer", SqlDbType.NVarChar, 50);
            p.Value = interviewer.Name;
            parameters.Add(p);
            p = new SqlParameter("@result", SqlDbType.NVarChar, 50);
            p.Value = interviewer.Result;
            parameters.Add(p);
            p = new SqlParameter("@comment", SqlDbType.NVarChar, 100);
            p.Value = interviewer.Comment;
            parameters.Add(p);

            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SaveInterviewers";
            return Database.Save(cmd, parameters);
        }

        public string EditeInterviewer(InterviewItem interviewer)
        {
            //TODO: Update code
            return null;
        }

        public string DeleteInterviewer(InterviewItem interviewer)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;
            p = new SqlParameter("@interviewref", SqlDbType.BigInt);
            p.Value = interviewer.InterViewref;
            parameters.Add(p);
            p = new SqlParameter("@interviewer", SqlDbType.NVarChar, 50);
            p.Value = interviewer.Name;
            parameters.Add(p);
            return null;

            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteInterviewer";
            return Database.Save(cmd, parameters);
        }
    }
}
