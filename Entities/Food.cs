using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Food : BaseEntity
    {
        public string Caption { get; set; }
        public int Code { get; set; }

        public ICollection<FoodSchedule> FoodSchedules { get; set; }
    }

    public class FoodConfiguration : IEntityTypeConfiguration<Food>
    {
        public void Configure(EntityTypeBuilder<Food> builder)
        {
            builder.Property(s => s.Caption).IsRequired().HasMaxLength(50);
        }
    }
}
