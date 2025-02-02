﻿using System.Linq;
using EvaLabs.Domain.Entities;
using EvaLabs.Infrastructure;
using EvaLabs.Services.BaseService;
using EvaLabs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EvaLabs.Services.Implementations
{
    public class TestBranchsService : BaseService<TestBranch>, ITestBranchsService
    {
        public TestBranchsService(IUnitOfWork unitOfWork, ILogger<TestBranchsService> logger)
            : base(unitOfWork, logger)
        {
        }


        public override IQueryable<TestBranch> Queryable => base.Queryable
            .Include(t => t.Branch).Include(t => t.Test);
    }
}