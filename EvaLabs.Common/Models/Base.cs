using EvaLabs.Common.Models.Interfaces;

namespace EvaLabs.Common.Models
{
    public class Base : Auditable<int?>, IBase<int>
    {
        public string Name { get; set; }
    }
}