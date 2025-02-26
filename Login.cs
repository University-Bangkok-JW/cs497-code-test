using System.Collections.Generic;
using System.Linq;

public class Login
{
    private List<User> users;

    public Login(List<User> userList)
    {
        users = userList;
    }

    public bool CheckUserExists(string emailOrUsername)
    {
        return users.Any(user => user.Email == emailOrUsername || user.Username == emailOrUsername);
    }

    public bool AuthenticateUser(string emailOrUsername, string password)
    {
        User user = users.FirstOrDefault(u => u.Email == emailOrUsername || u.Username == emailOrUsername);
        if (user == null) return false;

        return Utilities.VerifyPassword(password, user.Password);
    }
}
