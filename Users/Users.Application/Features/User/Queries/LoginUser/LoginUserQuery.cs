using MediatR;

namespace Users.Application.Features.User.Queries.LoginUser
{
    public record LoginUserQuery(Guid UserId, string Password)
        : IRequest<string>;
}
