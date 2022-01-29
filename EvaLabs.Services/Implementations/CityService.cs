using EvaLabs.Domain.Entities;
using EvaLabs.Infrastructure;
using EvaLabs.Services.BaseService;
using EvaLabs.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace EvaLabs.Services.Implementations
{
    public class CityService : BaseService<City>, ICityService
    {
        public CityService(IUnitOfWork unitOfWork, ILogger<CityService> logger)
            : base(unitOfWork, logger)
        {
        }
    }
}