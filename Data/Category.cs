namespace DZIproject.Data
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime RegisterOn { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
