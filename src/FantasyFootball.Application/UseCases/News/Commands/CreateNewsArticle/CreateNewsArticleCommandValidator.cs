namespace FantasyFootball.Application.UseCases.News.Commands.CreateNewsArticle;

public class CreateNewsArticleCommandValidator : AbstractValidator<CreateNewsArticleCommand>
{
    public CreateNewsArticleCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Content).NotEmpty();
        RuleFor(x => x.Category).NotEmpty().MaximumLength(50);
    }
}
