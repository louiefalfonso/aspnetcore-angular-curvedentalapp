namespace CurveDentalManagement.API.Models.Domain
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Specialization { get; set; }
        public string Department { get; set; }
        public string Schedule { get; set; }
        public string LicenseNumber { get; set; }
        public string YearsOfExperience { get; set; }
        public string DentalSchool { get; set; }
        public string OfficeAddress { get; set; }
        public string EmergencyContact { get; set; }
        public ICollection<Treatment> Treatments { get; set; }
    }
}
