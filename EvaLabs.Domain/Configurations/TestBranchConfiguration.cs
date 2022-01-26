using EvaLabs.Domain.Configurations.Base;
using EvaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Domain.Configurations
{
    public class TestBranchConfiguration : ConfigurationBase<TestBranch>
    {
        public override void ConfigureEntity(EntityTypeBuilder<TestBranch> builder)
        {
            builder.ToTable("TestBranchs", "dbo");

            builder.Property(x => x.TestId).HasColumnName("TestId").HasColumnType("int").IsRequired();
            builder.Property(x => x.BranchId).HasColumnName("BranchId").HasColumnType("int").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Branch).WithMany(b => b.TestBranches).HasForeignKey(c => c.BranchId).HasConstraintName("FK_TestBranchs_Branches_BranchId");
            builder.HasOne(a => a.Test).WithMany(b => b.TestBranches).HasForeignKey(c => c.TestId).HasConstraintName("FK_TestBranchs_Tests_TestId");

            builder.HasIndex(x => x.BranchId).HasDatabaseName("IX_TestBranchs_BranchId");
            builder.HasIndex(x => x.TestId).HasDatabaseName("IX_TestBranchs_TestId");
        }
    }
}
