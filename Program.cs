using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Load configuration
        ConfigManager config = new ConfigManager();
        IDatabaseManager dbManager;

        // Choose database type
        if (config.DbType.ToLower() == "json")
        {
            Console.WriteLine("Using JSON database.");
            dbManager = new DB_Json();
        }
        else
        {
            Console.WriteLine("Using SQLite database.");
            dbManager = new DB_Sqlite();
        }

        // Initialize user list
        List<User> users = new List<User>();
        Register register = new Register(users);
        Login login = new Login(users);

        // Example usage
        Console.WriteLine("Registering user...");
        bool registered = register.RegisterUser("testuser", "test@example.com", "password123", "password123");

        if (registered)
        {
            User newUser = new User("testuser", "test@example.com", "password123");
            dbManager.SaveUser(newUser);
            Console.WriteLine("User registered and saved successfully.");
        }
        else
        {
            Console.WriteLine("Registration failed.");
        }

        Console.WriteLine("Logging in...");
        bool authenticated = login.AuthenticateUser("testuser", "password123");

        if (authenticated)
        {
            Console.WriteLine("Login successful.");
        }
        else
        {
            Console.WriteLine("Invalid credentials.");
        }
    }
}
