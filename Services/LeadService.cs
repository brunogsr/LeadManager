using LeadManager.Data;
using LeadManager.Dtos;
using LeadManager.Utils;
using LeadManager.Models;
using Microsoft.EntityFrameworkCore;

namespace LeadManager.Services
{
    public class LeadService : ILeadService
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public LeadService(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<IEnumerable<InvitedLeadDto>> GetInvitedLeads()
        {
            return await _context.Leads
                .Where(l => l.Status == LeadStatus.Invited)
                .OrderByDescending(l => l.DateCreated)
                .Select(lead => new InvitedLeadDto
                {
                    Id = lead.Id,
                    FirstName = lead.FirstName,
                    DateCreated = lead.DateCreated,
                    Suburb = lead.Suburb,
                    Category = lead.Category,
                    JobId = lead.JobId,
                    Description = lead.Description,
                    Price = lead.Price
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<AcceptedLeadDto>> GetAcceptedLeads()
        {
            return await _context.Leads
                .Where(l => l.Status == LeadStatus.Accepted)
                .OrderByDescending(l => l.DateCreated)
                .Select(lead => new AcceptedLeadDto
                {
                    Id = lead.Id,
                    FullName = string.IsNullOrWhiteSpace(lead.LastName) ? lead.FirstName : $"{lead.FirstName} {lead.LastName}",
                    Phone = lead.Phone,
                    Email = lead.Email,
                    Suburb = lead.Suburb,
                    Category = lead.Category,
                    JobId = lead.JobId,
                    Description = lead.Description,
                    Price = lead.Price,
                    DateCreated = lead.DateCreated
                })
                .ToListAsync();
        }

        public async Task<(InvitedLeadDto? CreatedLead, string? ErrorMessage)> CreateLead(CreateLeadDto createDto)
        {
            bool jobExists = await _context.Jobs.AnyAsync(j => j.Id == createDto.JobId);
            if (!jobExists)
            {
                return (null, $"Invalid JobId: Job with ID {createDto.JobId} does not exist.");
            }

            var newLead = new Lead
            {
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                Suburb = createDto.Suburb,
                Category = createDto.Category,
                JobId = createDto.JobId,
                Description = createDto.Description,
                Price = createDto.Price,
                OriginalPrice = createDto.Price,
                Phone = createDto.Phone,
                Email = createDto.Email,
                DateCreated = DateTime.UtcNow,
                Status = LeadStatus.Invited
            };

            try
            {
                _context.Leads.Add(newLead);
                await _context.SaveChangesAsync();

                var createdLeadDto = new InvitedLeadDto
                {
                    Id = newLead.Id,
                    FirstName = newLead.FirstName,
                    DateCreated = newLead.DateCreated,
                    Suburb = newLead.Suburb,
                    Category = newLead.Category,
                    JobId = newLead.JobId,
                    Description = newLead.Description,
                    Price = newLead.Price
                };

                return (createdLeadDto, null);
            }
            catch (DbUpdateException dbEx)
            {

                Console.WriteLine($"Database error creating lead: {dbEx.InnerException?.Message ?? dbEx.Message}");
                return (null, "An error occurred while saving the new lead to the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error creating lead: {ex.Message}");
                return (null, "An unexpected error occurred while creating the lead.");
            }
        }

        public async Task<(bool Success, string ErrorMessage)> AcceptLead(int id)
        {
            var lead = await _context.Leads.FindAsync(id);
            if (lead == null)
            {
                return (false, $"Lead with ID {id} not found.");
            }

            if (lead.Status != LeadStatus.Invited)
            {
                return (false, $"Lead with ID {id} is not in 'Invited' status.");
            }

            lead.Status = LeadStatus.Accepted;
            LeadUtils.ApplyDiscount(lead, id);

            var saveResult = await LeadUtils.SaveLeadChanges(_context, id);
            if (!saveResult.Success)
            {
                return saveResult;
            }

            await LeadUtils.SendEmail(_emailService, lead, id);

            return (true, string.Empty);
        }

        public async Task<(bool Success, string ErrorMessage)> DeclineLead(int id)
        {
            var lead = await _context.Leads.FindAsync(id);

            if (lead == null)
            {
                return (false, $"Lead with ID {id} not found.");
            }

            if (lead.Status != LeadStatus.Invited)
            {
                return (false, $"Lead with ID {id} is not in 'Invited' status (current: {lead.Status}). Cannot decline.");
            }

            lead.Status = LeadStatus.Declined;
            Console.WriteLine($"Lead ID {id} declined. Status changed to {lead.Status}.");

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return (false, "The lead was modified by another process. Please reload and try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving changes for Lead ID {id} decline: {ex.Message}");
                return (false, "An unexpected error occurred while declining the lead.");
            }

            return (true, string.Empty);
        }
    }
}
