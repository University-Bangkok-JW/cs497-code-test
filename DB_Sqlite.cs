using System;
using System.Data.SQLite;
using System.IO;

public class DB_Sqlite : IDatabaseManager
{
    private string dataFolder = "data";
    private string dbFilePath;

    public DB_Sqlite()
    {
        EnsureDatabaseExists();
    }

    private void EnsureDatabaseExists()
    {
        if (!Directory.Exists(dataFolder))
        {
            Console.WriteLine("Creating data directory...");
            Directory.CreateDirectory(dataFolder);
        }

        dbFilePath = Path.Combine(dataFolder, "users.db");

        if (!File.Exists(dbFilePath))
        {
            Console.WriteLine("Creating data/users.db...");
            SQLiteConnection.CreateFile(dbFilePath);
            InitializeDatabase();
        }
    }

    private void InitializeDatabase()
    {
        using (var connection = new SQLiteConnection($"Data Source={dbFilePath};Version=3;"))
        {
            connection.Open();
            string tableCreation = @"CREATE TABLE IF NOT EXISTS users (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                username TEXT UNIQUE,
                email TEXT UNIQUE,
                password TEXT
            );";
            using (var command = new SQLiteCommand(tableCreation, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public void SaveUser(User user)
    {
        using (var connection = new SQLiteConnection($"Data Source={dbFilePath};Version=3;"))
        {
            connection.Open();
            string query = "INSERT INTO users (username, email, password) VALUES (@username, @email, @password)";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", user.Username);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@password", user.Password);
                command.ExecuteNonQuery();
            }
        }
    }
}
