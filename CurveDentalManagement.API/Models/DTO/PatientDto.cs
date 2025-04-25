namespace CurveDentalManagement.API.Models.DTO
{
    public class PatientDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Age { get; set; }
        public string Address { get; set; }
        public string InsuranceDetails { get; set; }
        public string InsuranceProvider { get; set; }
        public string InsurancePolicyNumber { get; set; }
        public DateTime InsuranceExpiryDate { get; set; }
    }
}
