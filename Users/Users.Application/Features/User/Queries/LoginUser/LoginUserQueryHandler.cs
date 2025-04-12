using Application.Auth;
using Application.Features.Exceptions;
using Application.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Users.Application.Features.User.Queries.LoginUser
{
    public class LoginUserQueryHandler(
        IUnitOfWork unitOfWork, IPasswordHasher<Domain.User.User> passwordHasher,
        IAuthService auth)
        : IRequestHandler<LoginUserQuery, string>
    {
        public async Task<string> Handle(
            LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.UserRepository
                .GetUserByIdAsync(request.UserId);

            if (user == null)
            {
                throw new UserNotFound(request.UserId);
            }

            var verificationResult = passwordHasher.VerifyHashedPassword(
                user, user.Password, request.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                throw new UsersPasswordIsInvalid();
            }

            var token = auth.GenerateToken(user);

            return token;
        }
    }
}
