using System.Reflection;
using Microsoft.Data.Sqlite;

namespace Timesheet.DB
{

    public class PrepareDB
    {
        
        public PrepareDB()
        {
            string sqlScript = @"
                CREATE TABLE IF NOT EXISTS PROJECTS ( projectid INTEGER PRIMARY KEY, name TEXT NOT NULL, description TEXT NOT NULL);
                CREATE TABLE IF NOT EXISTS ENTRIES ( entryid INTEGER PRIMARY KEY, projectid INTEGER NOT NULL, date TEXT NOT NULL, hours INTEGER NOT NULL, description TEXT NOT NULL);
                CREATE TABLE IF NOT EXISTS USERS ( userid INTEGER PRIMARY KEY, username TEXT NOT NULL, email TEXT NOT NULL, password TEXT NOT NULL, role TEXT NOT NULL);
                CREATE TABLE IF NOT EXISTS SESSIONS ( tokenid TEXT PRIMARY KEY, token TEXT NOT NULL, admin BOOLEAN NOT NULL, expiry DATETIME NOT NULL);

                DELETE FROM PROJECTS;
                DELETE FROM ENTRIES;
                DELETE FROM USERS;
                DELETE FROM SESSIONS;
            ";

            ExecuteSqlFile(sqlScript);
        }

        public void SeedUsers()
        {
            string sqlScript = @"
                INSERT INTO USERS (username, email, password, role) VALUES ('admin', 'admin@test.com', 'password123', 'admin');
                INSERT INTO USERS (username, email, password, role) VALUES ('user', 'user@test.com', 'password123', 'user');
            ";

            ExecuteSqlFile(sqlScript);
        }

        public void SeedProject()
        {
            string sqlScript = @"
                INSERT INTO PROJECTS (name, description) VALUES ('Project 1', 'This is a brief description of Project 1');
                INSERT INTO ENTRIES (projectid, date, hours, description) VALUES (1, '2023-01-01', 8, 'Ate cake');
            ";

            ExecuteSqlFile(sqlScript);
        }

        private void ExecuteSqlFile(string sqlScript)
        {
            using (var connection = new SqliteConnection("Data Source=timesheet.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = sqlScript;
                command.ExecuteNonQuery();
            }
        }

    }

}