namespace FantasyFootball.Application.UseCases.Gameweeks.Commands.CreateGameweek
{
    public record CreateGameweekCommand(int Number, DateTime Deadline) : IRequest<Result<Guid>>;
}
