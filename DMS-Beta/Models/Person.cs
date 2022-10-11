using System;

namespace DMS_Beta.Models
{
    public class Person
    {
        private int code;
        private string realname;
        private bool gender;
        private string nationalnumber;
        private Int64 certificatenumber;
        private string education;
        private DateTime birthdate;
        private bool singel;
        private int child;

        public int Child
        {
            get { return child; }
            set { child = value; }
        }
        public bool Status
        {
            get { return singel; }
            set { singel = value; }
        }
        public DateTime BirthDate
        {
            get { return birthdate; }
            set { birthdate = value; }
        }
        public string Education
        {
            get { return education; }
            set { education = value; }
        }
        public bool Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        public Int64 CertificateNumber
        {
            get { return certificatenumber; }
            set { certificatenumber = value; }
        }
        public string NationalNumber
        {
            get { return nationalnumber; }
            set { nationalnumber = value; }
        }
        public string RealName
        {
            get { return realname; }
            set { realname = value; }
        }
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        public Person()
        {

        }
    }
}
