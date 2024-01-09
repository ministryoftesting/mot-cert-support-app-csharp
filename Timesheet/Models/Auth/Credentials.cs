namespace Timesheet.Models.Auth
{
    public class Credentials
    {
        private string token = string.Empty;
        private bool admin;
        private int id;

        public Credentials()
        {
            // Jackson deserialization
        }

        public Credentials(string token, bool admin)
        {
            this.token = token;
            this.admin = admin;
        }
        

        public string Token
        {
            get { return token; }
            set { token = value; }
        }

        public bool Admin
        {
            get { return admin; }
            set { admin = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public override string ToString()
        {
            return $"Credentials{{token='{token}', admin={admin}, id={id}}}";
        }
    }
}
