using RepositoryContracts;

namespace CLI;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    public SinglePostView(
        IPostRepository postRepository,
        ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    public async Task ShowAsync()
    {
        Console.Write("Post ID: ");
        int postId = int.Parse(Console.ReadLine());

        var post = await postRepository.GetSingleAsync(postId);

        Console.WriteLine($"Title: {post.Title}");
        Console.WriteLine($"Body: {post.Body}");
        Console.WriteLine("Comments:");

        var comments = commentRepository
            .GetMany()
            .Where(comment => comment.PostId == postId);

        foreach (var comment in comments)
        {
            Console.WriteLine($"- {comment.Body}");
        }
    }
}