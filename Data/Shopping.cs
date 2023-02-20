namespace DZIproject.Data
{
    public class Shopping
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Clients { get; set; }
        public int ProductId { get; set; }
        public Product Products { get; set; }
        public int Quantity { get; set; }
        public decimal TotalSum { get; set; }
        public DateTime RegisterOn { get; set; }
    }
}
