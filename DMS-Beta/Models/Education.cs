using System;

namespace DMS_Beta.Models
{
    public class Education
    {
        private Int64 id;
        private string employename;
        private int employeeid;
        private string coursename;
        private string departmanname;
        private string institute;
        private DateTime begindate;
        private DateTime enddate;
        private string status;
        private UInt64 cost;
        private string goal;

        public string Goal
        {
            get { return goal; }
            set { goal = value; }
        }
        public UInt64 Cost
        {
            get { return cost; }
            set { cost = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public DateTime EndDate
        {
            get { return enddate; }
            set { enddate = value; }
        }
        public DateTime BeginDate
        {
            get { return begindate; }
            set { begindate = value; }
        }
        public string Institute
        {
            get { return institute; }
            set { institute = value; }
        }
        public string DepartmanName
        {
            get { return departmanname; }
            set { departmanname = value; }
        }
        public string CourseName
        {
            get { return coursename; }
            set { coursename = value; }
        }
        public int EmployeeId
        {
            get { return employeeid; }
            set { employeeid = value; }
        }
        public string EmployeName
        {
            get { return employename; }
            set { employename = value; }
        }
        public Int64 ID
        {
            get { return id; }
            set { id = value; }
        }
        public Education()
        {

        }
    }
}
