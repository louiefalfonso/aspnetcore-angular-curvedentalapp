namespace CurveDentalManagement.API.Models.Domain
{
    public class Billing
    {
        public Guid Id { get; set; }
        public string BillingCode { get; set; }
        public string TotalAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime BillingDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Remarks { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public ICollection<Treatment> Treatments { get; set; }
    }
}
