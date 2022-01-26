using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace EvaLabs.Domain.Context
{
    public sealed partial class EvaContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // To Set MaxLength for all string Properties
            foreach (var property in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(string)))
            {
                // skip property that have MaxLength
                if (property.GetMaxLength() == null)
                {
                    property.SetMaxLength(256);
                }
            }
        }
    }
}