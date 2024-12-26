namespace project_cache.Models
{
    public class AppointmentResponseModel<T>
    {
        public string? Mensagem { get; set; }
        public T? Agendamento { get; set; }

    }
}
