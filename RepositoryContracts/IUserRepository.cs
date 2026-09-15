using Entities;

namespace RepositoryContracts;

public interface IUserRepository
{
    Task<User> AddAsync(User user);// Esta operación es una tarea que puede terminar después.
    Task UpdateAsync(User user); // Actualizar un usuario.
    Task DeleteAsync(int id);
    Task<User> GetSingleAsync(int id); // Buscar un solo usuario por su ID y devolver ese usuario.
    IQueryable<User> GetMany(); // IQueryable<User> → devuelve una lista de usuarios.
                                //GetMany → significa obtener varios.
}