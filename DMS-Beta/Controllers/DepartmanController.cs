using DMS_Beta.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DMS_Beta.Controllers
{
    class DepartmanController
    {
        public DataTable ShowDepartmans()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select code,name from Departmans";
            return Database.Show(cmd);
        }

        public string Save(Departman departman)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;

            p = new SqlParameter("@depname", SqlDbType.NVarChar, 100);
            p.Value = departman.Name;
            parameters.Add(p);
            p = new SqlParameter("@depcode", SqlDbType.Int);
            p.Value = departman.Code;
            parameters.Add(p);

            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SaveDepartman";
            return Database.Save(cmd, parameters);
        }

        public List<Employee> ShowDepartmanEmployee(int depcode)
        {
            List<Employee> dep_emps = new List<Employee>();
            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter p = new SqlParameter("@code", SqlDbType.Int);
            p.Value = depcode;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Emp_ref, realname, filebuffer, position from DepartmansItem " +
                "left outer join Departmans on departman_ref = code " +
                "left outer join Employment on number = Emp_ref " +
                "left outer join Person on person_ref = Person.ID " +
                "left outer join Attachments on categoryid = Emp_ref where subcategory = 'cover' and code = @code " +
                "order by Emp_ref";

            DataTable dt = Database.Showbyparams(cmd, parameters);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Employee e = new Employee();
                e.RealName = dt.Rows[i][1].ToString();
                e.EmpCode = int.Parse(dt.Rows[i][0].ToString());
                e.JobPosition = dt.Rows[i][3].ToString();
                e.Avatar = new Attachment();
                e.Avatar.Filebuffer = (byte[])dt.Rows[i][2];
                dep_emps.Add(e);
            }
            return dep_emps;
        }

        public List<Employee> ShowDepartmannotEmployee(int depcode)
        {
            List<Employee> dep_emps = new List<Employee>();
            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter p = new SqlParameter("@code", SqlDbType.Int);
            p.Value = depcode;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Emp_ref, realname, filebuffer from DepartmansItem " +
                "left outer join Departmans on departman_ref = code " +
                "left outer join Employment on number = Emp_ref " +
                "left outer join Person on person_ref = Person.ID " +
                "left outer join Attachments on categoryid = Emp_ref where subcategory = 'cover' and code != @code " +
                "order by Emp_ref";

            DataTable dt = Database.Showbyparams(cmd, parameters);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Employee e = new Employee();
                e.RealName = dt.Rows[i][1].ToString();
                e.EmpCode = int.Parse(dt.Rows[i][0].ToString());
                e.Avatar = new Attachment();
                e.Avatar.Filebuffer = (byte[])dt.Rows[i][2];
                dep_emps.Add(e);
            }
            return dep_emps;
        }

        public DataTable showdepartmanAttachments(int depcode)
        {
            SqlCommand cmd1 = new SqlCommand();
            List<SqlParameter> sqlParameters1 = new List<SqlParameter>();
            SqlParameter parameter1 = new SqlParameter("@code", SqlDbType.Int);
            parameter1.Value = depcode;
            sqlParameters1.Add(parameter1);
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "SELECT dbo.Attachments.filename, dbo.Attachments.filelocation, dbo.Attachments.categoryid, dbo.Attachments.category, dbo.Attachments.subcategory, dbo.Employment.number, " +
                "dbo.Person.realname " +
                "FROM dbo.Departmans INNER JOIN " +
                "dbo.DepartmansItem ON dbo.Departmans.code = dbo.DepartmansItem.departman_ref INNER JOIN " +
                "dbo.Employment INNER JOIN " +
                "dbo.Attachments ON dbo.Employment.number = dbo.Attachments.categoryid ON dbo.DepartmansItem.Emp_ref = dbo.Employment.number INNER JOIN " +
                "dbo.Person ON dbo.Employment.person_ref = dbo.Person.ID " +
                "where (code = @code)";
            DataTable dt1 = new DataTable();
            dt1 = Database.Showbyparams(cmd1, sqlParameters1);
            return dt1;
        }

        public string ToEmploy(int emp_code, int depcode, string header)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;
            
            p = new SqlParameter("@emp_ref", SqlDbType.Int);
            p.Value = emp_code;
            parameters.Add(p);

            p = new SqlParameter("@dep_ref", SqlDbType.Int);
            p.Value = depcode;
            parameters.Add(p);

            p = new SqlParameter("@header", SqlDbType.NVarChar, 50);
            p.Value = header;
            parameters.Add(p);

            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

             SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ToEmploy";
            return Database.Save(cmd, parameters);
        }
    }
}
