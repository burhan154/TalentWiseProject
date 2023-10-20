using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using WiseProject.Business.Abstract;
using WiseProject.Models;

namespace WiseProject.Business.Concrete
{
    public class CurrentUserManager : ICurrentUserService
    {
        private UserManager<ApplicationUser> _userManager;
        private ServiceManager _serviceManager;

        public CurrentUserManager(ServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            _userManager = _serviceManager.GetUserManager();  
        }

        public string UserId()
        {
            return _serviceManager.GetClaim(ClaimTypes.NameIdentifier);
        }

        public ApplicationUser GetUserById(string userId)
        {
            _userManager = _serviceManager.GetUserManager();
            return _userManager.FindByIdAsync(userId).Result;
        }

        public ApplicationUser User()
        {
            return GetUserById(UserId());
        }

        public bool IsInRole(string role)
        {
            return _userManager.GetRolesAsync(User()).Result.ToList().Contains(role);
        }

        public bool IsAdmin()
        {
            return _userManager.GetRolesAsync(User()).Result.ToList().Contains("Admin");
        }

        public List<string> Roles()
        {
            return _userManager.GetRolesAsync(User()).Result.ToList();
        }

        public bool IsLogged()
        {
            if(_serviceManager.GetClaim(ClaimTypes.NameIdentifier)=="")
                return false;
            return true;
        }

        public bool IsUser()
        {
            return _userManager.GetRolesAsync(User()).Result.ToList().Contains("User");
        }

        public bool IsInstructor()
        {
            return _userManager.GetRolesAsync(User()).Result.ToList().Contains("Instructor");
        }
    }
}
