using RepositoryContracts;

namespace CLI;

public class ManageUsersView
{
    private readonly IUserRepository userRepository;

    public ManageUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        while (true)
        {
            Console.WriteLine("\nManage users");
            Console.WriteLine("1. Create user");
            Console.WriteLine("0. Back");

            Console.Write("Choose an option: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    CreateUserView view =
                        new CreateUserView(userRepository);

                    await view.ShowAsync();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}