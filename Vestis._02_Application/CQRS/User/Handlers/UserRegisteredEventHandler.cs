using MediatR;
using Vestis._02_Application.CQRS.User.Events;
using Vestis._04_Infrastructure.Email;
using Vestis._04_Infrastructure.Email.Interfaces;

namespace Vestis._02_Application.CQRS.User.Handlers;

public class UserRegisteredEventHandler : INotificationHandler<UserRegisteredEvent>
{
    private readonly IEmailSender _emailSender;

    public UserRegisteredEventHandler(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
    {
        var htmlBody = GenerateWelcomeEmailHtml(notification.VerificationCode);
        
        var email = new EmailMessage(
            "Welcome to Vestis!",
            htmlBody,
            true,
            new[] { notification.Email }
            );

        await _emailSender.SendEmailAsync(email, cancellationToken);
    }

    private static string GenerateWelcomeEmailHtml(string verificationCode)
    {
        return $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Welcome to Vestis</title>
    <style>
        body {{
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif;
            line-height: 1.6;
            color: #333;
            background-color: #f5f5f5;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 20px auto;
            background-color: #ffffff;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            overflow: hidden;
        }}
        .header {{
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: #ffffff;
            padding: 40px 20px;
            text-align: center;
        }}
        .header h1 {{
            margin: 0;
            font-size: 28px;
            font-weight: 600;
        }}
        .content {{
            padding: 40px;
        }}
        .greeting {{
            font-size: 16px;
            margin-bottom: 20px;
        }}
        .verification-section {{
            background-color: #f9f9f9;
            border-left: 4px solid #667eea;
            padding: 20px;
            margin: 30px 0;
            border-radius: 4px;
        }}
        .verification-label {{
            font-size: 12px;
            color: #666;
            text-transform: uppercase;
            letter-spacing: 1px;
            margin-bottom: 10px;
            display: block;
        }}
        .verification-code {{
            font-size: 32px;
            font-weight: 700;
            color: #667eea;
            text-align: center;
            letter-spacing: 4px;
            font-family: 'Courier New', monospace;
            margin: 15px 0;
        }}
        .verification-hint {{
            font-size: 13px;
            color: #666;
            text-align: center;
            margin-top: 15px;
        }}
        .message {{
            font-size: 14px;
            color: #555;
            margin-bottom: 20px;
            line-height: 1.8;
        }}
        .footer {{
            background-color: #f5f5f5;
            padding: 20px;
            text-align: center;
            font-size: 12px;
            color: #888;
            border-top: 1px solid #e0e0e0;
        }}
        .divider {{
            height: 1px;
            background-color: #e0e0e0;
            margin: 30px 0;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <h1>Welcome to Vestis!</h1>
        </div>
        
        <div class=""content"">
            <div class=""greeting"">
                <p>Hello,</p>
            </div>
            
            <div class=""message"">
                <p>Thank you for registering with Vestis. We're excited to have you on board!</p>
                <p>To complete your registration and verify your email address, please use the code below:</p>
            </div>
            
            <div class=""verification-section"">
                <span class=""verification-label"">Verification Code</span>
                <div class=""verification-code"">{verificationCode}</div>
                <p class=""verification-hint"">This code will expire in 24 hours</p>
            </div>
            
            <div class=""message"">
                <p>If you have any questions or need assistance, feel free to reach out to our support team.</p>
                <p>Best regards,<br>The Vestis Team</p>
            </div>
        </div>
        
        <div class=""footer"">
            <p>&copy; 2024 Vestis. All rights reserved.</p>
            <p>If you didn't create this account, please contact our support team immediately.</p>
        </div>
    </div>
</body>
</html>";
    }
}
