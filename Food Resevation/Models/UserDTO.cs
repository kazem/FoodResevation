using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace Food_Resevation.Models
{
    public class UserDTO : BaseDto<UserDTO, User>, IValidatableObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte Age { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (UserName.Equals(null) || Password.Equals(null))
                yield return new ValidationResult("UserName and Password are required", new[] { nameof(UserName), nameof(Password) });
        }
    }

    public class UserSelectDTO : BaseDto<UserSelectDTO, User>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte Age { get; set; }
        public string FullName { get; set; }

        public override void CustomMappings(AutoMapper.IMappingExpression<User, UserSelectDTO> mapping)
        {
            mapping.ForMember(
                dest => dest.FullName,
                config => config.MapFrom(src => src.FirstName + " " + src.LastName));
        }
    }
}
