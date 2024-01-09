using Microsoft.Data.Sqlite;
using Timesheet.Models.Auth;

namespace Timesheet.DB
{
    public interface IAuthDB
    {
        LoginResult CheckLogin(string email, string password);
        
        Credentials GenerateSession(string token, string userType, DateTime expiry);

        bool DeleteSession(string token);

        bool CheckSession(string token, DateTime date);
    }

    public class AuthDB : IAuthDB
    {

        public LoginResult CheckLogin(string email, string password)
        {
            using (var connection = new SqliteConnection("Data Source=timesheet.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT userid, username, email, role
                    FROM USERS
                    WHERE email = $email AND password = $password
                ";
                command.Parameters.AddWithValue("$email", email);
                command.Parameters.AddWithValue("$password", password);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new LoginResult(
                            true,
                            reader.GetString(3),
                            reader.GetInt32(1)
                        );
                    }
                    else
                    {
                        return new LoginResult(false, "", 0);
                    }
                }
            }
        }

        public Credentials GenerateSession(string token, string userType, DateTime expiry)
        {
            using (var connection = new SqliteConnection("Data Source=timesheet.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    INSERT INTO SESSIONS (tokenid, token, admin, expiry)
                    VALUES ($tokenid, $token, $admin, $expiry)
                ";
                command.Parameters.AddWithValue("$tokenid", Guid.NewGuid().ToString());
                command.Parameters.AddWithValue("$token", token);
                command.Parameters.AddWithValue("$admin", userType == "admin");
                command.Parameters.AddWithValue("$expiry", expiry);

                command.ExecuteNonQuery();

                return new Credentials(token, userType.Equals("admin"));
            }
        }

        public bool DeleteSession(string token)
        {
            using (var connection = new SqliteConnection("Data Source=timesheet.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    DELETE FROM SESSIONS
                    WHERE token = $token
                ";
                command.Parameters.AddWithValue("$token", token);

                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool CheckSession(string token, DateTime date)
        {
            using (var connection = new SqliteConnection("Data Source=timesheet.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT expiry
                    FROM SESSIONS
                    WHERE token = $token
                ";
                command.Parameters.AddWithValue("$token", token.Replace("Bearer ", ""));

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetDateTime(0) > date;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
    
}
