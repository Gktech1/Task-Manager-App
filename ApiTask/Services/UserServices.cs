using System.Threading.Tasks;
using ApiTask.Models;
using ApiTask.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ApiTask.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<AppUser> _userManager;
       // public AppUser User { get; set; }

        public UserServices(UserManager<AppUser> usermanager)
        {
            _userManager = usermanager;

        }


        public Task<AppUser> GetUserAsync(string id)
        {
            return _userManager.FindByIdAsync(id);
        }



        public async Task<ServiceReturnType<IdentityResult>> AddUserAsync(AppUser user, string password)
        {
            //validate the entity is not a null object
            if (user == null)
            {
                return new ServiceReturnType<IdentityResult>
                {
                    Status = false, Message = "Object must not be null", Data = null, Error = null
                };
            }
            // create user
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return new ServiceReturnType<IdentityResult>
                {
                    Status = false,
                    Message = "Failed to create user",
                    Data = null,
                    Error = result
                };
            }

            return new ServiceReturnType<IdentityResult>
            {
                Status = true,
                Message = "Added Successfully",
                Data = result,
                Error = null
            };

        }

        
        public class ServiceReturnType<T>
        {
            public bool Status { get; set; }
            public string Message { get; set; }
            public T Data { get; set; }
            public  T Error { get; set; }   
        }
        
        public async Task<bool> AlreadlyExistsAsync(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            if(result == null)
                return false;
            return true;
        }
    }
}
