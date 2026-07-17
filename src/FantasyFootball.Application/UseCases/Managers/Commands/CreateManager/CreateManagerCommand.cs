namespace FantasyFootball.Application.UseCases.Managers.Commands.CreateManager
{
    public record CreateManagerCommand(string TeamName, Guid ApplicationUserId, string UserName) : IRequest<Result<Guid>>;
}
