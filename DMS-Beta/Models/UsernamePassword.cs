using System;

namespace DMS_Beta.Models
{
    public class UsernamePassword
    {
        private Int64 id;
        private string username;
        private string password;
        private Int64 titleid;
        private string title;

        public Int64 ID
        {
            get { return id; }
            set { id = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public Int64 TitleID
        {
            get { return titleid; }
            set { titleid = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public UsernamePassword()
        {

        }
    }
}
