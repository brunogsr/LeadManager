using LeadManager.Dtos;

namespace LeadManager.Services
{
    public interface ILeadService
    {
        Task<IEnumerable<InvitedLeadDto>> GetInvitedLeads();
        Task<IEnumerable<AcceptedLeadDto>> GetAcceptedLeads();
        Task<(bool Success, string ErrorMessage)> AcceptLead(int id);
        Task<(bool Success, string ErrorMessage)> DeclineLead(int id);
        Task<(InvitedLeadDto? CreatedLead, string? ErrorMessage)> CreateLead(CreateLeadDto createDto);
    }
}
