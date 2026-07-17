namespace FantasyFootball.Application.UseCases.Fixtures.Commands.CreateFixture
{
    public class CreateFixtureCommandHandler : IRequestHandler<CreateFixtureCommand, Result<Guid>>
    {
        private readonly IFixtureRepository _fixtureRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateFixtureCommandHandler(IFixtureRepository fixtureRepository, IUnitOfWork unitOfWork)
        {
            _fixtureRepository = fixtureRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateFixtureCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var fixture = Fixture.Create(request.HomeTeamId, request.AwayTeamId, request.GameweekId, request.KickOff);

                await _fixtureRepository.AddAsync(fixture);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Success(fixture.Id);
            }
            catch (DomainException ex)
            {
                return Result<Guid>.Failure(new Error("Domain.Error", ex.Message));
            }
        }
    }
}
