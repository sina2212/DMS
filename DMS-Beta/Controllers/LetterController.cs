using DMS_Beta.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DMS_Beta.Controllers
{
    public class LetterController
    {
        public string SaveLetter(Letter letter)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;

            p = new SqlParameter("@subject", SqlDbType.NVarChar, 50);
            p.Value = letter.Subject;
            parameters.Add(p);
            p = new SqlParameter("@date", SqlDbType.Date);
            p.Value = letter.Date;
            parameters.Add(p);
            p = new SqlParameter("@lettertype", SqlDbType.Bit);
            p.Value = letter.LetterType;
            parameters.Add(p);
            p = new SqlParameter("@organ", SqlDbType.NVarChar, 50);
            p.Value = letter.OrganName;
            parameters.Add(p);
            p = new SqlParameter("@senderorreciever", SqlDbType.NVarChar, 50);
            p.Value = letter.RecieverorSender;
            parameters.Add(p);
            p = new SqlParameter("@answer", SqlDbType.BigInt);
            p.Value = letter.Answer;
            parameters.Add(p);
            p = new SqlParameter("@type", SqlDbType.NVarChar, 50);
            p.Value = letter.SendOrRecieveType;
            parameters.Add(p);
            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SaveLetter";
            return Database.Save(cmd, parameters);
        }

        public string UpdateLetter(Letter letter)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;

            p = new SqlParameter("@letternumber", SqlDbType.BigInt);
            p.Value = letter.LetterNumber;
            parameters.Add(p);
            p = new SqlParameter("@subject", SqlDbType.NVarChar, 50);
            p.Value = letter.Subject;
            parameters.Add(p);
            p = new SqlParameter("@date", SqlDbType.Date);
            p.Value = letter.Date;
            parameters.Add(p);
            p = new SqlParameter("@lettertype", SqlDbType.Bit);
            p.Value = letter.LetterType;
            parameters.Add(p);
            p = new SqlParameter("@organ", SqlDbType.NVarChar, 50);
            p.Value = letter.OrganName;
            parameters.Add(p);
            p = new SqlParameter("@senderorreciever", SqlDbType.NVarChar, 50);
            p.Value = letter.RecieverorSender;
            parameters.Add(p);
            p = new SqlParameter("@answer", SqlDbType.BigInt);
            p.Value = letter.Answer;
            parameters.Add(p);
            p = new SqlParameter("@type", SqlDbType.NVarChar, 50);
            p.Value = letter.SendOrRecieveType;
            parameters.Add(p);
            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UpdateLetter";
            return Database.Save(cmd, parameters);
        }

        public Letter ShowLetter(Int64 interviewid)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter parameter;
            Letter res = new Letter();

            parameter = new SqlParameter("@Letternumber", SqlDbType.BigInt);
            parameter.Value = interviewid;
            parameters.Add(parameter);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ShowLetter";
            DataTable dt = Database.Showbyparams(cmd, parameters);

            res.LetterNumber = Int64.Parse(dt.Rows[0][0].ToString());
            res.Subject = dt.Rows[0][1].ToString();
            res.OrganName = dt.Rows[0][2].ToString();
            res.Date = DateTime.Parse(dt.Rows[0][3].ToString());
            res.SendOrRecieveType = dt.Rows[0][4].ToString();
            res.RecieverorSender = dt.Rows[0][5].ToString();
            res.Answer = Int64.Parse(dt.Rows[0][6].ToString());
            res.LetterType = dt.Rows[0][7].ToString() == "ارسال" ? true : false;
            return res;
        }
    }
}
