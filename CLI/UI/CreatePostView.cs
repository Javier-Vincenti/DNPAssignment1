using Entities;
using RepositoryContracts;

namespace CLI;

public class CreateUserView
{
    private readonly IUserRepository userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        Console.Write("Username: ");
        string username = Console.ReadLine();

        Console.Write("Password: ");
        string password = Console.ReadLine();

        User user = new User
        {
            UserName = username,
            Password = password
        };

        await userRepository.AddAsync(user);

        Console.WriteLine("User created.");
    }
}