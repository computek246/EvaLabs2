using EvaLabs.Domain.Configurations.Base;
using EvaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Domain.Configurations
{
    public class AreaConfiguration : ConfigurationBase<Area>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Area> builder)
        {
            builder.ToTable("Areas", "dbo");

            builder.Property(x => x.CityId).HasColumnName("CityId").HasColumnType("int").IsRequired();
            builder.Property(x => x.AreaName).HasColumnName("AreaName").HasColumnType("nvarchar(256)").IsRequired(false).HasMaxLength(256);

            // Foreign keys
            builder.HasOne(a => a.City).WithMany(b => b.Areas).HasForeignKey(c => c.CityId).HasConstraintName("FK_Areas_Cities_CityId");

            builder.HasIndex(x => x.CityId).HasDatabaseName("IX_Areas_CityId");
        }
    }
}