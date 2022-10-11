using DMS_Beta.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DMS_Beta.Controllers
{
    public class PersonController
    {
        public string SavePerson(Person person)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;

            p = new SqlParameter("@realname", SqlDbType.NVarChar, 50);
            p.Value = person.RealName;
            parameters.Add(p);
            p = new SqlParameter("@education", SqlDbType.NVarChar, 50);
            p.Value = person.Education;
            parameters.Add(p);
            p = new SqlParameter("@nationalcard", SqlDbType.NVarChar, 50);
            p.Value = person.NationalNumber;
            parameters.Add(p);

            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SavePerson";
            return Database.Save(cmd, parameters);
        }
    }
}
