using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string filePath = "users.json";

    public UserFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    private async Task<List<User>> ReadAsync()
    {
        string json = await File.ReadAllTextAsync(filePath);

        return JsonSerializer.Deserialize<List<User>>(json)
               ?? new List<User>();
    }

    private async Task SaveAsync(List<User> users)
    {
        string json = JsonSerializer.Serialize(users);

        await File.WriteAllTextAsync(filePath, json);
    }

    public async Task<User> AddAsync(User user)
    {
        List<User> users = await ReadAsync();

        user.Id = users.Any()
            ? users.Max(u => u.Id) + 1
            : 1;

        users.Add(user);

        await SaveAsync(users);

        return user;
    }

    public async Task UpdateAsync(User user)
    {
        List<User> users = await ReadAsync();

        int index = users.FindIndex(u => u.Id == user.Id);

        if (index == -1)
        {
            throw new InvalidOperationException("User not found");
        }

        users[index] = user;

        await SaveAsync(users);
    }

    public async Task DeleteAsync(int id)
    {
        List<User> users = await ReadAsync();

        User? user = users.Find(u => u.Id == id);

        if (user is null)
        {
            throw new InvalidOperationException("User not found");
        }

        users.Remove(user);

        await SaveAsync(users);
    }

    
    public async Task<User> GetSingleAsync(int id)
    {
        List<User> users = await ReadAsync();

        User? user = users.Find(u => u.Id == id);

        if (user is null)
        {
            throw new InvalidOperationException("User not found");
        }

        return user;
    }

    public IQueryable<User> GetMany()
    {
        string json = File.ReadAllTextAsync(filePath).Result;

        List<User> users =
            JsonSerializer.Deserialize<List<User>>(json)
            ?? new List<User>();

        return users.AsQueryable();
    }
}