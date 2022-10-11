using DMS_Beta.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DMS_Beta.Controllers
{
    public class VacationController
    {
        public string Save(Vacation vacation)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p = new SqlParameter();
            p = new SqlParameter("@realname", SqlDbType.NVarChar, 50);
            p.Value = vacation.RealName;
            parameters.Add(p);
            p = new SqlParameter("@dep_name", SqlDbType.NVarChar, 50);
            p.Value = vacation.Departman;
            parameters.Add(p);
            p = new SqlParameter("@type", SqlDbType.Bit);
            p.Value = vacation.Type;
            parameters.Add(p);
            p = new SqlParameter("@duration", SqlDbType.Int);
            p.Value = vacation.Duration;
            parameters.Add(p);
            p = new SqlParameter("@fromdate", SqlDbType.DateTime);
            p.Value = vacation.From;
            parameters.Add(p);
            p = new SqlParameter("@todate", SqlDbType.DateTime);
            p.Value = vacation.Todate;
            parameters.Add(p);
            p = new SqlParameter("@date_", SqlDbType.DateTime);
            p.Value = vacation.Date_;
            parameters.Add(p);
            p = new SqlParameter("@status", SqlDbType.Bit);
            p.Value = vacation.Status;
            parameters.Add(p);
            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SaveVacation";
            return Database.Save(cmd, parameters);
        }
    }
}
