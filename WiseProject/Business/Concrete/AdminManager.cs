using Microsoft.AspNetCore.Identity;
using System.Data;
using WiseProject.Business.Abstract;
using WiseProject.Data.Results;
using WiseProject.Models;
using WiseProject.Models.Dto;
using IResult = WiseProject.Data.Results.IResult;

namespace WiseProject.Business.Concrete
{
    public class AdminManager : IAdminService
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ServiceManager _serviceManager;


        public AdminManager(ServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            _roleManager = _serviceManager.GetRoleManager();
            _userManager = _serviceManager.GetUserManager();
        }

        public IResult ChangeRole(string userId,string newRole)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            var role = _userManager.GetRolesAsync(user).Result.SingleOrDefault();
            var r =_userManager.RemoveFromRoleAsync(user, role).Result;
            var c =_userManager.AddToRoleAsync(user, newRole).Result;
            return new SuccessResult();
        }


        private UserRole GetUsersWithRole(string role)
        {
            var userRole = new UserRole()
            {
                Users = _userManager.GetUsersInRoleAsync(role).Result.ToList(),
                Roles = GetRoles().Data,
                CurrentRole= role
            };
            return userRole;
        }

        public IDataResult<UserRole> GetAdmins()
        {
            return new SuccessDataResult<UserRole>(GetUsersWithRole("Admin"));
        }

        public IDataResult<UserRole> GetInstructors()
        {
            return new SuccessDataResult<UserRole>(GetUsersWithRole("Instructor"));       
        }

        public IDataResult<UserRole> GetUsers()
        {
            return new SuccessDataResult<UserRole>(GetUsersWithRole("User"));
        }

        public  IResult AddRole(string role)
        {
            var roleExists = _roleManager.RoleExistsAsync(role);
            if (!roleExists.Result)
            {
                var roleResult = _roleManager.CreateAsync(new IdentityRole(role));
                if (roleResult.Result.Succeeded)
                {
                    return new SuccessResult();
                }
                return new ErrorResult();
            }
            return new ErrorResult();
        }

        public IDataResult<List<IdentityRole>> GetRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return new SuccessDataResult<List<IdentityRole>>(roles);
        }

        public IResult RemoveRole(string id)
        {
            var role = _roleManager.FindByIdAsync(id).Result;
            var result = _roleManager.DeleteAsync(role).Result.Succeeded;
            return new SuccessResult();
        }
    }
}
