using Microsoft.AspNetCore.Identity;

namespace CoMute.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Phone { get; set; }   
        public string  FirstName { get; set; }
        public string Surname { get; set; }
    }
}
