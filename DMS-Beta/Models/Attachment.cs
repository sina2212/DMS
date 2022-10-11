using System;

namespace DMS_Beta.Models
{
    public class Attachment
    {
        private Int64 proccess_ref;
        private string filename;
        private string proccesstype;
        private string proccesssubtype;
        private string location;
        private string filetype;
        private byte[] filebuffer;

        public byte[] Filebuffer
        {
            get { return filebuffer; }
            set { filebuffer = value; }
        }
        public string FileType
        {
            get { return filetype; }
            set { filetype = value; }
        }
        public string Location
        {
            get { return location; }
            set { location = value; }
        }
        public string ProccessSubType
        {
            get { return proccesssubtype; }
            set { proccesssubtype = value; }
        }
        public string Type
        {
            get { return proccesstype; }
            set { proccesstype = value; }
        }
        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }
        public Int64 Proccess
        {
            get { return proccess_ref; }
            set { proccess_ref = value; }
        }

    }
}
