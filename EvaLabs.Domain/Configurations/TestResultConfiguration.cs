using EvaLabs.Domain.Configurations.Base;
using EvaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Domain.Configurations
{


    public class TestResultConfiguration : ConfigurationBase<TestResult>
    {
        public override void ConfigureEntity(EntityTypeBuilder<TestResult> builder)
        {
            builder.ToTable("TestResults", "dbo");

            builder.Property(x => x.UserTestId).HasColumnName("UserTestId").HasColumnType("int").IsRequired();
            builder.Property(x => x.Result).HasColumnName("Result").HasColumnType("nvarchar(256)").IsRequired(false).HasMaxLength(256);

            // Foreign keys
            builder.HasOne(a => a.UserTest).WithOne(b => b.TestResult).HasForeignKey<TestResult>(c => c.UserTestId).HasConstraintName("FK_TestResult_UserTest_UserTestId");

            builder.HasIndex(x => x.UserTestId).HasDatabaseName("IX_TestResult_UserTestId").IsUnique();
        }
    }
}
