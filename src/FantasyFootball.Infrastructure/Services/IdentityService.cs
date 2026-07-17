namespace FantasyFootball.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IManagerRepository _managerRepository;
        private readonly IFantasyTeamRepository _fantasyTeamRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IManagerRepository managerRepository,
            IFantasyTeamRepository fantasyTeamRepository,
            IUnitOfWork unitOfWork,
            IJwtService jwtService,
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _managerRepository = managerRepository;
            _fantasyTeamRepository = fantasyTeamRepository;
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _emailService = emailService;
        }
        public async Task<(bool Success, AuthResponseDto? AuthResponse)> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return (false, null);

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded)
                return (false, null);

            var token = _jwtService.GenerateToken(user.Id.ToString(), user.Email!);
            var refreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); // 7 days valid
            await _userManager.UpdateAsync(user);

            return (true, new AuthResponseDto(token, refreshToken, user.RefreshTokenExpiryTime.Value));
        }

        public async Task<(bool Success, string[] Errors)> RegisterAsync(string email, string password, string teamName, string UserName)
        {
            var user = new ApplicationUser
            {
                Email = email,
                UserName = UserName,
            };
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return (false, result.Errors.Select(x => x.Description).ToArray());

            var manager = Manager.Create(teamName, user.Id, UserName);
            await _managerRepository.AddAsync(manager);

            var fantasyTeam = FantasyTeam.Create($"{teamName} FC", manager.Id);
            await _fantasyTeamRepository.AddAsync(fantasyTeam);

            await _unitOfWork.SaveChangesAsync();
            return (true, []);
        }

        public async Task<(bool Success, AuthResponseDto? AuthResponse)> RefreshTokenAsync(string token, string refreshToken)
        {


            var user = _userManager.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return (false, null); // Invalid or expired refresh token
            }

            var newAccessToken = _jwtService.GenerateToken(user.Id.ToString(), user.Email!);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return (true, new AuthResponseDto(newAccessToken, newRefreshToken, user.RefreshTokenExpiryTime.Value));
        }

        public async Task<bool> RevokeTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            await _userManager.UpdateAsync(user);

            return true;
        }

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetLink = $"https://your-frontend.com/reset-password?email={email}&token={Uri.EscapeDataString(token)}";
            await _emailService.SendEmailAsync(
                email,
                "Reset your Password - Fantasy Football",
                $"<p>Click <a href='{resetLink}'>here</a> to reset your password.</p>");

            return true;
        }

        public async Task<(bool Success, string[] Errors)> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return (false, new[] { "User not found." });

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description).ToArray());

            return (true, []);
        }

        public async Task<(bool Success, string[] Errors)> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return (false, new[] { "User not found." });

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description).ToArray());

            return (true, []);
        }

        public async Task<UserProfileDto?> GetProfileAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return null;

            var manager = await _managerRepository.GetByIdAsync(userId);
            return new UserProfileDto(user.Id, user.Email!, user.UserName!, manager?.TeamName ?? "");
        }

        public async Task<(bool Success, string[] Errors)> UpdateProfileAsync(Guid userId, string newEmail, string newUserName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return (false, new[] { "User not found." });

            user.Email = newEmail;
            user.UserName = newUserName;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description).ToArray());

            return (true, []);
        }

        public async Task<(bool Success, string[] Errors)> DeleteAccountAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return (false, new[] { "User not found." });

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description).ToArray());

            return (true, []);
        }

        public async Task<(bool Success, string[] Errors)> ConfirmEmailAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return (false, new[] { "User not found." });

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description).ToArray());

            return (true, []);
        }

        public async Task<bool> ResendConfirmationEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || user.EmailConfirmed) return false;

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var confirmLink = $"https://your-frontend.com/confirm-email?email={email}&token={Uri.EscapeDataString(token)}";
            await _emailService.SendEmailAsync(
                email,
                "Confirm your Email - Fantasy Football",
                $"<p>Welcome to Fantasy Football! Click <a href='{confirmLink}'>here</a> to confirm your email.</p>");

            return true;
        }

        public async Task<(bool Success, string[] Errors)> Enable2FAAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return (false, new[] { "User not found." });

            var result = await _userManager.SetTwoFactorEnabledAsync(user, true);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description).ToArray());

            return (true, []);
        }

        public async Task<(bool Success, string[] Errors)> Disable2FAAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return (false, new[] { "User not found." });

            var result = await _userManager.SetTwoFactorEnabledAsync(user, false);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description).ToArray());

            return (true, []);
        }

        public async Task<(bool Success, AuthResponseDto? AuthResponse)> Verify2FAAsync(string email, string code)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return (false, null);

            var result = await _signInManager.TwoFactorSignInAsync("Email", code, false, false);
            if (!result.Succeeded) return (false, null);

            var token = _jwtService.GenerateToken(user.Id.ToString(), user.Email!);
            var refreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return (true, new AuthResponseDto(token, refreshToken, user.RefreshTokenExpiryTime.Value));
        }

        public async Task<bool> SendVerificationCodeAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            var code = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            await _emailService.SendEmailAsync(
                email,
                "Your 2FA Verification Code",
                $"<p>Your verification code is: <strong>{code}</strong></p><p>This code will expire shortly.</p>");

            return true;
        }
    }
}