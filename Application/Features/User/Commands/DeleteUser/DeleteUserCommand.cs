using MediatR;

namespace Application.Features.User.Commands.DeleteUser
{
    public record DeleteUserCommand(Guid UserId)
        : IRequest;
}
