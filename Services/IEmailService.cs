using LeadManager.Models;
using System.Threading.Tasks;

namespace LeadManager.Services
{
    public interface IEmailService
    {
        Task SendAcceptanceEmail(Lead acceptedLead);
    }
}