using RepositoryContracts;
using Microsoft.AspNetCore.Mvc;
using ApiContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository userRepository;

    public UsersController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDto>> GetSingleAsync(int id)
    {
        try
        {
            var user = await userRepository.GetSingleAsync(id);

            var dto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName
            };

            return Ok(dto);
        }
        catch (InvalidOperationException)
        {
            return NotFound("User not found");
        }
    }

    [HttpGet]
    public ActionResult<List<UserDto>> GetMany()
    {
        var users = userRepository.GetMany();

        var dtos = users.Select(user => new UserDto
        {
            Id = user.Id,
            UserName = user.UserName
        }).ToList();

        return Ok(dtos);
    }
}