namespace DMS_Beta.Models
{
    public class Notification
    {
        private string tittle;
        private string subtitle;

        public string SubTitle
        {
            get { return subtitle; }
            set { subtitle = value; }
        }

        public string Title
        {
            get { return tittle; }
            set { tittle = value; }
        }
        public Notification()
        {

        }
    }
}
