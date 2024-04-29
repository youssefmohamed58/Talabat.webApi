using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.DTOS;
using Talabat.Apis.Extensions;
using Talabat.Core.Entities.Identity_Entities;
using Talabat.Service;

namespace Talabat.Apis.Controllers
{
    public class accountsController : BaseController
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly SignInManager<AppUser> _signinmanager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public accountsController(UserManager<AppUser> usermanager , SignInManager<AppUser>  signinmanager , ITokenService tokenService , IMapper mapper)
        {
            _usermanager = usermanager;
            _signinmanager = signinmanager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTo>> Register (RegisterDto model)
        {
            if(CheckEmailExists(model.Email).Result.Value)
                return BadRequest("Email is Already Existed");
            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email.Split('@')[0],   
            };

            var Result =await  _usermanager.CreateAsync(user, model.Password);

            if (!Result.Succeeded)
                return BadRequest();

            var ReturnedUser = new UserDTo()
            {
                Email = user.Email,    
                DisplayName = user.DisplayName,
                Token = await _tokenService.CreateTokenAsync(user, _usermanager)
            };

            return Ok(ReturnedUser);

        
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTo>> Login (LoginDto model)
        {
            var User = await _usermanager.FindByEmailAsync(model.Email);
            if(User == null) return Unauthorized(); 
 
           var Result = await _signinmanager.CheckPasswordSignInAsync(User, model.Password , false);

           if (!Result.Succeeded)
                return Unauthorized();

            var ReturnedUser = new UserDTo()
            {
                Email = User.Email, 
                DisplayName = User.DisplayName,
                Token = await _tokenService.CreateTokenAsync(User, _usermanager)
            };

            return Ok(ReturnedUser);


        }
        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDTo>> GetCurrentUser()
        {
            var Email =  User.FindFirstValue(ClaimTypes.Email);

            var user = await _usermanager.FindByEmailAsync(Email);
            if (user == null) return Unauthorized();

            var ReturnedUser = new UserDTo()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _tokenService.CreateTokenAsync(user, _usermanager)
            };

            return Ok(ReturnedUser);


        }
        [HttpGet("GetAddress")]
        public  async Task<ActionResult<AddressDto>> GetAddress()
        {
            var user = await _usermanager.FindUserWithAddressAsync(User);
            var MappedAddress = _mapper.Map<Address,AddressDto>(user.Address);
            return Ok(MappedAddress);   
        }
        [Authorize]
        [HttpPut("UpdateAddress")]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto UpdatedAddress)
        {
            var user = await _usermanager.FindUserWithAddressAsync(User);
            if (user == null) return Unauthorized();
            var Address = _mapper.Map<AddressDto, Address>(UpdatedAddress);
            Address.Id = user.Address.Id;
            user.Address = Address;
            var Result = await _usermanager.UpdateAsync(user);
            if(!Result.Succeeded)
               return BadRequest();
            return Ok(UpdatedAddress);
        }

        [HttpGet("CheckEmailExists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            
            return await _usermanager.FindByEmailAsync(email) is not null;
        }
    }
}
