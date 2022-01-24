using EvaLabs.Common.Models.Interfaces;

namespace EvaLabs.ViewModels.Common.Helper
{
    public class BaseEntityVm : IEntity<int>
    {
        public int Id { get; set; }
    }
}