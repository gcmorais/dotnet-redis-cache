using Microsoft.EntityFrameworkCore;
using project_cache.Models;

namespace project_cache.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<DoctorAgenda> Agenda { get; set; }
        public DbSet<Appointment> Agendamento { get; set; }
    }
}
