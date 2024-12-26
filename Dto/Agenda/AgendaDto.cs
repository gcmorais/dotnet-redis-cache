namespace project_cache.Dto.Agenda
{
    public class AgendaDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public List<DateTime> Schedules { get; set; }
    }
}
