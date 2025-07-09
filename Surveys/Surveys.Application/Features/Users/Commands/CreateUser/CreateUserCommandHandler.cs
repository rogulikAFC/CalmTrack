using MediatR;
using Surveys.Application.UnitOfWork;
using Surveys.Domain.User;

namespace Surveys.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateUserCommand>
{
    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = request.Message.Id
        };
        
        unitOfWork.UserRepository.AddUser(user);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}