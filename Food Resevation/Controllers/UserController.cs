using AutoMapper;
using AutoMapper.QueryableExtensions;
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
using WebFramework.Api;
using WebFramework.Filters;

namespace Food_Resevation.Controllers
{
    public class UserController : CrudController<UserDTO, UserSelectDTO, User, int>
    {
        private readonly IJwtService jwtService;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;

        public UserController(IRepository<User> userRepository, IJwtService jwtService, UserManager<User> userManager,
            RoleManager<Role> roleManager, SignInManager<User> signInManager, IMapper mapper)
            : base(userRepository, mapper)
        {
            this.jwtService = jwtService;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        [Route("[action]")]
        public async override Task<ApiResult<UserSelectDTO>> Create(UserDTO dto, CancellationToken cancellationToken)
        {
            var user = dto.ToEntity(mapper);
            var result = await userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                throw new AppException(Common.ApiResultStatusCode.BadRequest, result.Errors);
            var selectUser = await repository.TableNoTracking.ProjectTo<UserSelectDTO>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(u => u.Id.Equals(user.Id), cancellationToken);
            return selectUser;
        }


        //[HttpPost]
        //[AllowAnonymous]        
        //public new async Task<ApiResult<UserSelectDTO>> Create(UserDTO userDto, CancellationToken cancellationToken)
        //{
        //    var user = userDto.ToEntity(mapper);
        //    var result = await userManager.CreateAsync(user, userDto.Password); //userRepository.AddAsync(user, userDto.Password, cancellationToken);
        //    if (!result.Succeeded)
        //        throw new AppException(Common.ApiResultStatusCode.BadRequest, result.Errors);
        //    var selectUser = await repository.TableNoTracking.ProjectTo<UserSelectDTO>(mapper.ConfigurationProvider)
        //        .SingleOrDefaultAsync(u => u.Id.Equals(user.Id), cancellationToken);
        //    return Ok(selectUser);
        //}

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

        public override Task<ApiResult<UserSelectDTO>> Get(int id, CancellationToken cancellationToken)
        {
            return base.Get(id, cancellationToken);
        }
    }
}
