namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.ToggleAutoSub
{
    public record ToggleAutoSubCommand(Guid FantasyTeamId) : IRequest<Result<Guid>>;
}
