namespace CurveDentalManagement.API.Models.DTO
{
    public class StaffDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StaffRole { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public string Age { get; set; }
        public string Address { get; set; }
    }
}
