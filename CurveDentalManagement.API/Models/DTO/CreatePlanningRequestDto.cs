namespace CurveDentalManagement.API.Models.DTO
{
    public class CreatePlanningRequestDto
    {
        public string Status { get; set; }
        public string Remarks { get; set; }
        public DateOnly TreatmentPlanDate { get; set; }
        public Guid[] Doctors { get; set; }
        public Guid[] Patients { get; set; }
        public Guid[] Treatments { get; set; }
    }
}
