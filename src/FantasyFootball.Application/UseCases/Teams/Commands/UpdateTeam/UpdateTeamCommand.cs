namespace FantasyFootball.Application.UseCases.Teams.Commands.UpdateTeam
{
    public record UpdateTeamCommand(
        Guid Id,
        string Name,
        string ShortName) : IRequest<Result<MediatR.Unit>>;
}
