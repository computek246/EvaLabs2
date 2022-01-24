using EvaLabs.Services.BaseService;
using EvaLabs.ViewModels;
using EvaLabs.ViewModels.Common;

namespace EvaLabs.Services.Interfaces
{
    public interface IAreaService : IService<int, AreaViewModel, FilterVm>
    {
    }
}
