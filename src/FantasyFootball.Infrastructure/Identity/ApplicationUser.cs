namespace FantasyFootball.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Manager Manager { get; set; } = default!;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}