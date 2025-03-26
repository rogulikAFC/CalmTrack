using Application.Features.DTOs.User;
using Application.Features.Exceptions;
using Application.Features.User.Commands.RegistrateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult<CreatedUserDto>> RegistrateUser(
            UserForRegistrationDto userForRegistrationDto)
        {
            var command = new RegistrateUserCommand(userForRegistrationDto);

            try
            {
                var createdUser = await _sender.Send(command);

                return Ok(createdUser);
            }
            catch (RoleNotFoundByName exception)
            {
                return NotFound(exception.Message);
            } 
            catch (UserNotFound exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
