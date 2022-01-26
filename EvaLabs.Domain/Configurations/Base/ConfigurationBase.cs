using System;
using EvaLabs.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Domain.Configurations.Base
{
    public abstract class ConfigurationBase<TEntity>
        : ConfigurationBase<TEntity, int>
        where TEntity : Auditable<int>
    {
    }


    public abstract class ConfigurationBase<TEntity, TForeignKey>
        : IEntityTypeConfiguration<TEntity>
        where TEntity : Auditable<TForeignKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            ConfigureEntity(builder);

            builder.HasKey(x => x.Id).IsClustered();
            builder.Property(x => x.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();

            builder.Property(x => x.IsActive).HasColumnName("IsActive");
            builder.Property(x => x.CreatorId).HasColumnName("CreatorId");
            builder.Property(x => x.CreationDate).HasColumnName("CreationDate").HasColumnType("datetime");
            builder.Property(x => x.ModifierId).HasColumnName("ModifierId");
            builder.Property(x => x.LastModifiedDate).HasColumnName("LastModifiedDate").HasColumnType("datetime");
            builder.Property(x => x.IsDeleted).HasColumnName("IsDeleted");



            //builder.Property(x => x.IsActive).HasDefaultValue(true);
            //builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.CreatorId).HasDefaultValue(120);
            builder.Property(x => x.ModifierId).HasDefaultValue(120);
            builder.Property(x => x.CreationDate).HasDefaultValueSql("getdate()");
            builder.Property(x => x.LastModifiedDate).HasDefaultValueSql("getdate()");

            builder.HasQueryFilter(x => x.IsDeleted == false);
        }

        public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
    }
}