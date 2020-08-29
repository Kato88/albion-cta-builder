using Microsoft.AspNetCore.Identity;

namespace zergtool
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}