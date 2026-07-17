namespace FantasyFootball.Application.UseCases.Teams.Commands.DeleteTeam
{
    public record DeleteTeamCommand(Guid Id) : IRequest<Result<MediatR.Unit>>;
}
