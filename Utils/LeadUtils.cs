using LeadManager.Models;
using LeadManager.Data;
using LeadManager.Services;
using Microsoft.EntityFrameworkCore;

namespace LeadManager.Utils
{
    public static class LeadUtils
    {
        public static void ApplyDiscount(Lead lead, int leadId)
        {
            const decimal discountThreshold = 500m;
            const decimal discountRate = 0.10m;

            lead.OriginalPrice ??= lead.Price;

            if (lead.Price > discountThreshold)
            {
                lead.OriginalPrice ??= lead.Price;
                lead.Price = Math.Round(lead.Price * (1 - discountRate), 2);
                Console.WriteLine($"Applied 10% discount to Lead ID {leadId}. Original: {lead.OriginalPrice}, New: {lead.Price}");
            }
            else
            {
                lead.OriginalPrice ??= lead.Price;
            }
        }

        public static async Task<(bool Success, string ErrorMessage)> SaveLeadChanges(
            AppDbContext context, int leadId)
        {
            try
            {
                await context.SaveChangesAsync();
                return (true, string.Empty);
            }
            catch (DbUpdateConcurrencyException)
            {
                return (false, "The lead was modified by another process. Please reload and try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving changes for Lead ID {leadId}: {ex.Message}");
                return (false, "An unexpected error occurred while saving the lead.");
            }
        }

        public static async Task SendEmail(
            IEmailService emailService, Lead lead, int leadId)
        {
            try
            {
                await emailService.SendAcceptanceEmail(lead);
                Console.WriteLine($"Sent acceptance email for Lead ID {leadId}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WARNING: Failed to send acceptance email for Lead ID {leadId}: {ex.Message}");
            }
        }
    }
}