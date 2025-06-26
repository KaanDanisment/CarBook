using CarBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Persistence.Configurations
{
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.HasOne(r => r.PickupLocation)
                .WithMany()
                .HasForeignKey(r => r.PickupLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.DropoffLocation)
                .WithMany()
                .HasForeignKey(r => r.DropoffLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(r => r.RentTime)
                .HasConversion(
                    v => v.HasValue ? v.Value.ToTimeSpan() : (TimeSpan?)null,
                    v => v.HasValue ? TimeOnly.FromTimeSpan(v.Value) : (TimeOnly?)null
                );

            builder.Property(r => r.ReturnTime)
                .HasConversion(
                    v => v.HasValue ? v.Value.ToTimeSpan() : (TimeSpan?)null,
                    v => v.HasValue ? TimeOnly.FromTimeSpan(v.Value) : (TimeOnly?)null
                );
        }
    }
}
