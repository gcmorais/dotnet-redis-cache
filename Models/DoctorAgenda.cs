namespace project_cache.Models
{
    public class DoctorAgenda : BaseEntity
    {
        public string Name { get; private set; }
        public string Specialty { get; private set; }
        public List<DateTime> Schedules { get; private set; }
        public bool IsActive { get; private set; }

        private DoctorAgenda()
        {
            Schedules = new List<DateTime>();
        }

        public DoctorAgenda(string name, string specialty, List<DateTime> schedules)
        {
            Name = name;
            Specialty = specialty;
            Schedules = schedules ?? new List<DateTime>();
            IsActive = true;
        }

        public void UpdateName(string name)
        {
            Name = name;
            UpdateDate();
        }

        public void UpdateSpecialty(string specialty)
        {
            Specialty = specialty;
            UpdateDate();
        }

        public void UpdateSchedules(List<DateTime> schedules)
        {
            Schedules = schedules ?? new List<DateTime>();
            UpdateDate();
        }

        public void AddSchedule(DateTime schedule)
        {
            if (!Schedules.Contains(schedule))
            {
                Schedules.Add(schedule);
                UpdateDate();
            }
        }

        public void RemoveSchedule(DateTime schedule)
        {
            if (Schedules.Contains(schedule))
            {
                Schedules.Remove(schedule);
                UpdateDate();
            }
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdateDate();
        }
    }
}
