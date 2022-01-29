using EvaLabs.Domain.Entities;
using EvaLabs.Infrastructure;
using EvaLabs.Services.BaseService;
using EvaLabs.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace EvaLabs.Services.Implementations
{
    public class TestService : BaseService<Test>, ITestService
    {
        public TestService(IUnitOfWork unitOfWork, ILogger<TestService> logger)
            : base(unitOfWork, logger)
        {
        }
    }
}