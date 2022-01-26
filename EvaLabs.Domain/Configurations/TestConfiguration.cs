using EvaLabs.Domain.Configurations.Base;
using EvaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Domain.Configurations
{
    public class TestConfiguration : ConfigurationBase<Test>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Test> builder)
        {
            builder.ToTable("Tests", "dbo");

            builder.Property(x => x.TestName).HasColumnName("TestName").HasColumnType("nvarchar(256)").IsRequired(false).HasMaxLength(256);
            builder.Property(x => x.TestDetails).HasColumnName("TestDetails").HasColumnType("nvarchar(256)").IsRequired(false).HasMaxLength(256);
            builder.Property(x => x.Price).HasColumnName("Price").HasColumnType("decimal(18,6)").HasPrecision(18, 6).IsRequired();
            builder.Property(x => x.AtHome).HasColumnName("AtHome").HasColumnType("bit").IsRequired();
        }
    }
}