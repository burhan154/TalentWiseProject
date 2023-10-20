using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WiseProject.Data.Results;
using WiseProject.Models;
using IResult = WiseProject.Data.Results.IResult;

namespace WiseProject.Business.Concrete
{
    public class ServiceManager
    {
        private readonly IHttpContextAccessor _httpContext;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IServiceProvider service;

        public ServiceManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor;
            GetService();
        }

        private void GetService()
        {
            service = _httpContext.HttpContext.RequestServices;
            _userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
            _roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
        }

        public UserManager<ApplicationUser> GetUserManager()
        {
            //if (service == null || _userManager == null)
            GetService();
            return _userManager;
        }

        public RoleManager<IdentityRole> GetRoleManager()
        {
            //if (service == null || _userManager == null)
            GetService();
            return _roleManager;
        }

        public string GetClaim(string claim)
        {
            var userType = _httpContext.HttpContext.User.FindFirst(claim);

            if (userType == null || !userType.Subject.IsAuthenticated)
                return "";
            return userType.Value;
        }


        public async Task<IResult> Register(RegisterViewModel model)
        {
            GetUserManager();
            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email ,FirstName = model.FirstName, LastName= model.LastName };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            //_userManager.AddToRoleAsync(user.Id, "User");
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        /*public List<ApplicationUser> GetUsers()
        {
            GetUserManager();
            //_userManager.Users.ToList();     
            return _userManager.GetUsersInRoleAsync("User").Result.ToList();
        }
        public List<ApplicationUser> GetInstructor()
        {
            GetUserManager();
            return _userManager.GetUsersInRoleAsync("Instructor").Result.ToList();
        }


        public void ChangeRole(ApplicationUser user,string currentRole,string newRole)
        {
            GetUserManager();
            _userManager.RemoveFromRoleAsync(user, currentRole);
            _userManager.AddToRoleAsync(user, newRole);
            
        }*/
    }
}
