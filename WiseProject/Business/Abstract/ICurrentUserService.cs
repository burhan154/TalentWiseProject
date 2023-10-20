using Microsoft.AspNetCore.Identity;
using WiseProject.Models;

namespace WiseProject.Business.Abstract
{
    public interface ICurrentUserService
    {
        public string UserId();
        public bool IsLogged();
        public ApplicationUser User();
        public bool IsInRole(string role);
        public List<string> Roles();
        public bool IsAdmin();
        public bool IsUser();
        public bool IsInstructor();
        ApplicationUser GetUserById(string userId);
    }
}
