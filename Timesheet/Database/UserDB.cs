

using Timesheet.Models.User;
using Microsoft.Data.Sqlite;

namespace Timesheet.DB
{

    public interface IUserDB
    {
        User CreateUser(User user);
        bool DeleteUser(int userId);
        User GetUserProfile(int userId);
        bool UpdateUser(int userId, User user);
        List<User> GetUsers();
    }

    public class UserDB : IUserDB
    {
        public User CreateUser(User user)
        {
            using (var connection = new SqliteConnection("Data Source=timesheet.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    INSERT INTO USERS (username, email, password, role)
                    VALUES ($username, $email, $password, $role)
                ";
                command.Parameters.AddWithValue("$username", user.Username);
                command.Parameters.AddWithValue("$email", user.Email);
                command.Parameters.AddWithValue("$password", user.Password);
                command.Parameters.AddWithValue("$role", user.Role);
                command.ExecuteNonQuery();

                var queryCommand = connection.CreateCommand();
                queryCommand.CommandText =
                @"
                    SELECT userid, username, email, password, role
                    FROM USERS
                    WHERE userid = last_insert_rowid();
                ";
                using (var reader = queryCommand.ExecuteReader())
                {
                    reader.Read();
                    return new User(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4)
                    );
                }
            }
        }

        public bool DeleteUser(int userId)
        {
            using (var connection = new SqliteConnection("Data Source=timesheet.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    DELETE FROM USERS
                    WHERE userid = $userId
                ";
                command.Parameters.AddWithValue("$userId", userId);
                return command.ExecuteNonQuery() > 0;
            }
        }

        public User GetUserProfile(int userId)
        {
            using (var connection = new SqliteConnection("Data Source=timesheet.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT userid, username, email, password, role
                    FROM USERS
                    WHERE userid = $userId
                ";
                command.Parameters.AddWithValue("$userId", userId);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetString(4)
                        );
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public bool UpdateUser(int userId, User user)
        {
            using (var connection = new SqliteConnection("Data Source=timesheet.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    UPDATE USERS
                    SET username = $username, email = $email, password = $password, role = $role
                    WHERE userid = $userId
                ";
                command.Parameters.AddWithValue("$username", user.Username);
                command.Parameters.AddWithValue("$email", user.Email);
                command.Parameters.AddWithValue("$password", user.Password);
                command.Parameters.AddWithValue("$role", user.Role);
                command.Parameters.AddWithValue("$userId", userId);
                return command.ExecuteNonQuery() > 0;
            }
        }

        public List<User> GetUsers()
        {
            using (var connection = new SqliteConnection("Data Source=timesheet.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT userid, username, email, password, role
                    FROM USERS
                ";
                using (var reader = command.ExecuteReader())
                {
                    List<User> users = new List<User>();
                    while (reader.Read())
                    {
                        users.Add(new User(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetString(4)
                        ));
                    }
                    return users;
                }
            }
        }

    }

}