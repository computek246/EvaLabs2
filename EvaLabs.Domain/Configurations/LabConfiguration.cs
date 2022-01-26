using EvaLabs.Domain.Configurations.Base;
using EvaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Domain.Configurations
{
    public class LabConfiguration : ConfigurationBase<Lab>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Lab> builder)
        {
            builder.ToTable("Labs", "dbo");

            builder.Property(x => x.LabName).HasColumnName("LabName").HasColumnType("nvarchar(256)").IsRequired(false).HasMaxLength(256);
            builder.Property(x => x.LabLogo).HasColumnName("LabLogo").HasColumnType("nvarchar(256)").IsRequired(false).HasMaxLength(256);
        }
    }
}