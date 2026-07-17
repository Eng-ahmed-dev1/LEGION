namespace FantasyFootball.Application.DTOs;

public record PaymentIntentDto(string ClientSecret, string IdempotencyKey);
