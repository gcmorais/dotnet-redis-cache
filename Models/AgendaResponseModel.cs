namespace project_cache.Models
{
    public class AgendaResponseModel<T>
    {
        public T? Medicos { get; set; }
        public Boolean Status { get; set; }
    }
}
