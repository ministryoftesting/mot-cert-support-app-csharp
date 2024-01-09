using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Timesheet.Models.Auth
{
    public class Token
    {
         public string token { get; set; } = string.Empty;

         public Token(string token)
         {
             this.token = token;
         }
    }
}