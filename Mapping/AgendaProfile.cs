using AutoMapper;
using project_cache.Dto.Agenda;
using project_cache.Models;

namespace project_cache.Mapping
{
    public class AgendaProfile : Profile
    {
        public AgendaProfile()
        {
            CreateMap<DoctorAgenda, AgendaDto>();
        }
    }
}
