using ErrorOr;
using MediatR;

namespace Application.Users.Commands.MeCommand;

public sealed record MeCommand(
    string Login) : IRequest<ErrorOr<MeDTO>>;