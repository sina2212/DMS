using System;
using System.Windows.Media;

namespace DMS_Beta.Models
{
    public class Employee : Person
    {
        private int empcode;
        private string depname;
        private string position;
        private string jobtitle;
        private Attachment avatar;
        private DateTime begin;
        private DateTime end;
        private Int64 contract;

        public DateTime EndEmployment
        {
            get { return end; }
            set { end = value; }
        }
        public DateTime BeginEmployment
        {
            get { return begin; }
            set { begin = value; }
        }
        public Attachment Avatar
        {
            get { return avatar; }
            set { avatar = value; }
        }
        public string JobPosition
        {
            get { return position; }
            set { position = value; }
        }
        public string JobTitle
        {
            get { return jobtitle; }
            set { jobtitle = value; }
        }
        public string DepName
        {
            get { return depname; }
            set { depname = value; }
        }
        public int EmpCode
        {
            get { return empcode; }
            set { empcode = value; }
        }
        public Int64 Contract
        {
            get { return contract; }
            set { contract = value; }
        }
        public Employee()
        {

        }
    }
}
