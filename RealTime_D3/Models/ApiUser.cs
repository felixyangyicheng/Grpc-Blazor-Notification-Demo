using Microsoft.AspNetCore.Identity;

namespace RealTime_D3.Models
{
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
    }
}
