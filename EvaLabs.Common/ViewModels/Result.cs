using System.Collections.Generic;

namespace EvaLabs.Common.ViewModels
{

    public partial class Result<TResult>
    {
        public TResult Data { get; set; }
        public bool IsSucceeded { get; set; }
        public List<string> Messages { get; set; }
    }
}
