using Microsoft.AspNetCore.Identity;

namespace DZIproject.Data
{
    public class Client:IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        ICollection<Shopping> Shoppings { get; set; }
    }
}
