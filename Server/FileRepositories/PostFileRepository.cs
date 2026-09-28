using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
    private readonly string filePath = "posts.json";

    public PostFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    private async Task<List<Post>> ReadAsync()
    {
        string json = await File.ReadAllTextAsync(filePath);

        return JsonSerializer.Deserialize<List<Post>>(json)
               ?? new List<Post>();
    }

    private async Task SaveAsync(List<Post> posts)
    {
        string json = JsonSerializer.Serialize(posts);

        await File.WriteAllTextAsync(filePath, json);
    }

    public async Task<Post> AddAsync(Post post)
    {
        List<Post> posts = await ReadAsync();

        post.Id = posts.Any()
            ? posts.Max(p => p.Id) + 1
            : 1;

        posts.Add(post);

        await SaveAsync(posts);

        return post;
    }

    public async Task UpdateAsync(Post post)
    {
        List<Post> posts = await ReadAsync();

        int index = posts.FindIndex(p => p.Id == post.Id);

        if (index == -1)
        {
            throw new InvalidOperationException("Post not found");
        }

        posts[index] = post;

        await SaveAsync(posts);
    }

    public async Task DeleteAsync(int id)
    {
        List<Post> posts = await ReadAsync();

        Post? post = posts.Find(p => p.Id == id);

        if (post is null)
        {
            throw new InvalidOperationException("Post not found");
        }

        posts.Remove(post);

        await SaveAsync(posts);
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        List<Post> posts = await ReadAsync();

        Post? post = posts.Find(p => p.Id == id);

        if (post is null)
        {
            throw new InvalidOperationException("Post not found");
        }

        return post;
    }

    public IQueryable<Post> GetMany()
    {
        string json = File.ReadAllTextAsync(filePath).Result;

        List<Post> posts =
            JsonSerializer.Deserialize<List<Post>>(json)
            ?? new List<Post>();

        return posts.AsQueryable();
    }
}