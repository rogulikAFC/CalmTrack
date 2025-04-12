using Application.Features.DTOs.User;
using MediatR;

namespace Application.Features.User.Commands.UpdateUser
{
    public record UpdateUserCommand(
        Guid UserId, UserForUpdateDto UserForUpdateDto)
        :IRequest;
}
