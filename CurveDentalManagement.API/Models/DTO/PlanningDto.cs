namespace CurveDentalManagement.API.Models.DTO
{
    public class PlanningDto
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public DateOnly TreatmentPlanDate { get; set; }
        public List<DoctorDto> Doctors { get; set; } = new List<DoctorDto>();
        public List<PatientDto> Patients { get; set; } = new List<PatientDto>();
        public List<TreatmentDto> Treatments { get; set; } = new List<TreatmentDto>();
    }
}
