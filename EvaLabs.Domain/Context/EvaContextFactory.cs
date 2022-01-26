using Microsoft.EntityFrameworkCore.Design;

namespace EvaLabs.Domain.Context
{
    public class EvaContextFactory : IDesignTimeDbContextFactory<EvaContext>
    {
        public EvaContext CreateDbContext(string[] args)
        {
            return new EvaContext();
        }
    }
}
