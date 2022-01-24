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
	public class TestBranchsService : Service<TestBranchs>, ITestBranchsService
	{
		private readonly IMapper _mapper;

		public TestBranchsService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<TestBranchsService> logger)
			: base(unitOfWork, logger)
		{
			_mapper = mapper;
		}

		public async Task<Result<IPagedList<TestBranchsViewModel>>> ListAsync(FilterVm filter, CancellationToken cancellationToken)
		{
			return await TryDoAsync(async () =>
			{
				var data = await Queryable
					.Select(x => _mapper.Map<TestBranchsViewModel>(x))
					.ToPagedListAsync(filter.PageIndex, filter.PageSize, cancellationToken: cancellationToken);

				return Result<IPagedList<TestBranchsViewModel>>.Success(data);
			});
		}

		public async Task<Result<IEnumerable<TestBranchsViewModel>>> ListAllAsync(FilterVm filter, CancellationToken cancellationToken)
		{
			return await TryDoAsync(async () =>
			{
				var data = await Queryable
					.Select(x => _mapper.Map<TestBranchsViewModel>(x))
					.ToListAsync(cancellationToken);

				return Result<IEnumerable<TestBranchsViewModel>>.Success(data);
			});
		}

		public async Task<Result<TestBranchsViewModel>> GetByIdAsync(int id, CancellationToken cancellationToken)
		{
			return await TryDoAsync(id, async source =>
			{
				await Task.CompletedTask;
				var data = _mapper.Map<TestBranchsViewModel>(source);

				return Result<TestBranchsViewModel>.Success(data);
			});
		}

		public async Task<Result<TestBranchsViewModel>> CreateOrUpdateAsync(TestBranchsViewModel model, CancellationToken cancellationToken)
		{
			return await TryDoAsync(async () =>
			{
				if (model == null)
					return Result<TestBranchsViewModel>.Failed(AppValues.InvalidData);
				if (model.HasErrors)
					return Result<TestBranchsViewModel>.Failed(model.Errors);

				var entity = _mapper.Map<TestBranchs>(model);

				await Repository.AddOrUpdateAsync<TestBranchs, Auditable>(entity, (x, y) =>
				{
					x.CreatorId = y.CreatorId;
					x.CreationDate = y.CreationDate;
				}, cancellationToken);

				await UnitOfWork.SaveChangesAsync();
				var data = _mapper.Map<TestBranchsViewModel>(entity);
				return Result<TestBranchsViewModel>.Success(data);
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
