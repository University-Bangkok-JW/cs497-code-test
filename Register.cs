using System.Collections.Generic;

public class Register
{
    private List<User> users;

    public Register(List<User> userList)
    {
        users = userList;
    }

    public bool RegisterUser(string username, string email, string password, string cpassword)
    {
        if (password != cpassword)
        {
            return false;
        }

        if (users.Exists(u => u.Email == email || u.Username == username))
        {
            return false;
        }

        User newUser = new User(username, email, password);
        users.Add(newUser);
        return true;
    }
}
