using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VG.Infra.Data.Entities.Config
{
    class ModelEntityConfig : IEntityTypeConfiguration<ModelEntity>
    {
        public void Configure(EntityTypeBuilder<ModelEntity> builder)
        {
            builder.ToTable("Model");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                    .IsRequired()
                    .HasColumnName("id");

            builder.Property(c => c.Name)
                    .IsRequired()
                    .HasColumnName("name");
        }
    }
}
