



User user1 = new User()
{
    Name = "Sina",
    Point = 60,
    userStates = UserStates.SilverUser
};

Client client = new Client();
client.Run(user1);

Console.ReadKey();


public class Client
{
    public void Run(User user)
    {
        Context context = new Context();
        var r = context.Request(user);

        if (r.Point >= 50)
        {
            Console.WriteLine("Run step 2 process for change user state");

            context.Request(r);
        }
    }
}
public enum UserStates
{
    GoldUser,
    SilverUser
}


public class User
{
    public string Name { get; set; }
    public UserStates userStates { get; set; }
    public int Point { get; set; }
}

public abstract class ChangeUserAbstraction
{
    public abstract User RunProcess(Context context,User user);
}


public class Context
{
    public ChangeUserAbstraction changeUserAbstraction { get; set; }
    public Context()
    {
        changeUserAbstraction = new ChangeUserStateStep1();
    }
    public User Request(User user)
    {
        return changeUserAbstraction.RunProcess(this,user);
    }
}



public class ChangeUserStateStep1 : ChangeUserAbstraction
{
    public override User RunProcess(Context context, User user)
    {
        if (user.Point>50)
        {
            Console.WriteLine("Run Step 2");
            context.changeUserAbstraction = new ChangeUserStateStep2();
            return user;
        }
        Console.WriteLine("User Is Still Silver");
        user.userStates= UserStates.SilverUser;
        return user;
    }
}
public class ChangeUserStateStep2 : ChangeUserAbstraction
{
    public override User RunProcess(Context context, User user)
    {
       user.userStates = UserStates.GoldUser;
        Console.WriteLine("User Is Gold User");
        return user;    
    }
}