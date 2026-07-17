namespace FantasyFootball.Application.UseCases.Managers.Commands.UpdateManager
{
    public record UpdateManagerCommand(
        Guid Id,
        string TeamName,
        string UserName) : IRequest<Result<MediatR.Unit>>;
}
