using System.Collections.Generic;
using EvaLabs.Domain.Configurations.Base;
using EvaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Domain.Configurations
{
    public class CityConfiguration : ConfigurationBase<City>
    {
        public override void ConfigureEntity(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities", "dbo");

            builder.Property(x => x.CityName).HasColumnName("CityName").HasColumnType("nvarchar(256)").IsRequired(false).HasMaxLength(256);
        }
    }
}