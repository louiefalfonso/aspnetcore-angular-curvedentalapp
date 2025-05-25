namespace CurveDentalManagement.API.Models.Domain
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string AppointmentCode { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<Patient> Patients { get; set; }

    }
}