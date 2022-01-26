using EvaLabs.Domain.Configurations.Base;
using EvaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Domain.Configurations
{
    public class UserConfiguration : ConfigurationBase<User, int?>
    {
        public override void ConfigureEntity(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("AspNetUsers", "dbo");

            builder.Ignore(e => e.FullName);

            builder.Property(x => x.UserPassword).HasColumnName("UserPassword").HasColumnType("nvarchar(50)").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.UserType).HasColumnName("UserType").HasColumnType("nvarchar(50)").IsRequired(false).HasMaxLength(50);

            builder.HasIndex(x => x.NormalizedEmail).HasDatabaseName("IX_EmailIndex");
            builder.HasIndex(x => x.NormalizedUserName).HasDatabaseName("IX_UserNameIndex").IsUnique().HasFilter("[NormalizedUserName] IS NOT NULL");
        }
    }
}
