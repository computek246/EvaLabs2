using System.Linq;
using EvaLabs.Domain.Entities;
using EvaLabs.Infrastructure;
using EvaLabs.Services.BaseService;
using EvaLabs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EvaLabs.Services.Implementations
{
    public class UserTestService : BaseService<UserTest>, IUserTestService
    {
        public UserTestService(IUnitOfWork unitOfWork, ILogger<UserTestService> logger)
            : base(unitOfWork, logger)
        {
        }

        public override IQueryable<UserTest> Queryable => base.Queryable
            .Include(u => u.Area)
            .Include(u => u.Branch)
            .Include(u => u.City)
            .Include(u => u.Lab)
            .Include(u => u.Test)
            .Include(u => u.TestStatus)
            .Include(u => u.User);
    }
}