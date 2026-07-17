namespace FantasyFootball.Application.UseCases.Gameweeks.Commands.DeleteGameweek
{
    public record DeleteGameweekCommand(Guid Id) : IRequest<Result<MediatR.Unit>>;
}
