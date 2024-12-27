using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using project_cache.Caching;
using project_cache.Data;
using project_cache.Dto.Agenda;
using project_cache.Models;

namespace project_cache.Services.Agenda
{
    public class AgendaService : IAgendaInterface
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICachingService _cache;

        public AgendaService(ICachingService cache, AppDbContext context, IMapper mapper)
        {
            _cache = cache;
            _context = context;
            _mapper = mapper;
        }

        public async Task<AgendaResponseModel<AgendaDto>> BuscarAgendaPorId(Guid id)
        {
            var response = new AgendaResponseModel<AgendaDto>();

            try
            {
                var agendaCache = await _cache.GetAsync(id.ToString());

                if (!string.IsNullOrWhiteSpace(agendaCache))
                {
                    var cachedAgenda = JsonConvert.DeserializeObject<AgendaDto>(agendaCache);
                    Console.WriteLine("Loaded from cache.");
                    response.Medicos = cachedAgenda;
                    response.Status = true;
                    return response;
                }

                var agenda = await _context.Agenda.FirstOrDefaultAsync(a => a.Id == id);

                if (agenda == null)
                {
                    response.Status = false;
                    return response;
                }

                var agendaDto = _mapper.Map<AgendaDto>(agenda);
                await _cache.SetAsync(id.ToString(), JsonConvert.SerializeObject(agendaDto));

                response.Medicos = agendaDto;
                response.Status = true;
                return response;
            }
            catch (Exception ex)
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
                var agendaDtos = _mapper.Map<List<AgendaDto>>(agendas);

                await _cache.SetAsync("AllAgendas", JsonConvert.SerializeObject(agendaDtos));

                response.Medicos = agendaDtos;
                response.Status = true;

                return response;
            }
            catch (Exception ex)
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
                var agenda = await _context.Agenda.FirstOrDefaultAsync(a => a.Id == agendaDto.Id);

                if (agenda == null)
                {
                    response.Status = false;
                    return response;
                }

                agenda.UpdateName(agendaDto.Name);
                agenda.UpdateSpecialty(agendaDto.Specialty);
                agenda.UpdateSchedules(agendaDto.Schedules);

                await _context.SaveChangesAsync();

                var updatedAgendaDto = _mapper.Map<AgendaDto>(agenda);
                await _cache.SetAsync(agendaDto.Id.ToString(), JsonConvert.SerializeObject(updatedAgendaDto));

                var agendas = await _context.Agenda.ToListAsync();
                var agendaDtos = _mapper.Map<List<AgendaDto>>(agendas);

                await _cache.SetAsync("AllAgendas", JsonConvert.SerializeObject(agendaDtos));

                response.Medicos = agendaDtos;
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

                await _cache.RemoveAsync(id.ToString());

                var agendas = await _context.Agenda.ToListAsync();
                var agendaDtos = _mapper.Map<List<AgendaDto>>(agendas);

                await _cache.SetAsync("AllAgendas", JsonConvert.SerializeObject(agendaDtos));

                response.Medicos = agendaDtos;
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
                var agendasCache = await _cache.GetAsync("AllAgendas");

                if (!string.IsNullOrWhiteSpace(agendasCache))
                {
                    var cachedAgendas = JsonConvert.DeserializeObject<List<AgendaDto>>(agendasCache);
                    Console.WriteLine("Loaded all agendas from cache.");
                    response.Medicos = cachedAgendas;
                    response.Status = true;
                    return response;
                }

                var agendas = await _context.Agenda.ToListAsync();
                var agendaDtos = _mapper.Map<List<AgendaDto>>(agendas);

                await _cache.SetAsync("AllAgendas", JsonConvert.SerializeObject(agendaDtos));

                response.Medicos = agendaDtos;
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                return response;
            }
        }
    }
}
