namespace FantasyFootball.Application.UseCases.Fixtures.Commands.DeleteFixture
{
    public class DeleteFixtureCommandHandler : IRequestHandler<DeleteFixtureCommand, Result<MediatR.Unit>>
    {
        private readonly IFixtureRepository _fixtureRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFixtureCommandHandler(IFixtureRepository fixtureRepository, IUnitOfWork unitOfWork)
        {
            _fixtureRepository = fixtureRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MediatR.Unit>> Handle(DeleteFixtureCommand request, CancellationToken cancellationToken)
        {
            var fixture = await _fixtureRepository.GetByIdAsync(request.Id);

            if (fixture is null)
                return Result<MediatR.Unit>.Failure(new Error("Not.Found", "Entity not found"));

            _fixtureRepository.Delete(fixture);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
        }
    }
}
