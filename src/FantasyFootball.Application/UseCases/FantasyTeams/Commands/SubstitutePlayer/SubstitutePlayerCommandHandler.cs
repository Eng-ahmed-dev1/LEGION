namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.SubstitutePlayer;

public class SubstitutePlayerCommandHandler : IRequestHandler<SubstitutePlayerCommand, Result<Unit>>
{
    private readonly IFantasyTeamRepository _fantasyTeamRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SubstitutePlayerCommandHandler(
        IFantasyTeamRepository fantasyTeamRepository,
        IUnitOfWork unitOfWork)
    {
        _fantasyTeamRepository = fantasyTeamRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> Handle(SubstitutePlayerCommand request, CancellationToken cancellationToken)
    {
        // Must include players and their real Player entities to validate positions
        var team = await _fantasyTeamRepository.GetByIdWithPlayersAsync(request.FantasyTeamId);
        if (team is null)
            return Result<Unit>.Failure(new Error("FantasyTeam.NotFound", "Fantasy team not found."));

        try
        {
            team.SubstitutePlayer(request.PlayerInId, request.PlayerOutId);

            _fantasyTeamRepository.Update(team);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
        catch (DomainException ex)
        {
            return Result<Unit>.Failure(new Error("FantasyTeam.SubstitutionError", ex.Message));
        }
    }
}
