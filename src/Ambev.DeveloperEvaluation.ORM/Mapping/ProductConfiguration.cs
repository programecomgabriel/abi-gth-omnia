using Ambev.DeveloperEvaluation.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(p => p.Title).IsRequired();
        builder.Property(p => p.Description).IsRequired();
        builder.Property(p => p.Category).IsRequired();
        builder.Property(p => p.Image).IsRequired();

        builder.OwnsOne(p => p.Rating, n =>
        {
            n.Property(pr => pr.Rate).HasColumnName("Rate").IsRequired().HasPrecision(10, 2);
            n.Property(pr => pr.Count).HasColumnName("RateCount").IsRequired();
        });
    }
}
