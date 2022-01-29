using EvaLabs.Domain.Entities;
using EvaLabs.Services.BaseService;
using EvaLabs.ViewModels.Common;

namespace EvaLabs.Services.Interfaces
{
    public interface ITestService : IService<int, Test, FilterVm>
    {
    }
}
