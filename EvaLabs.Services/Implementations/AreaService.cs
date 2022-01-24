using System.Collections;
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
	public class AreaService : Service<Area>, IAreaService
	{
		private readonly IMapper _mapper;

		public AreaService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<AreaService> logger)
			: base(unitOfWork, logger)
		{
			_mapper = mapper;
		}

		public async Task<Result<IPagedList<AreaViewModel>>> ListAsync(FilterVm filter, CancellationToken cancellationToken)
		{
			return await TryDoAsync(async () =>
			{
				var data = await Queryable
					.Select(x => _mapper.Map<AreaViewModel>(x))
					.ToPagedListAsync(filter.PageIndex, filter.PageSize, cancellationToken: cancellationToken);

				return Result<IPagedList<AreaViewModel>>.Success(data);
			});
		}

		public async Task<Result<IEnumerable<AreaViewModel>>> ListAllAsync(FilterVm filter, CancellationToken cancellationToken)
		{
			return await TryDoAsync(async () =>
			{
				var data = await Queryable
					.Select(x => _mapper.Map<AreaViewModel>(x))
					.ToListAsync(cancellationToken);

				return Result<IEnumerable<AreaViewModel>>.Success(data);
			});
		}

		public async Task<Result<AreaViewModel>> GetByIdAsync(int id, CancellationToken cancellationToken)
		{
			return await TryDoAsync(id, async source =>
			{
				await Task.CompletedTask;
				var data = _mapper.Map<AreaViewModel>(source);

				return Result<AreaViewModel>.Success(data);
			});
		}

		public async Task<Result<AreaViewModel>> CreateOrUpdateAsync(AreaViewModel model, CancellationToken cancellationToken)
		{
			return await TryDoAsync(async () =>
			{
				if (model == null)
					return Result<AreaViewModel>.Failed(AppValues.InvalidData);
				if (model.HasErrors)
					return Result<AreaViewModel>.Failed(model.Errors);

				var entity = _mapper.Map<Area>(model);

				await Repository.AddOrUpdateAsync<Area, Auditable>(entity, (x, y) =>
				{
					x.CreatorId = y.CreatorId;
					x.CreationDate = y.CreationDate;
				}, cancellationToken);

				await UnitOfWork.SaveChangesAsync();
				var data = _mapper.Map<AreaViewModel>(entity);
				return Result<AreaViewModel>.Success(data);
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

        public override IEnumerable AsEnumerable()
        {
            return Queryable.Include(e => e.City).AsEnumerable();
        }
    }
}
