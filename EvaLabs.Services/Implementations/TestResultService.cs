using System.Linq;
using EvaLabs.Domain.Entities;
using EvaLabs.Infrastructure;
using EvaLabs.Services.BaseService;
using EvaLabs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EvaLabs.Services.Implementations
{
    public class TestResultService : BaseService<TestResult>, ITestResultService
    {
        public TestResultService(IUnitOfWork unitOfWork, ILogger<TestResultService> logger)
            : base(unitOfWork, logger)
        {
        }

        public override IQueryable<TestResult> Queryable => base.Queryable
            .Include(t => t.UserTest);
    }
}
