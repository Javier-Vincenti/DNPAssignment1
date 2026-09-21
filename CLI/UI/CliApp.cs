using RepositoryContracts;

namespace CLI;

public class CliApp
{
    private readonly IUserRepository userRepository;
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    public CliApp(
        IUserRepository userRepository,
        IPostRepository postRepository,
        ICommentRepository commentRepository)
    {
        this.userRepository = userRepository;
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("\nMain menu");
            Console.WriteLine("1. Manage users");
            Console.WriteLine("2. Manage posts");
            Console.WriteLine("3. Add comment");
            Console.WriteLine("0. Exit");

            Console.Write("Choose an option: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ManageUsersView usersView =
                        new ManageUsersView(userRepository);

                    await usersView.ShowAsync();
                    break;

                case "2":
                    ManagePostsView postsView =
                        new ManagePostsView(
                            postRepository,
                            commentRepository);

                    await postsView.ShowAsync();
                    break;

                case "3":
                    CreateCommentView commentView =
                        new CreateCommentView(commentRepository);

                    await commentView.ShowAsync();
                    break;

                case "0":
                    Console.WriteLine("Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}