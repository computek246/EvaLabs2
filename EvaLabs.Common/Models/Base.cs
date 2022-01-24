using EvaLabs.Common.Models.Interfaces;

namespace EvaLabs.Common.Models
{
    public abstract class Base : Auditable<int?>, IBase<int>
    {
        public virtual string Name { get; set; }
    }
}