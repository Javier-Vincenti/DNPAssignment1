using RepositoryContracts;

namespace CLI;

public class ListPostsView
{
    private readonly IPostRepository postRepository;

    public ListPostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public void Show()
    {
        Console.WriteLine("\nPosts:");

        foreach (var post in postRepository.GetMany())
        {
            Console.WriteLine($"{post.Id} - {post.Title}");
        }
    }
}