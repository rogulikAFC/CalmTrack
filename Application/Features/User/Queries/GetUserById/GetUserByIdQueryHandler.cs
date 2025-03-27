using Application.Features.DTOs.User;
using Application.Features.Exceptions;
using Application.UnitOfWork;
using MediatR;

namespace Application.Features.User.Queries.GetUserById
{
    public class GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        public async Task<UserDto> Handle(
            GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await unitOfWork.UserRepository
                .GetUserByIdAsync(request.CurrentUserId);

            if (currentUser == null)
            {
                throw new UserNotFound(request.CurrentUserId);
            }

            // Verify that user has permission to access endpoint
            if (!(
                currentUser.Role.ToString() == "admin" 
                || currentUser.Id == request.UserId))
            {
                throw new UserHasNotPermission(currentUser.Id);
            }

            return UserDto.MapFromUser(currentUser);
        }
    }
}
