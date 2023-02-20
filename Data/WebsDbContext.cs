using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DZIproject.Data
{
    public class WebsDbContext : IdentityDbContext<Client>
    {
        public WebsDbContext(DbContextOptions<WebsDbContext> options)
            : base(options)
        {
        }
        public DbSet<Categorie> Categories {get;set;}
        public DbSet<Product> Products { get; set; }
        public DbSet<Shopping> Shoppings { get; set; }
    }
}