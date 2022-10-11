using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS_Beta.Models
{
    public class AddressModel
    {
        private string state;
        private string city;
        private string address;
        private int person_ref;
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public int Person_ref
        {
            get { return person_ref; }
            set { person_ref = value; }
        }
        public string FullAddress
        {
            get { return address; }
            set { address = value; }
        }
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        public string State
        {
            get { return state; }
            set { state = value; }
        }
    }
}
