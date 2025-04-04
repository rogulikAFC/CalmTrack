using Application.Auth;
using Application.Features.DTOs.User;
using Application.Features.Exceptions;
using Application.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.User.Commands.RegistrateUser
{
    public class RegistrateUserCommandHandler(
        IUnitOfWork unitOfWork, IPasswordHasher<Domain.User.User> passwordHasher,
        IAuthService auth)
        : IRequestHandler<RegistrateUserCommand, CreatedUserDto>
    {
        public async Task<CreatedUserDto> Handle(
            RegistrateUserCommand request, CancellationToken cancellationToken)
        {
            var role = await unitOfWork.RoleRepository
                .GetRoleByNameAsync("user");

            if (role == null)
            {
                throw new RoleNotFoundByName("user");
            }

            var user = new Domain.User.User(
                request.userRegistrationDto.FirstName,
                request.userRegistrationDto.LastName,
                request.userRegistrationDto.Email,
                role.Id);

            var hashedPassword = passwordHasher
                .HashPassword(user, request.userRegistrationDto.Password);

            user.Password = hashedPassword;

            unitOfWork.UserRepository.AddUser(user);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            var createdUser = await unitOfWork.UserRepository
                .GetUserByIdAsync(user.Id);

            if (createdUser == null)
            {
                throw new Exception("Created user was not found.");
            }

            var token = auth.GenerateToken(createdUser);

            var createdUserDto = CreatedUserDto
                .MapFromUser(user, token);

            return createdUserDto;
        }
    }
}
