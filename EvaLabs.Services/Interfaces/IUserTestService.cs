using EvaLabs.Services.BaseService;
using EvaLabs.ViewModels;
using EvaLabs.ViewModels.Common;

namespace EvaLabs.Services.Interfaces
{
    public interface IUserTestService : IService<int, UserTestViewModel, FilterVm>
    {
    }
}
