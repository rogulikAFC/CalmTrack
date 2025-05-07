using Application.Auth;
using Application.Features.Exceptions;
using Application.Features.User.Queries.GetUserById;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Users.Application.Features.User.Queries.LoginUser;

namespace Users.gRPC.Services
{
    public class UsersService : Users.UsersBase
    {
        private readonly ISender _sender;

        public UsersService(ISender sender)
        {
            _sender = sender;
        }

        public override async Task<LoginResponse> LoginUser(
            LoginRequest request, ServerCallContext context)
        {
            var query = new LoginUserQuery(
                Guid.Parse(request.UserId), request.Password);

            try
            {
                var token = await _sender.Send(query);

                return await Task.FromResult(new LoginResponse
                {
                    Token = token
                });
            }
            catch (UserNotFound exception)
            {
                throw new RpcException(
                    new Status(StatusCode.NotFound, exception.Message));
            } 
            catch (UsersPasswordIsInvalid exception)
            {
                throw new RpcException(
                    new Status(StatusCode.Unauthenticated, exception.Message));
            }

        }

        [Authorize]
        public override async Task<GetUserByIdResponse> GetUserById(
            GetUserByIdRequest request, ServerCallContext context)
        {
            var currentUserId = context.GetHttpContext().User.GetUserId();

            var query = new GetUserByIdQuery(
                Guid.Parse(request.UserId), currentUserId);

            try
            {
                var user = await _sender.Send(query);

                return await Task.FromResult(new GetUserByIdResponse
                {
                    Id = user.Id.ToString(),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = user.Role,
                });
            }
            catch (UserHasNotPermission exception)
            {
                throw new RpcException(
                    new Status(StatusCode.Unauthenticated, exception.Message)); 
            }
            catch (UserNotFound exception)
            {
                throw new RpcException(
                    new Status(StatusCode.NotFound, exception.Message));
            }
        }

        public override async Task<TestGrpcResponse> TestGrpc(TestGrpcRequest request, ServerCallContext context)
        {
            return await Task.FromResult(new TestGrpcResponse
            {
                Id = request.Id,
            });
        }
    }
}
