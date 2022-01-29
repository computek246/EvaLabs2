using EvaLabs.Domain.Entities;
using EvaLabs.Infrastructure;
using EvaLabs.Services.BaseService;
using EvaLabs.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace EvaLabs.Services.Implementations
{
    public class LabService : BaseService<Lab>, ILabService
    {
        public LabService(IUnitOfWork unitOfWork, ILogger<LabService> logger)
            : base(unitOfWork, logger)
        {
        }
    }
}