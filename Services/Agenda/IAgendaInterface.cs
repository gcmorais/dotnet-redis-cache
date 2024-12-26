using project_cache.Dto.Agenda;
using project_cache.Models;

namespace project_cache.Services.Agenda
{
    public interface IAgendaInterface
    {
        Task<AgendaResponseModel<List<AgendaDto>>> ListarAgendas();
        Task<AgendaResponseModel<List<AgendaDto>>> EditarAgenda(AgendaDto agendaDto);
        Task<AgendaResponseModel<List<AgendaDto>>> ExcluirAgenda(Guid id);
        Task<AgendaResponseModel<List<AgendaDto>>> CriarAgenda(DoctorAgenda doctorAgenda);
        Task<AgendaResponseModel<AgendaDto>> BuscarAgendaPorId(Guid id);
    }
}
