using Microsoft.AspNetCore.Identity;
using WiseProject.Data.Results;
using WiseProject.Models;
using WiseProject.Models.Dto;
using IResult = WiseProject.Data.Results.IResult;

namespace WiseProject.Business.Abstract
{
    public interface IAdminService
    {
        public IDataResult<List<IdentityRole>> GetRoles();
        public IDataResult<UserRole> GetAdmins();
        public IDataResult<UserRole> GetUsers();
        public IDataResult<UserRole> GetInstructors();
        public IResult ChangeRole(string userId, string newRole);
        IResult AddRole(string role);
        IResult RemoveRole(string id);
    }
}
