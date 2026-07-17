namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.PlayChip
{
    public record PlayChipCommand(Guid FantasyTeamId, ChipType ChipType, Guid GameweekId) : IRequest<Result<Guid>>;
}
