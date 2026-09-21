using Entities;
using RepositoryContracts;

namespace CLI;

public class CreatePostView
{
    private readonly IPostRepository postRepository;

    public CreatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task ShowAsync()
    {
        Console.Write("Title: ");
        string title = Console.ReadLine();

        Console.Write("Body: ");
        string body = Console.ReadLine();

        Console.Write("User ID: ");
        int userId = int.Parse(Console.ReadLine());

        Post post = new Post
        {
            Title = title,
            Body = body,
            UserId = userId
        };

        await postRepository.AddAsync(post);

        Console.WriteLine("Post created.");
    }
}