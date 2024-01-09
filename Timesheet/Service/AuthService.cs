using Timesheet.Models.Auth;
using Timesheet.DB;

namespace Timesheet.Service
{
    
        public class AuthService
        {
    
            private IAuthDB authDB;
    
            public AuthService() 
            {
                authDB = new AuthDB();
            }
    
            public AuthService(IAuthDB authDB) 
            {
                this.authDB = authDB;
            }
    
            public (int code, Credentials credentials) Login(string email, string password) 
            {
                LoginResult loginResult = authDB.CheckLogin(email, password);

                if(loginResult.isUser){
                    string tokenString = new RandomString(6, new Random()).NextString();

                    DateTime date = DateTime.Now;
                    date = date.AddDays(1);

                    Credentials credentials = authDB.GenerateSession(tokenString, loginResult.userType, date);
                    credentials.Id = loginResult.id;

                    return (200, credentials);
                } else {
                    return (401, null);
                }
            }
    
            public bool CheckSession(string token, DateTime date) 
            {
                return authDB.CheckSession(token, date);
            }
    
            public int Logout(String token) 
            {
                if(authDB.DeleteSession(token))
                    return 202;
                else
                    return 404;
            }
    
        }
}