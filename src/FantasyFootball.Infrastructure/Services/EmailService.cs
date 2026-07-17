namespace FantasyFootball.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = body };

        using var smtp = new SmtpClient();
        
        // Connect and authenticate
        await smtp.ConnectAsync(_emailSettings.Server, _emailSettings.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.Password);
        
        // Send email
        await smtp.SendAsync(email);
        
        // Disconnect
        await smtp.DisconnectAsync(true);
    }
}
