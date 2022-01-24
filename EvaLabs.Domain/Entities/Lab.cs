using System.Collections.Generic;
using EvaLabs.Common.Models;

namespace EvaLabs.Domain.Entities
{
    public class Lab : Auditable
    {
        public Lab()
        {
            Branches = new HashSet<Branch>();
            UserTests = new HashSet<UserTest>();
        }

        public string LabName { get; set; }
        public string LabLogo { get; set; }

        public ICollection<Branch> Branches { get; set; }
        public ICollection<UserTest> UserTests { get; set; }
    }
}
