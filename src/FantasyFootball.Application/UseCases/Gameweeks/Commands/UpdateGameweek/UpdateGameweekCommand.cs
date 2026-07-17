namespace FantasyFootball.Application.UseCases.Gameweeks.Commands.UpdateGameweek
{
    public record UpdateGameweekCommand(
        Guid Id,
        int Number,
        DateTime Deadline,
        bool IsActive,
        bool IsFinished) : IRequest<Result<MediatR.Unit>>;
}
