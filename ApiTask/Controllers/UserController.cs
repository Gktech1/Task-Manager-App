using System.ComponentModel;
using System.Threading.Tasks;
using ApiTask.DTOs;
using ApiTask.Models;
using ApiTask.Security;
using ApiTask.Services;
using ApiTask.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;
        private readonly IJWTSecurity _jwt;

        public UserController(IUserServices userServices, IMapper mapper, IJWTSecurity jwt)
        {
            _userServices = userServices;
            _mapper = mapper;
            _jwt = jwt;

        }

        [HttpGet("GetToken")]
        // [Authorize]
        public async Task<IActionResult> GetToken(AppUser user, string id)
        {
            
            user = await _userServices.GetUserAsync(id);
            var token = _jwt.JWTGen(user);
            return Ok(token);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(AppUserToAddDto userDto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid Model");
                return BadRequest(ModelState);
            }

            //Check if user email already exists
            if (await _userServices.AlreadlyExistsAsync(userDto.Email))
            {
               ModelState.AddModelError("rt", "Email already exists");
                return BadRequest(ModelState["rt"]);
            }


            //map dto to user model
            var user = _mapper.Map<AppUser>(userDto);
            //manual mapping
          //  user.UserName = userDto.Email;

            // Create User
            var result = await _userServices.AddUserAsync(user, userDto.Password);
            if (result.Status == false)
            {
                foreach (var error in result.Error.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            // add role to user
            var roleResult = await _userServices.AddRoleAsync(user, userDto.Role);
            if (!roleResult.Succeeded)
            {
                foreach (var error in result.Error.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

             
            return Ok($"Added Successfully! => Id: " + user.Id);

        }
        
        [HttpGet ("Get-User/{Id}")]
        public async Task<IActionResult> GetUser(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
                return BadRequest("Null entry for Id");
             
            var user = await _userServices.GetUserAsync(Id);

            if (user == null)
                return NotFound($"User with Id: {Id} was not found");

            //map user to user to return dto
            var userToReturn = _mapper.Map<UserDetailsToReturnDto>(user);

            return Ok(userToReturn);
        }

    }

}
