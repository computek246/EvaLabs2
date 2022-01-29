using System.Linq;
using EvaLabs.Domain.Entities;
using EvaLabs.Infrastructure;
using EvaLabs.Services.BaseService;
using EvaLabs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EvaLabs.Services.Implementations
{
    public class BranchService : BaseService<Branch>, IBranchService
    {
        public BranchService(IUnitOfWork unitOfWork, ILogger<BranchService> logger)
            : base(unitOfWork, logger)
        {
        }


        public override IQueryable<Branch> Queryable => base.Queryable
            .Include(b => b.Area).Include(b => b.Lab);
    }
}