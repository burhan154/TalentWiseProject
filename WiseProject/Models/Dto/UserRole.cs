using Microsoft.AspNetCore.Identity;

namespace WiseProject.Models.Dto
{
    public class UserRole
    {
        public List<ApplicationUser> Users { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public string CurrentRole { get; set; }
    }
}
