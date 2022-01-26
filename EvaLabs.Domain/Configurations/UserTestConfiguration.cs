using EvaLabs.Domain.Configurations.Base;
using EvaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Domain.Configurations
{
    public class UserTestConfiguration : ConfigurationBase<UserTest>
    {
        public override void ConfigureEntity(EntityTypeBuilder<UserTest> builder)
        {
            builder.ToTable("UserTests", "dbo");

            builder.Property(x => x.UserId).HasColumnName("UserId").HasColumnType("int").IsRequired();
            builder.Property(x => x.LabId).HasColumnName("LabId").HasColumnType("int").IsRequired();
            builder.Property(x => x.BranchId).HasColumnName("BranchId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.TestId).HasColumnName("TestId").HasColumnType("int").IsRequired();
            builder.Property(x => x.TestPrice).HasColumnName("TestPrice").HasColumnType("decimal(18,6)").HasPrecision(18, 6).IsRequired();
            builder.Property(x => x.TestStatusId).HasColumnName("TestStatusId").HasColumnType("int").IsRequired();
            builder.Property(x => x.TestLocation).HasColumnName("TestLocation").HasColumnType("int").IsRequired();
            builder.Property(x => x.TestDate).HasColumnName("TestDate").HasColumnType("date").IsRequired();
            builder.Property(x => x.ResultDate).HasColumnName("ResultDate").HasColumnType("date").IsRequired();
            builder.Property(x => x.CityId).HasColumnName("CityId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.AreaId).HasColumnName("AreaId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.HomeAddress).HasColumnName("HomeAddress").HasColumnType("nvarchar(256)").IsRequired(false).HasMaxLength(256);


            // Foreign keys
            builder.HasOne(a => a.Area).WithMany(b => b.UserTests).HasForeignKey(c => c.AreaId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserTests_Areas_AreaId");
            builder.HasOne(a => a.Branch).WithMany(b => b.UserTests).HasForeignKey(c => c.BranchId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserTests_Branches_BranchId");
            builder.HasOne(a => a.City).WithMany(b => b.UserTests).HasForeignKey(c => c.CityId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserTests_Cities_CityId");
            builder.HasOne(a => a.Lab).WithMany(b => b.UserTests).HasForeignKey(c => c.LabId).HasConstraintName("FK_UserTests_Labs_LabId");
            builder.HasOne(a => a.Test).WithMany(b => b.UserTests).HasForeignKey(c => c.TestId).HasConstraintName("FK_UserTests_Tests_TestId");
            builder.HasOne(a => a.TestStatus).WithMany(b => b.UserTests).HasForeignKey(c => c.TestStatusId).HasConstraintName("FK_UserTests_TestStatuses_TestStatusId");
            builder.HasOne(a => a.User).WithMany(b => b.UserTests).HasForeignKey(c => c.UserId).HasConstraintName("FK_UserTests_AspNetUsers_UserId");

            builder.HasIndex(x => x.AreaId).HasDatabaseName("IX_UserTests_AreaId");
            builder.HasIndex(x => x.BranchId).HasDatabaseName("IX_UserTests_BranchId");
            builder.HasIndex(x => x.CityId).HasDatabaseName("IX_UserTests_CityId");
            builder.HasIndex(x => x.LabId).HasDatabaseName("IX_UserTests_LabId");
            builder.HasIndex(x => x.TestId).HasDatabaseName("IX_UserTests_TestId");
            builder.HasIndex(x => x.TestStatusId).HasDatabaseName("IX_UserTests_TestStatusId");
            builder.HasIndex(x => x.UserId).HasDatabaseName("IX_UserTests_UserId");
        }
    }
}