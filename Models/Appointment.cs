namespace project_cache.Models
{
    public class Appointment : BaseEntity
    {
        public Guid DoctorId { get; private set; }
        public string PatientName { get; private set; }
        public DateTime AppointmentDateTime { get; private set; }
        public bool IsActive { get; private set; }

        private Appointment() { }

        public Appointment(Guid doctorId, string patientName, DateTime appointmentDateTime)
        {
            DoctorId = doctorId;
            PatientName = patientName;
            AppointmentDateTime = appointmentDateTime;
            IsActive = true;
        }

        public void UpdatePatientName(string patientName)
        {
            PatientName = patientName;
            UpdateDate();
        }

        public void UpdateAppointmentDateTime(DateTime appointmentDateTime)
        {
            AppointmentDateTime = appointmentDateTime;
            UpdateDate();
        }

        public void Cancel()
        {
            IsActive = false;
            UpdateDate();
        }
    }
}
