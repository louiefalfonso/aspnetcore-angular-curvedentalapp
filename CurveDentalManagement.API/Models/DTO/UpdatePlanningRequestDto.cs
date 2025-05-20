namespace CurveDentalManagement.API.Models.DTO
{
    public class UpdatePlanningRequestDto
    {
        public string Status { get; set; }
        public string Remarks { get; set; }
        public DateOnly TreatmentPlanDate { get; set; }
        public List<Guid> Doctors { get; set; } = new List<Guid>();
        public List<Guid> Patients { get; set; } = new List<Guid>();
        public List<Guid> Treatments { get; set; } = new List<Guid>();
    }
}
