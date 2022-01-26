using System.Collections.Generic;
using EvaLabs.Common.Models;

namespace EvaLabs.Domain.Entities
{
    public class Branch : Auditable
    {
        public Branch()
        {
            TestBranches = new HashSet<TestBranch>();
            UserTests = new HashSet<UserTest>();
        }

        public int LabId { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }

        public int AreaId { get; set; }

        public Area Area { get; set; }
        public Lab Lab { get; set; }

        public ICollection<TestBranch> TestBranches { get; set; }
        public ICollection<UserTest> UserTests { get; set; }
    }
}
