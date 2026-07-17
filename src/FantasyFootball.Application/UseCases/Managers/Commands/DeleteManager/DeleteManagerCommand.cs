namespace FantasyFootball.Application.UseCases.Managers.Commands.DeleteManager
{
    public record DeleteManagerCommand(Guid Id) : IRequest<Result<MediatR.Unit>>;
}
