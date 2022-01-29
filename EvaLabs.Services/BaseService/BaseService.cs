using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EvaLabs.Common.Constant;
using EvaLabs.Common.Models;
using EvaLabs.Common.ViewModels;
using EvaLabs.Infrastructure;
using EvaLabs.Infrastructure.Collections;
using EvaLabs.Services.ExtensionMethod;
using EvaLabs.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EvaLabs.Services.BaseService
{
    public class BaseService<TEntity> : Service<TEntity>
        where TEntity : Auditable
    {
        public BaseService(IUnitOfWork unitOfWork, ILogger logger)
            : base(unitOfWork, logger)
        {
        }

        public virtual async Task<Result<IPagedList<TEntity>>> ListAsync(FilterVm filter,
            CancellationToken cancellationToken)
        {
            return await TryDoAsync(async () =>
            {
                var data = await Queryable
                    .ToPagedListAsync(filter.PageIndex, filter.PageSize, cancellationToken: cancellationToken);
                return Result<IPagedList<TEntity>>.Success(data);
            });
        }

        public virtual async Task<Result<IEnumerable<TEntity>>> ListAllAsync(FilterVm filter,
            CancellationToken cancellationToken)
        {
            return await TryDoAsync(async () =>
            {
                var data = await Queryable
                    .ToListAsync(cancellationToken);
                return Result<IEnumerable<TEntity>>.Success(data);
            });
        }

        public virtual async Task<Result<TEntity>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await TryDoAsync(id, async source =>
            {
                await Task.CompletedTask;
                return Result<TEntity>.Success(source);
            });
        }

        public virtual async Task<Result<TEntity>> CreateOrUpdateAsync(TEntity model,
            CancellationToken cancellationToken)
        {
            return await TryDoAsync(async () =>
            {
                if (model == null)
                    return Result<TEntity>.Failed(AppValues.InvalidData);

                var entity = await Repository.AddOrUpdateAsync<TEntity, Auditable>(model, (x, y) =>
                {
                    x.CreatorId = y.CreatorId;
                    x.CreationDate = y.CreationDate;
                }, cancellationToken);

                await UnitOfWork.SaveChangesAsync();
                return Result<TEntity>.Success(entity);
            });
        }

        public virtual async Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            return await TryDoAsync(id, async entity =>
            {
                Repository.Delete(entity);
                await UnitOfWork.SaveChangesAsync();

                return Result<bool>.Success(true);
            });
        }


        public virtual async Task<Result<bool>> ToggleEnableProp(int id, CancellationToken cancellationToken)
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