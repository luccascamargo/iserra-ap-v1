using iserra_api.Dto;
using iserra_api.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace iserra_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("/users")]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {

            var users = await _service.GetAllUsers();

            return Ok(users);
        }

        [HttpPost("/users")]
        public async Task<ActionResult<UserDto>> SignUp([FromBody] UserCreateDto userCreateDto)
        {

            var user = await _service.CreateUser(userCreateDto);

            return Created("Usuario criado com sucesso", user);
        }

        [HttpPut("/users/{email}")]
        public async Task<ActionResult<UserDto>> Update(string email, [FromBody] UserUpdateDto user)
        {

            var users = await _service.UpdateUser(email, user);

            return Ok(users);
        }
    }
}
