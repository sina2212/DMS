using System;
using System.Collections.Generic;

namespace DMS_Beta.Models
{
    public class Interview:Person
    {
        private Int64 i_code;
        private string position;
        private DateTime date;
        private int interviewee;
        private string result;
        private string introduction;
        private bool trial;
        private DateTime trainingdate;
        private string trainingresult;
        private bool educational;
        private DateTime educationdate;
        private string educationresult;

        public string EducationResult
        {
            get { return educationresult; }
            set { educationresult = value; }
        }
        public DateTime Educationdate
        {
            get { return educationdate; }
            set { educationdate = value; }
        }
        public bool Educational
        {
            get { return educational; }
            set { educational = value; }
        }
        public string TrainingResult
        {
            get { return trainingresult; }
            set { trainingresult = value; }
        }
        public DateTime TrainingDate
        {
            get { return trainingdate; }
            set { trainingdate = value; }
        }
        public bool Training
        {
            get { return trial; }
            set { trial = value; }
        }
        public string Introduction
        {
            get { return introduction; }
            set { introduction = value; }
        }
        public string Result
        {
            get { return result; }
            set { result = value; }
        }
        public int InterViewee
        {
            get { return interviewee; }
            set { interviewee = value; }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public string Position
        {
            get { return position; }
            set { position = value; }
        }
        public Int64 I_Code
        {
            get { return i_code; }
            set { i_code = value; }
        }
        public Interview()
        {

        }
    }
}
