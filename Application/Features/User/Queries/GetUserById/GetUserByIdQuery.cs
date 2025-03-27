using Application.Features.DTOs.User;
using MediatR;

namespace Application.Features.User.Queries.GetUserById
{
    public record GetUserByIdQuery(Guid UserId, Guid CurrentUserId) 
        : IRequest<UserDto>;
}
