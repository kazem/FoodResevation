using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities
{
    public class PeriodDefinition : BaseEntity
    {
        public string Caption { get; set; }
        public long RestaurantId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public Guid CreatorId { get; set; }
        public DateTime CreationDate { get; set; }

        public Restaurant Restaurant { get; set; }
        public User Creator { get; set; }
        public ICollection<FoodSchedule> FoodSchedules { get; set; }

    }

    public class PeriodDefinitionConfiguration : IEntityTypeConfiguration<PeriodDefinition>
    {
        public void Configure(EntityTypeBuilder<PeriodDefinition> builder)
        {
            builder.Property(s => s.Caption).IsRequired().HasMaxLength(200);
            builder.Property(s => s.FromDate).IsRequired();
            builder.Property(s => s.FromDate).IsRequired();
            builder.Property(s => s.ToDate).IsRequired();
            builder.Property(s => s.RestaurantId).IsRequired();
            builder.Property(s => s.CreationDate).HasDefaultValue(DateTime.Now).Metadata.SetBeforeSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Throw);
            builder.HasOne(s => s.Restaurant).WithMany(d => d.PeriodDefinitions).HasForeignKey(s => s.RestaurantId);
            builder.HasOne(s => s.Creator).WithMany(d => d.PeriodDefinitionsCreated).HasForeignKey(s => s.CreatorId);
            builder.HasMany(s => s.FoodSchedules).WithOne(d => d.PeriodDefinition).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
