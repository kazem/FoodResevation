using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FoodSchedule : BaseEntity
    {
        public long PeriodDefinitionId { get; set; }
        public long FoodId { get; set; }
        public DateTime Date { get; set; }
        public DayOfWeek DayOfWeek { get; set; }        

        public PeriodDefinition PeriodDefinition { get; set; }
        public Food Food { get; set; }
        public ICollection<RequestFoodReservation> RequestFoodReservations { get; set; }
    }

    public class FoodScheduleConfiguration : IEntityTypeConfiguration<FoodSchedule>
    {
        public void Configure(EntityTypeBuilder<FoodSchedule> builder)
        {
            builder.Property(s => s.PeriodDefinitionId).IsRequired();
            builder.Property(s => s.FoodId).IsRequired();
            builder.Property(s => s.Date).IsRequired();
            builder.HasOne(s => s.PeriodDefinition).WithMany(d => d.FoodSchedules).HasForeignKey(s => s.PeriodDefinitionId);
            builder.HasOne(s => s.Food).WithMany(d => d.FoodSchedules).HasForeignKey(s => s.FoodId);
        }
    }
}
