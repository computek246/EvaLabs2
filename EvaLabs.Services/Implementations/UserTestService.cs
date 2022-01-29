using System.Linq;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public UserTestService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<UserTestService> logger)
            : base(unitOfWork, logger)
        {
            _mapper = mapper;
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