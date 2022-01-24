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
    public class CityService : Service<City>, ICityService
    {
        private readonly IMapper _mapper;

        public CityService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<CityService> logger)
            : base(unitOfWork, logger)
        {
            _mapper = mapper;
        }

        public async Task<Result<IPagedList<CityViewModel>>> ListAsync(FilterVm filter, CancellationToken cancellationToken)
        {
            return await TryDoAsync(async () =>
            {
                var data = await Queryable
                    .Select(x => _mapper.Map<CityViewModel>(x))
                    .ToPagedListAsync(filter.PageIndex, filter.PageSize, cancellationToken: cancellationToken);

                return Result<IPagedList<CityViewModel>>.Success(data);
            });
        }

        public async Task<Result<IEnumerable<CityViewModel>>> ListAllAsync(FilterVm filter, CancellationToken cancellationToken)
        {
            return await TryDoAsync(async () =>
            {
                var data = await Queryable
                    .Select(x => _mapper.Map<CityViewModel>(x))
                    .ToListAsync(cancellationToken);

                return Result<IEnumerable<CityViewModel>>.Success(data);
            });
        }

        public async Task<Result<CityViewModel>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await TryDoAsync(id, async source =>
            {
                await Task.CompletedTask;
                var cityVm = _mapper.Map<CityViewModel>(source);

                return Result<CityViewModel>.Success(cityVm);
            });
        }

        public async Task<Result<CityViewModel>> CreateOrUpdateAsync(CityViewModel model, CancellationToken cancellationToken)
        {
            return await TryDoAsync(async () =>
            {
                if (model == null)
                    return Result<CityViewModel>.Failed(AppValues.InvalidData);
                if (model.HasErrors)
                    return Result<CityViewModel>.Failed(model.Errors);

                var entity = _mapper.Map<City>(model);

                await Repository.AddOrUpdateAsync<City, Auditable>(entity, (x, y) =>
                {
                    x.CreatorId = y.CreatorId;
                    x.CreationDate = y.CreationDate;
                }, cancellationToken);

                await UnitOfWork.SaveChangesAsync();
                var data = _mapper.Map<CityViewModel>(entity);
                return Result<CityViewModel>.Success(data);
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