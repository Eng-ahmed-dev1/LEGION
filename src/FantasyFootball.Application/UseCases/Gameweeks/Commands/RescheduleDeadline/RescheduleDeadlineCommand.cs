namespace FantasyFootball.Application.UseCases.Gameweeks.Commands.RescheduleDeadline
{
    public record RescheduleDeadlineCommand(Guid id, DateTime newDeadline) : IRequest<Result<Unit>>;
}