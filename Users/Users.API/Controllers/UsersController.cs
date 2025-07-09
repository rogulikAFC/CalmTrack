using Application.Features.DTOs.User;
using Application.Features.Exceptions;
using Application.Features.User.Commands.DeleteUser;
using Application.Features.User.Commands.RegistrateUser;
using Application.Features.User.Commands.UpdateUser;
using Application.Features.User.Queries.GetUserById;
using Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Features.User.Queries.LoginUser;
using Users.Application.Kafka;
using Users.Application.Kafka.Exceptions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;

        public UsersController(ISender sender, IUsersProducer usersProducer)
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
            var command = new LoginUserQuery(userId, password);

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
            catch (UsersPasswordIsInvalid exception)
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

        [Authorize]
        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteUser(
            Guid userId, CancellationToken cancellationToken)
        {
            var currentUserId = User.GetUserId();

            if (currentUserId != userId)
            {
                return Unauthorized();
            }

            var command = new DeleteUserCommand(userId);

            try
            {
                await _sender.Send(command, cancellationToken);

                return NoContent();
            }
            catch (UserNotFound exception)
            {
                return NotFound(exception.Message);
            }
        }

        [Authorize]
        [HttpPut("{userId}")]
        public async Task<ActionResult> UpdateUser(
            Guid userId, UserForUpdateDto userForUpdateDto, CancellationToken cancellationToken)
        {
            var currentUserId = User.GetUserId();

            if (currentUserId != userId)
            {
                return Unauthorized();
            }

            var command = new UpdateUserCommand(userId, userForUpdateDto);

            try
            {
                await _sender.Send(command, cancellationToken);

                return NoContent();
            }
            catch (UserNotFound exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "GETTED";
        }
    }
}
