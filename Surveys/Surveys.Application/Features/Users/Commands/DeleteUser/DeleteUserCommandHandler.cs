using MediatR;
using Surveys.Application.Features.Exceptions;
using Surveys.Application.UnitOfWork;

namespace Surveys.Application.Features.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository
            .GetUserById(request.UserId);

        if (user == null)
        {
            throw new UserNotFound(request.UserId);
        }

        unitOfWork.UserRepository.RemoveUser(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}