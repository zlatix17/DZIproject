namespace DZIproject.Data
{
    public class Categorie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime RegisterOn { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
