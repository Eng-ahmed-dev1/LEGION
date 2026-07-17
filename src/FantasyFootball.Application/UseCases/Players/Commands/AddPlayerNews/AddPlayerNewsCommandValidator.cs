namespace FantasyFootball.Application.UseCases.Players.Commands.AddPlayerNews;

public class AddPlayerNewsCommandValidator : AbstractValidator<AddPlayerNewsCommand>
{
    public AddPlayerNewsCommandValidator()
    {
        RuleFor(x => x.PlayerId).NotEmpty();
        RuleFor(x => x.NewsText).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Type).IsInEnum();
    }
}
