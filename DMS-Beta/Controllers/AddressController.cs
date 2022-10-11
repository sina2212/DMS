using DMS_Beta.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DMS_Beta.Controllers
{
    public class AddressController
    {
        public string SaveAddress(AddressModel ph)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;

            p = new SqlParameter("@emp_ref", SqlDbType.Int);
            p.Value = ph.Person_ref;
            parameters.Add(p);
            p = new SqlParameter("@state", SqlDbType.NVarChar, 50);
            p.Value = ph.State;
            parameters.Add(p);
            p = new SqlParameter("@city", SqlDbType.NVarChar, 50);
            p.Value = ph.City;
            parameters.Add(p);
            p = new SqlParameter("@address", SqlDbType.NVarChar, 50);
            p.Value = ph.FullAddress;
            parameters.Add(p);
            p = new SqlParameter("@type", SqlDbType.NVarChar, 50);
            p.Value = ph.Type;
            parameters.Add(p);
            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SaveAddress";
            return Database.Save(cmd, parameters);
        }

        public DataTable ShowAddress(int emp_id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter parameter;

            parameter = new SqlParameter("@emp_id", SqlDbType.Int);
            parameter.Value = emp_id;
            parameters.Add(parameter);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ShowAddress";
            DataTable dt = Database.Showbyparams(cmd, parameters);
            return dt;
        }
    }
}
