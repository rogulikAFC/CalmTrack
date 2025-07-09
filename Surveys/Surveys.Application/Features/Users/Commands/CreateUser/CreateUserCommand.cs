using MediatR;
using UserMessages;

namespace Surveys.Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand(CreateUserMessage Message) : IRequest;