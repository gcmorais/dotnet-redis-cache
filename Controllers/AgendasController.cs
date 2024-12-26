using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using project_cache.Dto.Agenda;
using project_cache.Models;
using project_cache.Services.Agenda;

namespace project_cache.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgendasController : ControllerBase
    {
        private readonly IAgendaInterface _agendaInterface;
        private readonly IValidator<DoctorAgenda> _doctorAgendaValidator;

        public AgendasController(IAgendaInterface agendaInterface, IValidator<DoctorAgenda> doctorAgendaValidator)
        {
            _agendaInterface = agendaInterface;
            _doctorAgendaValidator = doctorAgendaValidator;
        }

        [HttpGet]
        public async Task<ActionResult<AgendaResponseModel<List<AgendaDto>>>> ListarAgendas()
        {
            var agendas = await _agendaInterface.ListarAgendas();
            return Ok(agendas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AgendaResponseModel<AgendaDto>>> ListarAgendasPorId(Guid id)
        {
            var agenda = await _agendaInterface.BuscarAgendaPorId(id);
            return Ok(agenda);
        }

        [HttpPost("create")]
        public async Task<ActionResult<AgendaResponseModel<List<AgendaDto>>>> CriarAgenda([FromBody] DoctorAgenda doctorAgenda)
        {
            var validationResult = await _doctorAgendaValidator.ValidateAsync(doctorAgenda); // Validar a DoctorAgenda

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var agenda = await _agendaInterface.CriarAgenda(doctorAgenda);
            return Ok(agenda);
        }

        [HttpPut("edit")]
        public async Task<ActionResult<AgendaResponseModel<List<AgendaDto>>>> EditarAgenda([FromBody] AgendaDto agendaDto)
        {
            var agenda = await _agendaInterface.EditarAgenda(agendaDto);

            if (!agenda.Status)
            {
                return BadRequest("Erro ao editar a agenda.");
            }

            return Ok(agenda);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AgendaResponseModel<List<AgendaDto>>>> ExcluirAgenda(Guid id)
        {
            var agenda = await _agendaInterface.ExcluirAgenda(id);

            if (!agenda.Status)
            {
                return BadRequest("Erro ao excluir a agenda.");
            }

            return Ok(agenda);
        }
    }
}
