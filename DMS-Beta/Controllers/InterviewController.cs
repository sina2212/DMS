using DMS_Beta.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DMS_Beta.Controllers
{
    public class InterviewController
    {
        public InterviewController()
        {

        }

        public string SaveInterview(Interview interview, string nationalcode,int emp)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;
            
            p = new SqlParameter("@introduction", SqlDbType.NVarChar, 50);
            p.Value = interview.Introduction;
            parameters.Add(p);
            p = new SqlParameter("@nationalcode", SqlDbType.NVarChar, 50);
            p.Value = nationalcode;
            parameters.Add(p);
            p = new SqlParameter("@position", SqlDbType.NVarChar, 50);
            p.Value = interview.Position;
            parameters.Add(p);
            p = new SqlParameter("@interviewdate", SqlDbType.Date);
            p.Value = interview.Date;
            parameters.Add(p);
            p = new SqlParameter("@emp", SqlDbType.Int);
            p.Value = emp;
            parameters.Add(p);
            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SaveInterview";
            return Database.Save(cmd, parameters);
        }

        public string SaveEducational(Interview interview, int emp)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;
            
            p = new SqlParameter("@emp", SqlDbType.Int);
            p.Value = emp;
            parameters.Add(p);
            p = new SqlParameter("@educational", SqlDbType.Bit);
            p.Value = interview.Educational;
            parameters.Add(p);
            p = new SqlParameter("@educationdate", SqlDbType.Date,12);
            p.Value = interview.Educationdate;
            parameters.Add(p);
            p = new SqlParameter("@educationresult", SqlDbType.NVarChar, 50);
            p.Value = interview.EducationResult;
            parameters.Add(p);
            p = new SqlParameter("@interviewid", SqlDbType.BigInt);
            p.Value = interview.I_Code;
            parameters.Add(p);
            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SaveEducational";
            return Database.Save(cmd, parameters);
        }

        public string SaveTrial(Interview interview, int emp)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;

            p = new SqlParameter("@emp", SqlDbType.Int);
            p.Value = emp;
            parameters.Add(p);
            p = new SqlParameter("@trial", SqlDbType.Bit);
            p.Value = interview.Training;
            parameters.Add(p);
            p = new SqlParameter("@trainingdate", SqlDbType.Date, 12);
            p.Value = interview.TrainingDate;
            parameters.Add(p);
            p = new SqlParameter("@trainingresult", SqlDbType.NVarChar, 50);
            p.Value = interview.TrainingResult;
            parameters.Add(p);
            p = new SqlParameter("@interviewid", SqlDbType.BigInt);
            p.Value = interview.I_Code;
            parameters.Add(p);
            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SaveTrial";
            return Database.Save(cmd, parameters);
        }

        public DataTable ShowInterview(Int64 interviewid)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter parameter;

            parameter = new SqlParameter("@interviewid", SqlDbType.BigInt);
            parameter.Value = interviewid;
            parameters.Add(parameter);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ShowInterview";
            DataTable dt = Database.Showbyparams(cmd, parameters);
            return dt;
        }

        public string UpdateInterview(Interview interview, int emp, int interviewid)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;

            p = new SqlParameter("@introduction", SqlDbType.NVarChar, 50);
            p.Value = interview.Introduction;
            parameters.Add(p);
            p = new SqlParameter("@interviewid", SqlDbType.BigInt);
            p.Value = interviewid;
            parameters.Add(p);
            p = new SqlParameter("@position", SqlDbType.NVarChar, 50);
            p.Value = interview.Position;
            parameters.Add(p);
            p = new SqlParameter("@interviewdate", SqlDbType.Date);
            p.Value = interview.Date;
            parameters.Add(p);
            p = new SqlParameter("@emp", SqlDbType.Int);
            p.Value = emp;
            parameters.Add(p);
            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UpdateInterview";
            return Database.Save(cmd, parameters);
        }
    }
}
