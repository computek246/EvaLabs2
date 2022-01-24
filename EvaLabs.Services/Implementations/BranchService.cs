using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EvaLabs.Common.Constant;
using EvaLabs.Common.Models;
using EvaLabs.Common.ViewModels;
using EvaLabs.Domain.Entities;
using EvaLabs.Infrastructure;
using EvaLabs.Infrastructure.Collections;
using EvaLabs.Services.BaseService;
using EvaLabs.Services.ExtensionMethod;
using EvaLabs.Services.Interfaces;
using EvaLabs.ViewModels;
using EvaLabs.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EvaLabs.Services.Implementations
{
    public class BranchService : Service<Branch>, IBranchService
    {
        private readonly IMapper _mapper;

        public BranchService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<BranchService> logger)
            : base(unitOfWork, logger)
        {
            _mapper = mapper;
        }

        public async Task<Result<IPagedList<BranchViewModel>>> ListAsync(FilterVm filter, CancellationToken cancellationToken)
        {
            return await TryDoAsync(async () =>
            {
                var data = await Queryable.Include(b => b.Area).Include(b => b.Lab)
                    .Select(x => _mapper.Map<BranchViewModel>(x))
                    .ToPagedListAsync(filter.PageIndex, filter.PageSize, cancellationToken: cancellationToken);

                return Result<IPagedList<BranchViewModel>>.Success(data);
            });
        }

        public async Task<Result<IEnumerable<BranchViewModel>>> ListAllAsync(FilterVm filter, CancellationToken cancellationToken)
        {
            return await TryDoAsync(async () =>
            {
                var data = await Queryable.Include(b => b.Area).Include(b => b.Lab)
                    .Select(x => _mapper.Map<BranchViewModel>(x))
                    .ToListAsync(cancellationToken);

                return Result<IEnumerable<BranchViewModel>>.Success(data);
            });
        }

        public async Task<Result<BranchViewModel>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await TryDoAsync(id, async source =>
            {
                await Task.CompletedTask;
                var data = _mapper.Map<BranchViewModel>(source);

                return Result<BranchViewModel>.Success(data);
            });
        }

        public async Task<Result<BranchViewModel>> CreateOrUpdateAsync(BranchViewModel model, CancellationToken cancellationToken)
        {
            return await TryDoAsync(async () =>
            {
                if (model == null)
                    return Result<BranchViewModel>.Failed(AppValues.InvalidData);
                if (model.HasErrors)
                    return Result<BranchViewModel>.Failed(model.Errors);

                var entity = _mapper.Map<Branch>(model);

                await Repository.AddOrUpdateAsync<Branch, Auditable>(entity, (x, y) =>
                {
                    x.CreatorId = y.CreatorId;
                    x.CreationDate = y.CreationDate;
                }, cancellationToken);

                await UnitOfWork.SaveChangesAsync();
                var data = _mapper.Map<BranchViewModel>(entity);
                return Result<BranchViewModel>.Success(data);
            });
        }

        public async Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            return await TryDoAsync(id, async entity =>
            {
                Repository.Delete(entity);
                await UnitOfWork.SaveChangesAsync();

                return Result<bool>.Success(true);
            });
        }

        public async Task<Result<bool>> ToggleEnableProp(int id, CancellationToken cancellationToken)
        {
            return await TryDoAsync(id, async entity =>
            {
                entity.IsActive = !entity.IsActive;
                Repository.Update(entity);
                await UnitOfWork.SaveChangesAsync();

                return Result<bool>.Success(true);
            });
        }
        
    }
}
