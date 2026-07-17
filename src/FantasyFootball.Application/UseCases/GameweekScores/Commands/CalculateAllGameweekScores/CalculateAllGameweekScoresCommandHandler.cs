namespace FantasyFootball.Application.UseCases.GameweekScores.Commands.CalculateAllGameweekScores
{
    public class CalculateAllGameweekScoresCommandHandler : IRequestHandler<CalculateAllGameweekScoresCommand, Result<Unit>>
    {
        private readonly IFantasyTeamRepository _fantasyTeamRepository;
        private readonly IMediator _mediator;

        public CalculateAllGameweekScoresCommandHandler(IFantasyTeamRepository fantasyTeamRepository, IMediator mediator)
        {
            _fantasyTeamRepository = fantasyTeamRepository;
            _mediator = mediator;
        }

        public async Task<Result<Unit>> Handle(CalculateAllGameweekScoresCommand request, CancellationToken cancellationToken)
        {
            var allTeams = await _fantasyTeamRepository.GetAllAsync();
            foreach (var team in allTeams)
            {
                var command = new CalculateGameweekScoreCommand(team.ManagerId, request.GameweekId);
                await _mediator.Send(command, cancellationToken);
            }

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
