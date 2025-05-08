namespace CurveDentalManagement.API.Models.DTO
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string AppointmentCode { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public List<DoctorDto> Doctors { get; set; } = new List<DoctorDto>();
        public List<PatientDto> Patients { get; set; } = new List<PatientDto>();
    }
}
