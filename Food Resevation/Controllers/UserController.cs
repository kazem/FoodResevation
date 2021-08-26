using Common.Exceptions;
using Data;
using Entities;
using Food_Resevation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework;
using WebFramework.Filters;

namespace Food_Resevation.Controllers
{
    [Route("api/[controller]")]
    [ApiResultFilter]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IJwtService jwtService;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;

        public UserController(IUserRepository userRepository, IJwtService jwtService, UserManager<User> userManager,
            RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            this.userRepository = userRepository;
            this.jwtService = jwtService;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResult<User>> Create(UserDTO userDto, CancellationToken cancellationToken)
        {

            //var exists = await userRepository.TableNoTracking.AnyAsync(p => p.UserName == userDto.UserName);
            //if (exists)
            //    return BadRequest("نام کاربری تکراری است");

            var user = new User
            {
                Age = userDto.Age,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Gender = userDto.Gender,
                UserName = userDto.Username,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber
               
            };
            var result = await userManager.CreateAsync(user, userDto.Password); //userRepository.AddAsync(user, userDto.Password, cancellationToken);
            if (!result.Succeeded)
                    throw new AppException(Common.ApiResultStatusCode.BadRequest, result.Errors);
            return user;
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<string> Token(string username, string password, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(username);
            //var user = await userRepository.GetByUsernameAndPassword(username, password, cancellationToken);
            if (user == null)
                throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");  
            
            var isValidPassword = await userManager.CheckPasswordAsync(user, password);
            if (!isValidPassword)
                throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

            return await jwtService.GenerateAsync(user);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<User>>> Get(int id, CancellationToken cancellationToken)
        {
            var user2 = await userManager.FindByIdAsync(id.ToString());
            var users = await userRepository.TableNoTracking.ToListAsync(cancellationToken);
            return Ok(users);
        }
    }
}
