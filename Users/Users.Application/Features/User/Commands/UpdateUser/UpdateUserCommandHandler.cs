using Application.Features.Exceptions;
using Application.UnitOfWork;
using MediatR;

namespace Application.Features.User.Commands.UpdateUser
{
    public class UpdateUserCommandHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateUserCommand>
    {
        public async Task Handle(
            UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.UserRepository
                .GetUserByIdAsync(request.UserId);

            if (user == null)
            {
                throw new UserNotFound(request.UserId);
            }

            var userForUpdate = request.UserForUpdateDto;

            user.FirstName = userForUpdate.FirstName;
            user.LastName = userForUpdate.LastName;
            user.Email = userForUpdate.Email;

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
