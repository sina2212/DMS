using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS_Beta.Models
{
    public class PhoneModel
    {
        private int person_ref;
        private string phone;
        private string title;
        private string subtitle;

        public string Subtitle
        {
            get { return subtitle; }
            set { subtitle = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string PhoneNumber
        {
            get { return phone; }
            set { phone = value; }
        }
        public int Person_ref
        {
            get { return person_ref; }
            set { person_ref = value; }
        }
        public PhoneModel()
        {

        }
    }
}
