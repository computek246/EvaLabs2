using EvaLabs.Common.Models;

namespace EvaLabs.Domain.Entities
{
    public class TestResult : Auditable
    {
        public int UserTestId { get; set; }
        public string Result { get; set; }

        public UserTest UserTest { get; set; }
    }
}
