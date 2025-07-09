using Application.Features.Exceptions;
using Application.UnitOfWork;
using MediatR;
using Users.Application.Kafka;

namespace Application.Features.User.Commands.DeleteUser
{
    public class DeleteUserCommandHandler(
        IUnitOfWork unitOfWork, IUsersProducer usersProducer)
        : IRequestHandler<DeleteUserCommand>
    {
        public async Task Handle(
            DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.UserRepository
                .GetUserByIdAsync(request.UserId);

            if (user == null)
            {
                throw new UserNotFound(request.UserId);
            }

            unitOfWork.UserRepository
                .DeleteUser(user);

            await usersProducer.ProduceDeleteUserMessage(user.Id);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
