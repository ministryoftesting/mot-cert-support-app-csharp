using Microsoft.Data.Sqlite;

namespace Timesheet.DB
{

    public class PrepareDB
    {

        public PrepareDB()
        {
            using (var connection = new SqliteConnection("Data Source=timesheet.db"))
            {
                connection.Open();

                var projectTableCommand = connection.CreateCommand();
                projectTableCommand.CommandText =
                @"
                    CREATE TABLE IF NOT EXISTS PROJECTS (
                        projectid INTEGER PRIMARY KEY,
                        name TEXT NOT NULL,
                        description TEXT NOT NULL
                    );
                ";
                projectTableCommand.ExecuteNonQuery();

                var entryTableCommand = connection.CreateCommand();
                entryTableCommand.CommandText =
                @"
                    CREATE TABLE IF NOT EXISTS ENTRIES (
                        entryid INTEGER PRIMARY KEY,
                        projectid INTEGER NOT NULL,
                        date TEXT NOT NULL,
                        hours INTEGER NOT NULL,
                        description TEXT NOT NULL
                    );
                ";
                entryTableCommand.ExecuteNonQuery();

                var userTableCommand = connection.CreateCommand();
                userTableCommand.CommandText =
                @"
                    CREATE TABLE IF NOT EXISTS USERS (
                        userid INTEGER PRIMARY KEY,
                        username TEXT NOT NULL,
                        email TEXT NOT NULL,
                        password TEXT NOT NULL,
                        role TEXT NOT NULL
                    );
                ";
                userTableCommand.ExecuteNonQuery();

                var sessionTableCommand = connection.CreateCommand();
                sessionTableCommand.CommandText =
                @"
                    CREATE TABLE IF NOT EXISTS SESSIONS (
                        tokenid TEXT PRIMARY KEY,
                        token TEXT NOT NULL,
                        admin BOOLEAN NOT NULL,
                        expiry DATETIME NOT NULL
                    );
                ";
                sessionTableCommand.ExecuteNonQuery();

                var deleteTableCommand = connection.CreateCommand();
                deleteTableCommand.CommandText =
                @"
                    DELETE FROM PROJECTS;
                    DELETE FROM ENTRIES;
                    DELETE FROM USERS;
                    DELETE FROM SESSIONS;
                ";
                deleteTableCommand.ExecuteNonQuery();
            }
        }

        public void SeedUsers(){
            using (var connection = new SqliteConnection("Data Source=timesheet.db"))
            {
                connection.Open();
                
                var adminInsertCommand = connection.CreateCommand();
                adminInsertCommand.CommandText =
                @"
                    INSERT INTO USERS (username, email, password, role)
                    VALUES ($username, $email, $password, $role)
                ";
                adminInsertCommand.Parameters.AddWithValue("$username", "admin");
                adminInsertCommand.Parameters.AddWithValue("$email", "admin@test.com");
                adminInsertCommand.Parameters.AddWithValue("$password", "password123");
                adminInsertCommand.Parameters.AddWithValue("$role", "admin");
                adminInsertCommand.ExecuteNonQuery();

                var userInsertCommand = connection.CreateCommand();
                userInsertCommand.CommandText =
                @"
                    INSERT INTO USERS (username, email, password, role)
                    VALUES ($username, $email, $password, $role)
                ";
                userInsertCommand.Parameters.AddWithValue("$username", "user");
                userInsertCommand.Parameters.AddWithValue("$email", "user@test.com");
                userInsertCommand.Parameters.AddWithValue("$password", "password123");
                userInsertCommand.Parameters.AddWithValue("$role", "user");
                userInsertCommand.ExecuteNonQuery();
            }
        }

        public void SeedProject(){
            using (var connection = new SqliteConnection("Data Source=timesheet.db"))
            {
                connection.Open();

                var insertProjectCommand = connection.CreateCommand();
                insertProjectCommand.CommandText =
                @"
                    INSERT INTO PROJECTS (name, description)
                    VALUES ($name, $description)
                ";
                insertProjectCommand.Parameters.AddWithValue("$name", "Project 1");
                insertProjectCommand.Parameters.AddWithValue("$description", "This is a brief description of Project 1");
                insertProjectCommand.ExecuteNonQuery();

                var insertEntryCommand = connection.CreateCommand();
                insertEntryCommand.CommandText =
                @"
                    INSERT INTO ENTRIES (projectid, date, hours, description)
                    VALUES ($projectid, $date, $hours, $description)
                ";

                insertEntryCommand.Parameters.AddWithValue("$projectid", 1);
                insertEntryCommand.Parameters.AddWithValue("$date", "2023-01-01");
                insertEntryCommand.Parameters.AddWithValue("$hours", 8);
                insertEntryCommand.Parameters.AddWithValue("$description", "Ate cake");
                insertEntryCommand.ExecuteNonQuery();
            }
        }

    }

}