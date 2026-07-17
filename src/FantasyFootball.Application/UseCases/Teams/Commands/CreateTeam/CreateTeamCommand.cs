namespace FantasyFootball.Application.UseCases.Teams.Commands.CreateTeam
{
    public record CreateTeamCommand(string Name, string ShortName) : IRequest<Result<Guid>>;
}
