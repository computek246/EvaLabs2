using EvaLabs.Domain.Configurations.Base;
using EvaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Domain.Configurations
{
    public class TestStatusConfiguration : ConfigurationBase<TestStatus>
    {
        public override void ConfigureEntity(EntityTypeBuilder<TestStatus> builder)
        {
            builder.ToTable("TestStatuses", "dbo");

            builder.Property(x => x.StatusName).HasColumnName("StatusName").HasColumnType("nvarchar(256)").IsRequired(false).HasMaxLength(256);
        }
    }
}
