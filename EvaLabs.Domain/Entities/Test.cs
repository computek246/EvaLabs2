using System.Collections.Generic;
using EvaLabs.Common.Models;

namespace EvaLabs.Domain.Entities
{
    public class Test : Auditable
    {
        public Test()
        {
            TestBranches = new HashSet<TestBranch>();
            UserTests = new HashSet<UserTest>();
        }

        public string TestName { get; set; }
        public string TestDetails { get; set; }
        public decimal Price { get; set; }
        public bool AtHome { get; set; }

        public ICollection<TestBranch> TestBranches { get; set; }
        public ICollection<UserTest> UserTests { get; set; }
    }
}
