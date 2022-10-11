using System;

namespace DMS_Beta.Models
{
    public class License
    {
        private Int64 id;
        private Int64 licensenumbber;
        private string licensename;
        private string issuer;
        private DateTime date;
        private bool type;
        private DateTime expiredate;

        public Int64 ID
        {
            get { return id; }
            set { id = value; }
        }
        public DateTime ExpireDate
        {
            get { return expiredate; }
            set { expiredate = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public string LicenseName
        {
            get { return licensename; }
            set { licensename = value; }
        }

        public bool Type
        {
            get { return type; }
            set { type = value; }
        }

        public Int64 LicenseNumber
        {
            get { return licensenumbber; }
            set { licensenumbber = value; }
        }
        public string Issuer
        {
            get { return issuer; }
            set { issuer = value; }
        }
        public License()
        {

        }
    }
}
