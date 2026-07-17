namespace FantasyFootball.Application.UseCases.News.Commands.CreateNewsArticle;


public class CreateNewsArticleCommandHandler : IRequestHandler<CreateNewsArticleCommand, Result<Guid>>
{
    private readonly INewsArticleRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateNewsArticleCommandHandler(INewsArticleRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateNewsArticleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var article = NewsArticle.Create(request.Title, request.Slug, request.Summary, request.Content, request.Category, request.ImageUrl);
            _repository.Add(article);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<Guid>.Success(article.Id);
        }
        catch (DomainException ex)
        {
            return Result<Guid>.Failure(new Error("NewsArticle.DomainError", ex.Message));
        }
    }
}
