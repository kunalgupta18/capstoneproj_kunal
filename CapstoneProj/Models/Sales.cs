namespace CapstoneProj.Models
{
    public class Sales
    {
        public int SalesId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentDetails { get; set; }
    }
}
