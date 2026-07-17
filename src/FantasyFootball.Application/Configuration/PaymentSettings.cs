namespace FantasyFootball.Application.Configuration;

public class PaymentSettings
{
    public const string SectionName = "PaymentSettings";
    public decimal PremiumPrice { get; set; } = 99m;
    public string Currency { get; set; } = "EGP";
}
