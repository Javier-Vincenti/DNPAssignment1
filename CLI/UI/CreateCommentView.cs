using Entities;
using RepositoryContracts;

namespace CLI;

public class CreateCommentView
{
    private readonly ICommentRepository commentRepository;

    public CreateCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task ShowAsync()
    {
        Console.Write("Comment: ");
        string body = Console.ReadLine();

        Console.Write("User ID: ");
        int userId = int.Parse(Console.ReadLine());

        Console.Write("Post ID: ");
        int postId = int.Parse(Console.ReadLine());

        Comment comment = new Comment
        {
            Body = body,
            UserId = userId,
            PostId = postId
        };

        await commentRepository.AddAsync(comment);

        Console.WriteLine("Comment created.");
    }
}