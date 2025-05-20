namespace CurveDentalManagement.API.Models.Domain
{
    public class Planning
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public DateOnly TreatmentPlanDate { get; set; }
        public ICollection<Treatment> Treatments { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<Patient> Patients { get; set; }
    }
}
