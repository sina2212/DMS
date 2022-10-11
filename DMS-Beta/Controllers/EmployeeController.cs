using DMS_Beta.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DMS_Beta.Controllers
{
    public class EmployeeController
    {
        public string SaveEmployee(Employee employee)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;

            p = new SqlParameter("@realname", SqlDbType.NVarChar, 50);
            p.Value = employee.RealName;
            parameters.Add(p);
            p = new SqlParameter("@education", SqlDbType.NVarChar, 50);
            p.Value = employee.Education;
            parameters.Add(p);
            p = new SqlParameter("@gender", SqlDbType.Bit);
            p.Value = employee.Gender;
            parameters.Add(p);
            p = new SqlParameter("@status", SqlDbType.Bit);
            p.Value = employee.Status;
            parameters.Add(p);
            p = new SqlParameter("@child", SqlDbType.Int);
            p.Value = employee.Child;
            parameters.Add(p);
            p = new SqlParameter("@birthdate", SqlDbType.Date);
            p.Value = employee.BirthDate;
            parameters.Add(p);
            p = new SqlParameter("@contract", SqlDbType.BigInt);
            p.Value = employee.Contract;
            parameters.Add(p);
            p = new SqlParameter("@emp_code", SqlDbType.Int);
            p.Value = employee.EmpCode;
            parameters.Add(p);
            p = new SqlParameter("@beginemployment", SqlDbType.Date);
            p.Value = employee.BeginEmployment;
            parameters.Add(p);
            p = new SqlParameter("@endemployment", SqlDbType.Date);
            p.Value = employee.EndEmployment;
            parameters.Add(p);
            p = new SqlParameter("@jobposition", SqlDbType.NVarChar, 50);
            p.Value = employee.JobPosition;
            parameters.Add(p);
            p = new SqlParameter("@jobtitile", SqlDbType.NVarChar, 50);
            p.Value = employee.JobTitle;
            parameters.Add(p);
            p = new SqlParameter("@dep_name", SqlDbType.NVarChar, 50);
            p.Value = employee.DepName;
            parameters.Add(p);
            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SaveEmployee";
            return Database.Save(cmd, parameters);
        }

        public DataTable ShowEmployee(int employeeid)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter parameter;

            parameter = new SqlParameter("@emp_id", SqlDbType.BigInt);
            parameter.Value = employeeid;
            parameters.Add(parameter);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ShowEmployee";
            DataTable dt = Database.Showbyparams(cmd, parameters);
            return dt;
        }

        public DataTable Login(string user, string pass)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;

            p = new SqlParameter("@username", SqlDbType.NVarChar, 50);
            p.Value = user;
            parameters.Add(p);

            p = new SqlParameter("@password", SqlDbType.NVarChar, 50);
            p.Value = pass;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UserLogin";
            return Database.Showbyparams(cmd, parameters);
        }

        public string Logout(Int64 var)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;

            p = new SqlParameter("@logkey", SqlDbType.BigInt);
            p.Value = var;
            parameters.Add(p);

            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UserLogOut";

            return Database.Save(cmd, parameters);
        }

        public string UpdateEmployee(Employee employee)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;

            p = new SqlParameter("@realname", SqlDbType.NVarChar, 50);
            p.Value = employee.RealName;
            parameters.Add(p);
            p = new SqlParameter("@education", SqlDbType.NVarChar, 50);
            p.Value = employee.Education;
            parameters.Add(p);
            p = new SqlParameter("@gender", SqlDbType.Bit);
            p.Value = employee.Gender;
            parameters.Add(p);
            p = new SqlParameter("@status", SqlDbType.Bit);
            p.Value = employee.Status;
            parameters.Add(p);
            p = new SqlParameter("@child", SqlDbType.Int);
            p.Value = employee.Child;
            parameters.Add(p);
            p = new SqlParameter("@birthdate", SqlDbType.Date);
            p.Value = employee.BirthDate;
            parameters.Add(p);
            p = new SqlParameter("@contract", SqlDbType.BigInt);
            p.Value = employee.Contract;
            parameters.Add(p);
            p = new SqlParameter("@emp_code", SqlDbType.Int);
            p.Value = employee.EmpCode;
            parameters.Add(p);
            p = new SqlParameter("@beginemployment", SqlDbType.Date);
            p.Value = employee.BeginEmployment;
            parameters.Add(p);
            p = new SqlParameter("@endemployment", SqlDbType.Date);
            p.Value = employee.EndEmployment;
            parameters.Add(p);
            p = new SqlParameter("@jobposition", SqlDbType.NVarChar, 50);
            p.Value = employee.JobPosition;
            parameters.Add(p);
            p = new SqlParameter("@jobtitile", SqlDbType.NVarChar, 50);
            p.Value = employee.JobTitle;
            parameters.Add(p);
            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UpdateEmployee";
            return Database.Save(cmd, parameters);
        }
    }
}
