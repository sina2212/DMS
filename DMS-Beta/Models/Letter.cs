using System;

namespace DMS_Beta.Models
{
    public class Letter
    {
        private Int64 letternumber;
        private string subject;
        private bool lettertype;
        private string reciverorsender;
        private string organname;
        private string sendorrecievetype;
        private DateTime date;
        private Int64 answer;

        public Int64 Answer
        {
            get { return answer; }
            set { answer = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public string SendOrRecieveType
        {
            get { return sendorrecievetype; }
            set { sendorrecievetype = value; }
        }

        public string RecieverorSender
        {
            get { return reciverorsender; }
            set { reciverorsender = value; }
        }

        public string OrganName
        {
            get { return organname; }
            set { organname = value; }
        }

        public bool LetterType
        {
            get { return lettertype; }
            set { lettertype = value; }
        }

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        public Int64 LetterNumber
        {
            get { return letternumber; }
            set { letternumber = value; }
        }

        public Letter()
        {

        }
    }
}
