using System.Collections.Generic;

namespace EvaLabs.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public List<string> Messages { get; set; }
    }
}
