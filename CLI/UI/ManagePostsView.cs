using RepositoryContracts;

namespace CLI;

public class ManagePostsView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    public ManagePostsView(
        IPostRepository postRepository,
        ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    public async Task ShowAsync()
    {
        while (true)
        {
            Console.WriteLine("\nManage posts");
            Console.WriteLine("1. Create post");
            Console.WriteLine("2. View posts");
            Console.WriteLine("3. View specific post");
            Console.WriteLine("0. Back");

            Console.Write("Choose an option: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    CreatePostView createPostView =
                        new CreatePostView(postRepository);

                    await createPostView.ShowAsync();
                    break;

                case "2":
                    ListPostsView listPostsView =
                        new ListPostsView(postRepository);

                    listPostsView.Show();
                    break;

                case "3":
                    SinglePostView singlePostView =
                        new SinglePostView(
                            postRepository,
                            commentRepository);

                    await singlePostView.ShowAsync();
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