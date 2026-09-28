using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    private readonly string filePath = "comments.json";

    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
            File.WriteAllText(filePath, "[]");
    }

    private async Task<List<Comment>> ReadAsync()
    {
        string json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Comment>>(json) ?? new();
    }

    private async Task SaveAsync(List<Comment> comments)
    {
        string json = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, json);
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        List<Comment> comments = await ReadAsync();

        comment.Id = comments.Any()
            ? comments.Max(c => c.Id) + 1
            : 1;

        comments.Add(comment);
        await SaveAsync(comments);

        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
        List<Comment> comments = await ReadAsync();

        int index = comments.FindIndex(c => c.Id == comment.Id);

        if (index == -1)
            throw new Exception("Comment not found");

        comments[index] = comment;
        await SaveAsync(comments);
    }

    public async Task DeleteAsync(int id)
    {
        List<Comment> comments = await ReadAsync();

        Comment? comment = comments.Find(c => c.Id == id);

        if (comment is null)
            throw new Exception("Comment not found");

        comments.Remove(comment);
        await SaveAsync(comments);
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        List<Comment> comments = await ReadAsync();

        return comments.Find(c => c.Id == id)
               ?? throw new Exception("Comment not found");
    }

    public IQueryable<Comment> GetMany()
    {
        string json = File.ReadAllText(filePath);

        List<Comment> comments =
            JsonSerializer.Deserialize<List<Comment>>(json) ?? new();

        return comments.AsQueryable();
    }
}