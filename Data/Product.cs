namespace DZIproject.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Categories { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public enum Gender
        {
            Women, Men
        }
        public decimal Price { get; set; }
        public DateTime RegisterOn { get; set; }
        ICollection<Shopping> Shoppings { get; set; }
    }
}
