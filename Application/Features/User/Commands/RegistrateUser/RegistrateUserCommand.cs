using Application.Features.DTOs.User;
using MediatR;

namespace Application.Features.User.Commands.RegistrateUser
{
    public record RegistrateUserCommand(
        UserForRegistrationDto userRegistrationDto)
        : IRequest<CreatedUserDto>;
}
