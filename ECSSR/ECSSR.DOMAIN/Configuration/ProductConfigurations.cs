using System;
using System.Collections.Generic;
using System.Text;
using ECSSR.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECSSR.DOMAIN.Configuration
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                    .HasColumnType("varchar(90)");
            builder.Property(p => p.Color)
                   .HasColumnType("varchar(90)");
            builder.Property(p => p.Created);
            builder.Property(p => p.CreatedBy).HasColumnType("varchar(18)");
            builder.Property(p => p.Updated).IsRequired(false);
            builder.Property(p => p.UpdatedBy).HasColumnType("varchar(18)");
            builder.ToTable("tblProduct", "dbo");
        }
    }
}
