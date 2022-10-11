using DMS_Beta.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DMS_Beta.Controllers
{
    public class PhoneController
    {
        public string SavePhone(PhoneModel ph)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;

            p = new SqlParameter("@emp_ref", SqlDbType.Int);
            p.Value = ph.Person_ref;
            parameters.Add(p);
            p = new SqlParameter("@phone", SqlDbType.NVarChar, 15);
            p.Value = ph.PhoneNumber;
            parameters.Add(p);
            p = new SqlParameter("@title", SqlDbType.NVarChar, 50);
            p.Value = ph.Title;
            parameters.Add(p);
            p = new SqlParameter("@subtitle", SqlDbType.NVarChar, 50);
            p.Value = ph.Subtitle;
            parameters.Add(p);
            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SavePhone";
            return Database.Save(cmd, parameters);
        }

        public DataTable ShowPhoneNumbers(int emp_id, string title)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter parameter;

            parameter = new SqlParameter("@emp_id", SqlDbType.Int);
            parameter.Value = emp_id;
            parameters.Add(parameter);
            parameter = new SqlParameter("@title", SqlDbType.NVarChar, 50);
            parameter.Value = title;
            parameters.Add(parameter);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ShowPhonenumbers";
            DataTable dt = Database.Showbyparams(cmd, parameters);
            return dt;
        }
    }
}
