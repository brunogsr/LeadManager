using LeadManager.Models;
using System;
using System.Text;

namespace LeadManager.Services
{
    public class FakeEmailService : IEmailService
    {
        private readonly string _logFilePath = "email_log.txt";

        public Task SendAcceptanceEmail(Lead acceptedLead)
        {
            string recipient = "vendas@test.com";
            string subject = $"Lead Accepted: Job ID {acceptedLead.JobId} ({acceptedLead.FirstName})";

            var bodyBuilder = new StringBuilder();
            bodyBuilder.AppendLine($"Lead accepted:");
            bodyBuilder.AppendLine($"  ID: {acceptedLead.Id}");
            bodyBuilder.AppendLine($"  Job ID: {acceptedLead.JobId}");
            bodyBuilder.AppendLine($"  Name: {acceptedLead.FirstName} {acceptedLead.LastName ?? ""}".Trim());
            bodyBuilder.AppendLine($"  Category: {acceptedLead.Category}");
            bodyBuilder.AppendLine($"  Suburb: {acceptedLead.Suburb}");
            bodyBuilder.AppendLine($"  Accepted Price: {acceptedLead.Price:C}");
            if (acceptedLead.OriginalPrice.HasValue && acceptedLead.OriginalPrice != acceptedLead.Price)
            {
                bodyBuilder.AppendLine($"  Original Price: {acceptedLead.OriginalPrice.Value:C}");
                bodyBuilder.AppendLine($"  (Discount Applied)");
            }
            bodyBuilder.AppendLine($"  Description: {acceptedLead.Description}");
            bodyBuilder.AppendLine($"---");

            string emailContent = bodyBuilder.ToString();

            Console.WriteLine("--- SIMULATING EMAIL SEND ---");
            Console.WriteLine($"To: {recipient}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine("Body:");
            Console.Write(emailContent);
            Console.WriteLine("--- END SIMULATING EMAIL SEND ---");

            try
            {
                File.AppendAllText(_logFilePath, $"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss UTC}] Email Sent Simulation:\nTo: {recipient}\nSubject: {subject}\nBody:\n{emailContent}\n\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to fake email log file: {ex.Message}");

            }

            return Task.CompletedTask;
        }
    }
}