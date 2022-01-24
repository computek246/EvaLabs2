using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EvaLabs.Common.Models.Interfaces;
using EvaLabs.Common.ViewModels;
using EvaLabs.Infrastructure.Collections;

namespace EvaLabs.Services.BaseService
{
    public interface IService
    {
        IEnumerable AsEnumerable();
        Task<Result<TResult>> TryDoAsync<TResult>(Func<Task<Result<TResult>>> func);
    }


    public interface IService<in TKey, TModel, in TFilter> : IService
        where TKey : IEquatable<TKey>
        where TModel : IEntity<TKey>
        where TFilter : class
    {
        public Task<Result<IPagedList<TModel>>> ListAsync(TFilter filter, CancellationToken cancellationToken);
        public Task<Result<IEnumerable<TModel>>> ListAllAsync(TFilter filter, CancellationToken cancellationToken);
        public Task<Result<TModel>> GetByIdAsync(TKey id, CancellationToken cancellationToken);
        public Task<Result<TModel>> CreateOrUpdateAsync(TModel model, CancellationToken cancellationToken);
        public Task<Result<bool>> DeleteAsync(TKey id, CancellationToken cancellationToken);
        public Task<Result<bool>> ToggleEnableProp(TKey id, CancellationToken cancellationToken);
    }
}