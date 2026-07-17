namespace FantasyFootball.Application.UseCases.Fixtures.Commands.DeleteFixture
{
    public class DeleteFixtureCommandValidator : AbstractValidator<DeleteFixtureCommand>
    {
        public DeleteFixtureCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Fixture Id is required.");
        }
    }
}
