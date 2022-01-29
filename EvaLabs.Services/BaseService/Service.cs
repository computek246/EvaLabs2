using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using EvaLabs.Common.Constant;
using EvaLabs.Common.Models.Interfaces;
using EvaLabs.Common.ViewModels;
using EvaLabs.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EvaLabs.Services.BaseService
{
    public abstract class Service<TEntity> : IService
        where TEntity : class, IEntity<int>, IActiveable
    {
        private readonly ILogger _logger;
        public readonly IRepository<TEntity> Repository;
        public readonly IUnitOfWork UnitOfWork;


        protected Service(IUnitOfWork unitOfWork, ILogger logger)
        {
            _logger = logger;
            UnitOfWork = unitOfWork;
            Repository = unitOfWork.GetRepository<TEntity>();
        }

        public virtual IQueryable<TEntity> Queryable =>
            Repository.GetAll();

        public virtual IEnumerable AsEnumerable()
        {
            return Queryable.Where(e => e.IsActive).AsEnumerable();
        }

        public virtual async Task<Result<TResult>> TryDoAsync<TResult>(Func<Task<Result<TResult>>> func)
        {
            var guid = func.GetType().GUID;

            try
            {
                _logger.LogInformation($"TryDo.. {guid}");

                return await func();
            }
            catch (Exception exception)
            {
                _logger.LogInformation($"TryDo Failed {guid}");

                _logger.LogError($"TryDo Failed {exception.Message}\n{exception.StackTrace}");

                return Result<TResult>.Failed(exception);
            }
            finally
            {
                _logger.LogInformation($"TryDo Successfully {guid}");
            }
        }

        public virtual async Task<Result<TResult>> TryDoAsync<TResult>(int id,
            Func<TEntity, Task<Result<TResult>>> func)
        {
            return await TryDoAsync(async () =>
            {
                if (id == default)
                    return Result<TResult>.Failed(AppValues.InvalidData);

                var entity = await Queryable.FirstOrDefaultAsync(e => e.Id == id);
                if (entity == null)
                    return Result<TResult>.NotFound(string.Format(AppValues.NotFound, typeof(TEntity)));

                return await func(entity);
            });
        }
    }
}