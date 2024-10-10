using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // Implémentez la logique d'envoi d'e-mail ici
        return Task.CompletedTask;
    }
}