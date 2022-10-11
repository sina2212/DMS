using System;

namespace DMS_Beta.Models
{
    public class KnowledgeManagement
    {
        private UInt64 kmid;
        private string senario;
        private string keyword;
        private DateTime date;
        private string client;
        private int emplyer;

        public UInt64 KMID
        {
            get { return kmid; }
            set { kmid = value; }
        }
        public int Employer
        {
            get { return emplyer; }
            set { emplyer = value; }
        }

        public string Client
        {
            get { return client; }
            set { client = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public string KeyWord
        {
            get { return keyword; }
            set { keyword = value; }
        }

        public string Senario
        {
            get { return senario; }
            set { senario = value; }
        }

        public KnowledgeManagement()
        {

        }
    }
}
