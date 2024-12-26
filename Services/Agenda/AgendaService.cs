using AutoMapper;
using Microsoft.EntityFrameworkCore;
using project_cache.Data;
using project_cache.Dto.Agenda;
using project_cache.Models;

namespace project_cache.Services.Agenda
{
    public class AgendaService : IAgendaInterface
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AgendaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AgendaResponseModel<AgendaDto>> BuscarAgendaPorId(Guid id)
        {
            AgendaResponseModel<AgendaDto> response = new();

            try
            {
                var agenda = await _context.Agenda.FirstOrDefaultAsync(a => a.Id == id);

                if (agenda == null)
                {
                    response.Status = false;
                    return response;
                }

                response.Medicos = _mapper.Map<AgendaDto>(agenda);
                response.Status = true;

                return response;
            }
            catch (Exception)
            {
                response.Status = false;
                return response;
            }
        }

        public async Task<AgendaResponseModel<List<AgendaDto>>> CriarAgenda(DoctorAgenda doctorAgenda)
        {
            var response = new AgendaResponseModel<List<AgendaDto>>();

            try
            {
                var agenda = new DoctorAgenda(doctorAgenda.Name, doctorAgenda.Specialty, doctorAgenda.Schedules);

                _context.Add(agenda);
                await _context.SaveChangesAsync();

                var agendas = await _context.Agenda.Where(a => a.IsActive).ToListAsync();
                response.Medicos = _mapper.Map<List<AgendaDto>>(agendas);
                response.Status = true;

                return response;
            }
            catch (Exception)
            {
                response.Status = false;
                return response;
            }
        }

        public async Task<AgendaResponseModel<List<AgendaDto>>> EditarAgenda(AgendaDto agendaDto)
        {
            var response = new AgendaResponseModel<List<AgendaDto>>();

            try
            {
                var agenda = await _context.Agenda
                    .FirstOrDefaultAsync(a => a.Id == agendaDto.Id);

                if (agenda == null)
                {
                    response.Status = false;
                    return response;
                }

                agenda.UpdateName(agendaDto.Name);
                agenda.UpdateSpecialty(agendaDto.Specialty);
                agenda.UpdateSchedules(agendaDto.Schedules);

                await _context.SaveChangesAsync();

                response.Medicos = await _context.Agenda
                    .Select(a => new AgendaDto
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Specialty = a.Specialty,
                        Schedules = a.Schedules
                    }).ToListAsync();

                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                return response;
            }
        }


        public async Task<AgendaResponseModel<List<AgendaDto>>> ExcluirAgenda(Guid id)
        {
            var response = new AgendaResponseModel<List<AgendaDto>>();

            try
            {
                var agenda = await _context.Agenda.FirstOrDefaultAsync(a => a.Id == id);

                if (agenda == null)
                {
                    response.Status = false;
                    return response;
                }

                _context.Remove(agenda);
                await _context.SaveChangesAsync();

                response.Medicos = await _context.Agenda
                    .Select(a => new AgendaDto
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Specialty = a.Specialty,
                        Schedules = a.Schedules
                    }).ToListAsync();

                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                return response;
            }
        }

        public async Task<AgendaResponseModel<List<AgendaDto>>> ListarAgendas()
        {
            var response = new AgendaResponseModel<List<AgendaDto>>();

            try
            {
                var agendas = await _context.Agenda.ToListAsync();
                response.Medicos = _mapper.Map<List<AgendaDto>>(agendas);
                response.Status = true;

                return response;
            }
            catch (Exception)
            {
                response.Status = false;
                return response;
            }
        }
    }
}
