using AutoMapper;
using EvaLabs.Domain.Entities;
using EvaLabs.Infrastructure;
using EvaLabs.Services.BaseService;
using EvaLabs.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace EvaLabs.Services.Implementations
{
    public class TestService : BaseService<Test>, ITestService
    {
        private readonly IMapper _mapper;

        public TestService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<TestService> logger)
            : base(unitOfWork, logger)
        {
            _mapper = mapper;
        }
    }
}