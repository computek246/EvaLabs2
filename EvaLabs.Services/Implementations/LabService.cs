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
	public class LabService : Service<Lab>, ILabService
	{
		private readonly IMapper _mapper;

		public LabService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<LabService> logger)
			: base(unitOfWork, logger)
		{
			_mapper = mapper;
		}

		public async Task<Result<IPagedList<LabViewModel>>> ListAsync(FilterVm filter, CancellationToken cancellationToken)
		{
			return await TryDoAsync(async () =>
			{
				var data = await Queryable
					.Select(x => _mapper.Map<LabViewModel>(x))
					.ToPagedListAsync(filter.PageIndex, filter.PageSize, cancellationToken: cancellationToken);

				return Result<IPagedList<LabViewModel>>.Success(data);
			});
		}

		public async Task<Result<IEnumerable<LabViewModel>>> ListAllAsync(FilterVm filter, CancellationToken cancellationToken)
		{
			return await TryDoAsync(async () =>
			{
				var data = await Queryable
					.Select(x => _mapper.Map<LabViewModel>(x))
					.ToListAsync(cancellationToken);

				return Result<IEnumerable<LabViewModel>>.Success(data);
			});
		}

		public async Task<Result<LabViewModel>> GetByIdAsync(int id, CancellationToken cancellationToken)
		{
			return await TryDoAsync(id, async source =>
			{
				await Task.CompletedTask;
				var data = _mapper.Map<LabViewModel>(source);

				return Result<LabViewModel>.Success(data);
			});
		}

		public async Task<Result<LabViewModel>> CreateOrUpdateAsync(LabViewModel model, CancellationToken cancellationToken)
		{
			return await TryDoAsync(async () =>
			{
				if (model == null)
					return Result<LabViewModel>.Failed(AppValues.InvalidData);
				if (model.HasErrors)
					return Result<LabViewModel>.Failed(model.Errors);

				var entity = _mapper.Map<Lab>(model);

				await Repository.AddOrUpdateAsync<Lab, Auditable>(entity, (x, y) =>
				{
					x.CreatorId = y.CreatorId;
					x.CreationDate = y.CreationDate;
				}, cancellationToken);

				await UnitOfWork.SaveChangesAsync();
				var data = _mapper.Map<LabViewModel>(entity);
				return Result<LabViewModel>.Success(data);
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
