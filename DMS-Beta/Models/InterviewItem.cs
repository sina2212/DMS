
using System;

namespace DMS_Beta.Models
{
    public class InterviewItem
    {
        private Int64 interviewref;
        private string name;
        private string result;
        private string comment;

        public string Result
        {
            get { return result; }
            set { result = value; }
        }

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Int64 InterViewref
        {
            get { return interviewref; }
            set { interviewref = value; }
        }

    }
}
