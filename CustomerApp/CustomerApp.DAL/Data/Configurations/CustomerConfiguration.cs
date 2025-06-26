using CustomerApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerApp.DAL.Data.Configurations;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(e => e.Customerid).HasName("customer_pkey");

        builder.ToTable("customer");

        builder.Property(e => e.Customerid).HasColumnName("customerid");
        builder.Property(e => e.Createdat)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("createdat");
        builder.Property(e => e.Createdby).HasColumnName("createdby");
        builder.Property(e => e.Customeraddress)
            .HasMaxLength(1000)
            .HasDefaultValueSql("''::character varying")
            .HasColumnName("customeraddress");
        builder.Property(e => e.Customercode)
            .HasMaxLength(50)
            .HasColumnName("customercode");
        builder.Property(e => e.Customername)
            .HasMaxLength(255)
            .HasColumnName("customername");
        builder.Property(e => e.Modifiedat)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("modifiedat");
        builder.Property(e => e.Modifiedby).HasColumnName("modifiedby");
    }
}