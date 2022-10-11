using System;

namespace DMS_Beta.Models
{
    public class Vacation
    {
        private int id;
        private string realname;
        private string departmanname;
        private int duration;
        private DateTime fromdate;
        private DateTime todate;
        private DateTime date_;
        private bool status;
        private bool type;

        public bool Type
        {
            get { return type; }
            set { type = value; }
        }

        public bool Status
        {
            get { return status; }
            set { status = value; }
        }

        public DateTime Date_
        {
            get { return date_; }
            set { date_ = value; }
        }

        public DateTime Todate
        {
            get { return todate; }
            set { todate = value; }
        }

        public DateTime From
        {
            get { return fromdate; }
            set { fromdate = value; }
        }

        public int Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        public string Departman
        {
            get { return departmanname; }
            set { departmanname = value; }
        }

        public string RealName
        {
            get { return realname; }
            set { realname = value; }
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

    }
}
