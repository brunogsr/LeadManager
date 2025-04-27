using LeadManager.Dtos;
using LeadManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeadManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeadController : ControllerBase
    {
        private readonly ILeadService _leadService; 

        public LeadController(ILeadService leadService)
        {
            _leadService = leadService;
        }

        // GET: api/lead/invited (para obter leads convidados)
        [HttpGet("invited")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<InvitedLeadDto>>> GetInvitedLeads()
        {
            var leads = await _leadService.GetInvitedLeads();
            return Ok(leads);
        }

        // GET: api/lead/accepted (mostrar todos os leads aceitos)
        [HttpGet("accepted")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AcceptedLeadDto>>> GetAcceptedLeads()
        {
            var leads = await _leadService.GetAcceptedLeads();
            return Ok(leads);
        }

        // POST: api/lead (criar novo lead pelo postman)
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<InvitedLeadDto>> CreateLead([FromBody] CreateLeadDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (createdLead, errorMessage) = await _leadService.CreateLead(createDto);

            if (createdLead != null)
            {
                return CreatedAtAction(nameof(GetInvitedLeads), createdLead);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage ?? "An unexpected error occurred.");
            }
        }

        // POST: api/lead/{id}/accept (aceitar um lead específico)
        [HttpPost("{id}/accept")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AcceptLead(int id)
        {
            var (success, errorMessage) = await _leadService.AcceptLead(id);

            if (success)
            {
                return NoContent();
            }

            if (errorMessage.Contains("not found"))
                return NotFound(errorMessage);
            if (errorMessage.Contains("not in 'Invited' status"))
                return BadRequest(errorMessage);

            return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
        }

        // POST: api/lead/{id}/decline (rejeitar um lead específico)
        [HttpPost("{id}/decline")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeclineLead(int id)
        {
            var (success, errorMessage) = await _leadService.DeclineLead(id);

            if (success)
            {
                return NoContent();
            }

            if (errorMessage.Contains("not found"))
                return NotFound(errorMessage);
            if (errorMessage.Contains("not in 'Invited' status"))
                return BadRequest(errorMessage);

            return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
        }
    }
}