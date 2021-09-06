using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    public class User : IdentityUser<int>, IEntity<int>
    {
        public User()
        {
            IsActive = true;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public byte Age { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
        public bool IsActive { get; set; }

        public ICollection<PeriodDefinition> PeriodDefinitionsCreated { get; set; }
        public ICollection<RequestFoodReservation> RequestFoodReservations { get; set; }
    }

    public enum Gender
    {
        [Display(Name="مرد")]
        Male = 1,
        [Display(Name = "زن")]
        Female = 2
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(s => s.UserName).IsRequired().HasMaxLength(20);
        }
    }
}
