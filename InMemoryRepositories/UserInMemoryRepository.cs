using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository // -> : cumple las reglas IUserRepository 
{
    private readonly List<User> users = new(); // Crea una lista vacía llamada users, donde después vamos a guardar usuarios.
    
    public Task<User> AddAsync(User user) // (User user) → recibe el usuario que queremos agregar.
    {
        user.Id = users.Any() // .Any() → pregunta “¿hay algún usuario en la lista?
            ? users.Max(u => u.Id) + 1 // Si ya hay usuarios, buscá el ID más grande y sumale 1
            : 1; // Y el ? significa “si la condición anterior es verdadera, hacé esto”
                // Si NO hay ningún usuario en la lista, poné el ID en 1.
        users.Add(user);

        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        User? existingUser = users.SingleOrDefault(u => u.Id == user.Id); // “Buscá en la lista users un usuario que tenga el mismo ID que el usuario que quiero actualizar.”

        if (existingUser is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{user.Id}' not found");
        }

        users.Remove(existingUser); // Eliminá de la lista users el usuario que encontramos.
        users.Add(user);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        User? userToRemove = users.SingleOrDefault(u => u.Id == id);

        if (userToRemove is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{id}' not found");
        }

        users.Remove(userToRemove);

        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        User? user = users.SingleOrDefault(u => u.Id == id);

        if (user is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{id}' not found");
        }

        return Task.FromResult(user);
    }

    public IQueryable<User> GetMany()
    {
        return users.AsQueryable(); // Dame todos los usuarios que están en la lista users.”
    }
}