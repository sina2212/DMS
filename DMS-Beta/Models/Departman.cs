
using System.Collections.Generic;

namespace DMS_Beta.Models
{
    public class Departman
    {
        private string name;
        private Attachment icon;
        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public Attachment Icon
        {
            get { return icon; }
            set { icon = value; }
        }
        public Departman()
        {

        }
    }
}
