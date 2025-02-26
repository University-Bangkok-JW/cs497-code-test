using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class DB_Json : IDatabaseManager
{
    private string dataFolder = "data";
    private string filePath;

    public DB_Json()
    {
        EnsureJsonFileExists();
    }

    private void EnsureJsonFileExists()
    {
        if (!Directory.Exists(dataFolder))
        {
            Console.WriteLine("Creating data directory...");
            Directory.CreateDirectory(dataFolder);
        }

        filePath = Path.Combine(dataFolder, "users.json");

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Creating data/users.json...");
            File.WriteAllText(filePath, "[]"); // Empty JSON array
        }
    }

    public void SaveUser(User user)
    {
        List<User> users = new List<User>();

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            users = JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
        }

        users.Add(user);
        File.WriteAllText(filePath, JsonConvert.SerializeObject(users, Formatting.Indented));
    }
}
