namespace CurveDentalManagement.API.Models.DTO
{
    public class CreateAppointmentRequestDto
    {
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string AppointmentCode { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public Guid[] Doctors {  get; set; }
        public Guid[] Patients {  get; set; }
    }
}
