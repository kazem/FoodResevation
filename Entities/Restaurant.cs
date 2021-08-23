using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Restaurant : BaseEntity
    {
        public string Caption { get; set; }
        public string Address { get; set; }

        public ICollection<PeriodDefinition> PeriodDefinitions { get; set; }
    }

    public class RetaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.Property(s => s.Caption).IsRequired().HasMaxLength(50);
        }
    }
}
