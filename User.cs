public class User
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; private set; }

    public User(string username, string email, string password)
    {
        Username = username;
        Email = email;
        Password = Utilities.HashPassword(password);
    }
}
