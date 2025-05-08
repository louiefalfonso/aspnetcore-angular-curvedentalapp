namespace CurveDentalManagement.API.Models.Domain
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string AppointmentCode { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<Patient> Patients { get; set; }

    }
}