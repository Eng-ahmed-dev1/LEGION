namespace FantasyFootball.Application.Common.Interfaces.Services
{
    public interface IIdentityService
    {
        Task<(bool Success, string[] Errors)> RegisterAsync(string email, string password, string teamName, string username);
        Task<(bool Success, AuthResponseDto? AuthResponse)> LoginAsync(string email, string password);
        Task<(bool Success, AuthResponseDto? AuthResponse)> RefreshTokenAsync(string token, string refreshToken);
        Task<bool> RevokeTokenAsync(string email);
        Task<bool> ForgotPasswordAsync(string email);
        Task<(bool Success, string[] Errors)> ResetPasswordAsync(string email, string token, string newPassword);
        Task<(bool Success, string[] Errors)> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
        
        Task<UserProfileDto?> GetProfileAsync(Guid userId);
        Task<(bool Success, string[] Errors)> UpdateProfileAsync(Guid userId, string newEmail, string newUserName);
        Task<(bool Success, string[] Errors)> DeleteAccountAsync(Guid userId);

        Task<(bool Success, string[] Errors)> ConfirmEmailAsync(string email, string token);
        Task<bool> ResendConfirmationEmailAsync(string email);
        Task<(bool Success, string[] Errors)> Enable2FAAsync(Guid userId);
        Task<(bool Success, string[] Errors)> Disable2FAAsync(Guid userId);
        Task<(bool Success, AuthResponseDto? AuthResponse)> Verify2FAAsync(string email, string code);
        Task<bool> SendVerificationCodeAsync(string email);
    }
}
