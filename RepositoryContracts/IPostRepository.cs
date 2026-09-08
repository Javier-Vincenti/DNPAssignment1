using Entities;

namespace RepositoryContracts;

public interface IPostRepository
{
    Task<Post> AddAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(int id); // delete post 
    Task<Post> GetSingleAsync(int id); // Finds a Post by its ID.
    IQueryable<Post> GetMany(); // 
     
    
}