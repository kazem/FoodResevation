using Common.Exceptions;
using Data;
using Entities;
using Food_Resevation.Models;
using Microsoft.AspNetCore.Authorization;
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

        public UserController(IUserRepository userRepository, IJwtService jwtService)
        {
            this.userRepository = userRepository;
            this.jwtService = jwtService;
        }

        [HttpPost]
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
                Username = userDto.Username
            };
            await userRepository.AddAsync(user, userDto.Password, cancellationToken);
            return user;
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<string> Token(string username, string password, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByUsernameAndPassword(username, password, cancellationToken);
            if (user == null)
                throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");
            return jwtService.GenerateAsync(user);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<User>>> Get(CancellationToken cancellationToken)
        {
            var users = await userRepository.TableNoTracking.ToListAsync(cancellationToken);
            return Ok(users);
        }
    }
}
