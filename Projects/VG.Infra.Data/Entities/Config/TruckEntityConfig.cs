using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VG.Infra.Data.Entities.Config
{
    class TruckEntityConfig : IEntityTypeConfiguration<TruckEntity>
    {
        public void Configure(EntityTypeBuilder<TruckEntity> builder)
        {
            builder.ToTable("Truck");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                    .IsRequired()
                    .HasColumnName("id");

            builder.Property(c => c.ManufactureYear)
                .IsRequired()
                .HasColumnName("manufacture_year");

            builder.Property(c => c.ModelYear)
                .IsRequired()
                .HasColumnName("manufacture_model");

            builder.Property(c => c.Color)
                .HasMaxLength(15)
                .HasColumnName("color");

            builder.Property(c => c.Price)
                .HasColumnName("price")
                .HasColumnType("DECIMAL(12,2)");

            builder.Property(c => c.ModelId)
               .HasColumnName("model_id");

            builder.HasOne(c => c.Model)
                    .WithMany(c => c.Trucks)
                    .HasForeignKey(c => c.ModelId);

        }
    }
}
