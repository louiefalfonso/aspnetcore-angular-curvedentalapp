namespace CurveDentalManagement.API.Models.DTO
{
    public class CreateBillingRequestDto
    {
        public string BillingCode { get; set; }
        public string TotalAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime BillingDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Remarks { get; set; }
        public Guid[] Patients { get; set; }
        public Guid[] Treatments { get; set; }
    }
}
