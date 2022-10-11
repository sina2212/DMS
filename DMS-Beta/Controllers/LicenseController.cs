using DMS_Beta.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DMS_Beta.Controllers
{
    public class LicenseController
    {
        public string SaveLicense(License license)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;

            p = new SqlParameter("@number", SqlDbType.BigInt);
            p.Value = license.LicenseNumber;
            parameters.Add(p);
            p = new SqlParameter("@name", SqlDbType.NVarChar, 100);
            p.Value = license.LicenseName;
            parameters.Add(p);
            p = new SqlParameter("issuer", SqlDbType.NVarChar, 100);
            p.Value = license.Issuer;
            parameters.Add(p);
            p = new SqlParameter("issudate", SqlDbType.Date);
            p.Value = license.Date;
            parameters.Add(p);
            p = new SqlParameter("exp_date", SqlDbType.Date);
            p.Value = license.ExpireDate;
            parameters.Add(p);
            p = new SqlParameter("@type", SqlDbType.Bit);
            p.Value = license.Type;
            parameters.Add(p);
            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SaveLicense";
            return Database.Save(cmd, parameters);
        }

        public string UpdateLicense(License license)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;

            p = new SqlParameter("@id", SqlDbType.BigInt);
            p.Value = license.ID;
            parameters.Add(p);
            p = new SqlParameter("@number", SqlDbType.BigInt);
            p.Value = license.LicenseNumber;
            parameters.Add(p);
            p = new SqlParameter("@name", SqlDbType.NVarChar, 100);
            p.Value = license.LicenseName;
            parameters.Add(p);
            p = new SqlParameter("issuer", SqlDbType.NVarChar, 100);
            p.Value = license.Issuer;
            parameters.Add(p);
            p = new SqlParameter("issudate", SqlDbType.Date);
            p.Value = license.Date;
            parameters.Add(p);
            p = new SqlParameter("exp_date", SqlDbType.Date);
            p.Value = license.ExpireDate;
            parameters.Add(p);
            p = new SqlParameter("@type", SqlDbType.Bit);
            p.Value = license.Type;
            parameters.Add(p);
            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UpdateLicense";
            return Database.Save(cmd, parameters);
        }

        public License ShowLicense(Int64 id)
        {
            License result = new License();
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p = new SqlParameter("@id", SqlDbType.BigInt);
            p.Value = id;
            parameters.Add(p);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ShowLicense";
            DataTable dt = Database.Showbyparams(cmd, parameters);
            result.ID = Int64.Parse(dt.Rows[0][0].ToString());
            result.LicenseNumber = Int64.Parse(dt.Rows[0][1].ToString());
            result.LicenseName = dt.Rows[0][2].ToString();
            result.Issuer = dt.Rows[0][3].ToString();
            result.Date = DateTime.Parse(dt.Rows[0][4].ToString());
            result.ExpireDate = DateTime.Parse(dt.Rows[0][5].ToString());
            result.Type = (bool)dt.Rows[0][6];
            return result;
        }
    }
}
