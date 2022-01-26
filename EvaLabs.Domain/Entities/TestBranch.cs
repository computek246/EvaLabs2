using EvaLabs.Common.Models;

namespace EvaLabs.Domain.Entities
{
    public class TestBranch : Auditable
    {
        public int TestId { get; set; }
        public int BranchId { get; set; }

        public Test Test { get; set; }
        public Branch Branch { get; set; }
    }
}
