using EvaLabs.Common.Models.Interfaces;

namespace EvaLabs.Common.Models
{
    public class BaseEntity : IEntity<int>
    {
        public int Id { get; set; }
    }
}