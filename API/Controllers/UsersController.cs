using Application.Auth;
using Application.Features.DTOs.User;
using Application.Features.Exceptions;
using Application.Features.User.Commands.LoginUser;
using Application.Features.User.Commands.RegistrateUser;
using Application.Features.User.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            UserForRegistrationDto userForRegistrationDto, CancellationToken cancellationToken)
        {
            var command = new RegistrateUserCommand(userForRegistrationDto);

            try
            {
                var createdUser = await _sender.Send(
                    command, cancellationToken);

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

        [HttpPost("login/{userId}")]
        public async Task<ActionResult<string>> LoginUser(
            Guid userId, [FromBody] string password, CancellationToken cancellationToken)
        {
            var command = new LoginUserCommand(userId, password);

            try
            {
                var token = await _sender.Send(
                    command, cancellationToken);

                return Ok(token);
            }
            catch (UserNotFound exception)
            {
                return NotFound(exception.Message);
            }
            catch(UsersPasswordIsInvalid exception)
            {
                return Unauthorized(exception.Message);
            }
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDto>> GetUserById(
            Guid userId, CancellationToken cancellationToken)
        {
            var currentUserId = User.GetUserId();

            var query = new GetUserByIdQuery(userId, currentUserId);

            try
            {
                var userDto = await _sender.Send(
                    query, cancellationToken);

                return Ok(userDto);
            }
            catch (UserNotFound exception)
            {
                return NotFound(exception.Message);
            }
            catch (UserHasNotPermission exception)
            {
                return Unauthorized(exception.Message);
            }
        }
    }
}
