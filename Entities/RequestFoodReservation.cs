using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RequestFoodReservation : BaseEntity<Guid>
    {
        public int UserId { get; set; }
        public long FoodScheduleId { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreationRegister { get; set; }

        public User User { get; set; }
        public FoodSchedule FoodSchedule { get; set; }
    }

    public class RequestFoodReservationConfiguration : IEntityTypeConfiguration<RequestFoodReservation>
    {
        public void Configure(EntityTypeBuilder<RequestFoodReservation> builder)
        {            
            builder.Property(s => s.UserId).IsRequired();
            builder.Property(s => s.FoodScheduleId).IsRequired();
            builder.Property(s => s.Date).IsRequired();
            builder.Property(s => s.CreationRegister).IsRequired().HasDefaultValue(DateTime.Now).Metadata.SetBeforeSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Throw);
            builder.HasOne(s => s.User).WithMany(d => d.RequestFoodReservations).HasForeignKey(s => s.UserId);
            builder.HasOne(s => s.FoodSchedule).WithMany(d => d.RequestFoodReservations).HasForeignKey(s => s.FoodScheduleId);
        }
    }
}
