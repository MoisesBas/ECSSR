using System;
using System.Collections.Generic;
using System.Text;
using ECSSR.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECSSR.DOMAIN.Configuration
{
    public class ProductImageConfigurations : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.Property(p => p.Title)
                    .HasColumnType("varchar(90)");
            builder.Property(p => p.ProductId);
            builder.Property(p => p.ImageData).HasColumnType("varbinary(max)");
            builder.Property(p => p.Created);
            builder.Property(p => p.CreatedBy).HasColumnType("varchar(18)");
            builder.Property(p => p.Updated).IsRequired(false);
            builder.Property(p => p.UpdatedBy).HasColumnType("varchar(18)");
            builder.ToTable("tblProductImage", "dbo");

            builder.HasOne(t => t.Product)
                .WithMany(t => t.Images)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Product_ImageProducts_ProductId");
        }
    }
}
