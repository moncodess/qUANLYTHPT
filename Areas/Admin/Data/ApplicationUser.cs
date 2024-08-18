using Microsoft.AspNetCore.Identity;
using qUANLYTHPT.Areas.Admin.Models;

namespace qUANLYTHPT.Areas.Admin.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}