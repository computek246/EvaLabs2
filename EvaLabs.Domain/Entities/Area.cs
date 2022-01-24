using System.Collections.Generic;
using EvaLabs.Common.Models;

namespace EvaLabs.Domain.Entities
{
    public class Area : Auditable
    {
        public Area()
        {
            Branches = new HashSet<Branch>();
            UserTests = new HashSet<UserTest>();
        }

        public int CityId { get; set; }
        public string AreaName { get; set; }

        public City City { get; set; }
        public ICollection<Branch> Branches { get; set; }
        public ICollection<UserTest> UserTests { get; set; }
    }
}
