namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.SubstitutePlayer;

public record SubstitutePlayerCommand(
    Guid FantasyTeamId,
    Guid PlayerInId,
    Guid PlayerOutId
) : IRequest<Result<Unit>>;
