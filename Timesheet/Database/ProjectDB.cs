using Timesheet.Models.Project;
using Microsoft.Data.Sqlite;

namespace Timesheet.DB
{
    
    public interface IProjectDB
    {
        List<Project> GetProjects();
        ProjectDetails GetProject(int id);
        int CreateProject(Project project);
        bool DeleteProject(int id);
        bool DeleteEntry(int projectId, int entryId);
        int StoreEntry(int projectId, Entry entry);
    }

    public class ProjectDB : IProjectDB
    {

        public List<Project> GetProjects()
        {
            using (var connection = new SqliteConnection("Data Source=timesheet.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT projectid, name, description
                    FROM PROJECTS
                ";

                var reader = command.ExecuteReader();

                List<Project> projects = new List<Project>();

                while (reader.Read())
                {
                    projects.Add(new Project(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2)
                    ));
                }

                return projects;
            }
        }

        public ProjectDetails GetProject(int id)
        {
            using (var connection = new SqliteConnection("Data Source=timesheet.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT name, description
                    FROM PROJECTS
                    WHERE projectid = $id
                ";
                command.Parameters.AddWithValue("$id", id);

                var reader = command.ExecuteReader();

                if (reader.Read())
                {

                    var entryCommand = connection.CreateCommand();
                    entryCommand.CommandText =
                    @"
                        SELECT entryid, date, hours, description
                        FROM ENTRIES
                        WHERE projectid = $id
                    ";
                    entryCommand.Parameters.AddWithValue("$id", id);

                    var entryReader = entryCommand.ExecuteReader();

                    List<Entry> entries = new List<Entry>();

                    while (entryReader.Read())
                    {
                        entries.Add(new Entry(
                            entryReader.GetInt32(0),
                            entryReader.GetDateTime(1),
                            entryReader.GetInt32(2),
                            entryReader.GetString(3)
                        ));
                    }

                    ProjectDetails project = new ProjectDetails(
                        reader.GetString(0),
                        reader.GetString(1),
                        total: entries.Sum(entry => entry.Hours),
                        entries
                    );

                    return project;
                }
                else
                {
                    return null;
                }
            }
        }
        
        public int CreateProject(Project project)
        {
            using (var connection = new SqliteConnection("Data Source=timesheet.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    INSERT INTO PROJECTS (name, description)
                    VALUES ($name, $description)
                ";
                command.Parameters.AddWithValue("$name", project.Name);
                command.Parameters.AddWithValue("$description", project.Description);

                command.ExecuteNonQuery();

                var lastInsertIdCommand = connection.CreateCommand();
                lastInsertIdCommand.CommandText =
                @"
                    SELECT last_insert_rowid();
                ";
                var lastIdReader = lastInsertIdCommand.ExecuteReader();
                lastIdReader.Read();

                return lastIdReader.GetInt32(0);
            }
        }

        public bool DeleteProject(int id)
        {
            using (var connection = new SqliteConnection("Data Source=timesheet.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    DELETE FROM PROJECTS
                    WHERE projectid = $id
                ";
                command.Parameters.AddWithValue("$id", id);

                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteEntry(int projectId, int entryId)
        {
            using (var connection = new SqliteConnection("Data Source=timesheet.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    DELETE FROM ENTRIES
                    WHERE projectid = $projectId
                    AND entryid = $entryId
                ";
                command.Parameters.AddWithValue("$projectId", projectId);
                command.Parameters.AddWithValue("$entryId", entryId);

                return command.ExecuteNonQuery() > 0;
            }
        }

        public int StoreEntry(int projectId, Entry entry)
        {
            using (var connection = new SqliteConnection("Data Source=timesheet.db"))
            {
                connection.Open();

                var insertEntryCommand = connection.CreateCommand();
                insertEntryCommand.CommandText =
                @"
                    INSERT INTO ENTRIES (projectid, date, hours, description)
                    VALUES ($projectid, $date, $hours, $description)
                ";

                insertEntryCommand.Parameters.AddWithValue("$projectid", projectId);
                insertEntryCommand.Parameters.AddWithValue("$date", entry.Date.ToString("yyyy-MM-dd"));
                insertEntryCommand.Parameters.AddWithValue("$hours", entry.Hours);
                insertEntryCommand.Parameters.AddWithValue("$description", entry.Description);
                insertEntryCommand.ExecuteNonQuery();

                var lastInsertIdCommand = connection.CreateCommand();
                lastInsertIdCommand.CommandText =
                @"
                    SELECT last_insert_rowid();
                ";
                var lastIdReader = lastInsertIdCommand.ExecuteReader();
                lastIdReader.Read();

                return lastIdReader.GetInt32(0);
            }
        }

        public void Reset(){
            using (var connection = new SqliteConnection("Data Source=timesheet.db"))
            {
                connection.Open();

                var deleteEntriesCommand = connection.CreateCommand();
                deleteEntriesCommand.CommandText =
                @"
                    DELETE FROM ENTRIES
                ";
                deleteEntriesCommand.ExecuteNonQuery();

                var deleteProjectsCommand = connection.CreateCommand();
                deleteProjectsCommand.CommandText =
                @"
                    DELETE FROM PROJECTS
                ";
                deleteProjectsCommand.ExecuteNonQuery();

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