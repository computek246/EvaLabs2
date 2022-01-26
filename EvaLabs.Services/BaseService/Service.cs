using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using EvaLabs.Common.Constant;
using EvaLabs.Common.ExtensionMethod;
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
        public readonly IUnitOfWork UnitOfWork;
        public readonly IRepository<TEntity> Repository;


        protected Service(IUnitOfWork unitOfWork, ILogger logger)
        {
            _logger = logger;
            UnitOfWork = unitOfWork;
            Repository = unitOfWork.GetRepository<TEntity>();
        }

        public virtual IQueryable<TEntity> Queryable =>
            Repository.GetAll(e => e.IsActive);

        public virtual IEnumerable AsEnumerable()
        {
            return Queryable.AsEnumerable();
        }

        public virtual async Task<Result<TResult>> TryDoAsync<TResult>(Func<Task<Result<TResult>>> func)
        {
            var cSharpName = func.GetType().CSharpName();

            try
            {
                _logger.LogInformation($"TryDo.. {cSharpName}");

                return await func();
            }
            catch (Exception exception)
            {
                _logger.LogInformation($"TryDo Failed {cSharpName}");
                _logger.LogError($"TryDo Failed {exception.Message}\n{exception.StackTrace}");

                return Result<TResult>.Failed(exception);
            }
            finally
            {
                _logger.LogInformation($"TryDo Successfully {cSharpName}");
            }
        }

        public virtual async Task<Result<TResult>> TryDoAsync<TResult>(int id, Func<TEntity, Task<Result<TResult>>> func)
        {
            return await TryDoAsync(async () =>
            {
                if (id == default)
                    return Result<TResult>.Failed(AppValues.InvalidData);

                var entity = await Queryable.FirstOrDefaultAsync(e => e.Id == id);
                if (entity == null)
                    return Result<TResult>.Failed(string.Format(AppValues.NotFound, typeof(TEntity)));

                return await func(entity);
            });
        }
    }
}