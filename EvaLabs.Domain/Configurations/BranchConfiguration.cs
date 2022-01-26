using EvaLabs.Domain.Configurations.Base;
using EvaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Domain.Configurations
{
    public class BranchConfiguration : ConfigurationBase<Branch>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("Branches", "dbo");

            builder.Property(x => x.LabId).HasColumnName("LabId").HasColumnType("int").IsRequired();
            builder.Property(x => x.BranchName).HasColumnName("BranchName").HasColumnType("nvarchar(256)").IsRequired(false).HasMaxLength(256);
            builder.Property(x => x.BranchAddress).HasColumnName("BranchAddress").HasColumnType("nvarchar(256)").IsRequired(false).HasMaxLength(256);
            builder.Property(x => x.AreaId).HasColumnName("AreaId").HasColumnType("int").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Area).WithMany(b => b.Branches).HasForeignKey(c => c.AreaId).HasConstraintName("FK_Branches_Areas_AreaId");
            builder.HasOne(a => a.Lab).WithMany(b => b.Branches).HasForeignKey(c => c.LabId).HasConstraintName("FK_Branches_Labs_LabId");

            builder.HasIndex(x => x.AreaId).HasDatabaseName("IX_Branches_AreaId");
            builder.HasIndex(x => x.LabId).HasDatabaseName("IX_Branches_LabId");
        }
    }
}