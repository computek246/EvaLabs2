using System.Linq;
using EvaLabs.Domain.Entities;
using EvaLabs.Infrastructure;
using EvaLabs.Services.BaseService;
using EvaLabs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EvaLabs.Services.Implementations
{
    public class AreaService : BaseService<Area>, IAreaService
    {
        public AreaService(IUnitOfWork unitOfWork, ILogger<AreaService> logger)
            : base(unitOfWork, logger)
        {
        }


        public override IQueryable<Area> Queryable => base.Queryable
            .Include(e => e.City);
    }
}