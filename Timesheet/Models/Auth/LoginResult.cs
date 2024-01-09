using Newtonsoft.Json;

namespace Timesheet.Models.Auth
{

    public class LoginResult
    {

        public bool isUser { get; set; }
        public string userType { get; set; } = string.Empty;
        public int id { get; set; }

        public LoginResult(bool isUser, string userType, int id)
        {
            this.isUser = isUser;
            this.userType = userType;
            this.id = id;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }

}