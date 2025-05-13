namespace CurveDentalManagement.API.Models.DTO
{
    public class UpdateAppointmentRequestDto
    {
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string AppointmentCode { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public List<Guid> Doctors { get; set; } = new List<Guid>();
        public List<Guid> Patients { get; set; } = new List<Guid>();
    }
}
