using EvaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaLabs.Domain.Context
{
    public sealed partial class EvaContext : DbContext, IEvaContext
    {
        public EvaContext()
        {
        }

        public EvaContext(DbContextOptions<EvaContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<User> Users { get; set; } // Users
        public DbSet<Area> Areas { get; set; } // Areas
        public DbSet<Branch> Branches { get; set; } // Branches
        public DbSet<City> Cities { get; set; } // Cities
        public DbSet<Lab> Labs { get; set; } // Labs
        public DbSet<Test> Tests { get; set; } // Tests
        public DbSet<TestBranch> TestBranches { get; set; } // TestBranchs
        public DbSet<TestResult> TestResults { get; set; } // TestResults
        public DbSet<TestStatus> TestStatus { get; set; } // TestStatuses
        public DbSet<UserTest> UserTests { get; set; } // UserTests

    }
}