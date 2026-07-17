namespace FantasyFootball.Application.UseCases.Players
{
    public record PlayerDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Position,
    decimal Price,
    int TotalPoints,
    bool IsAvailable,
    string ImageUrl);
}