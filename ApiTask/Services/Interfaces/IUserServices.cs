using System.Threading.Tasks;
using ApiTask.Models;
using Microsoft.AspNetCore.Identity;
using Task = ApiTask.Models.Task;

namespace ApiTask.Services.Interfaces
{
    public interface IUserServices
    {
        Task<ServiceReturnType<IdentityResult>> AddUserAsync(AppUser user, string password); 
        Task<AppUser> GetUserAsync(string id);
        Task<bool> AlreadlyExistsAsync(string email);
        Task<IdentityResult> AddRoleAsync(AppUser user, string roleName);
    }
}
