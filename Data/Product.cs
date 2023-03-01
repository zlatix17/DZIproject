using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace DZIproject.Data
{
    public enum Gender
        {
            Women, Men
        }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Categories { get; set; }
        public string Size { get; set; }
        public Gender Gender { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime RegisterOn { get; set; }
        ICollection<Shopping> Shoppings { get; set; }
    }
}
