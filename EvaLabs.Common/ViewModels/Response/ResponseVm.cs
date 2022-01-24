using System.Collections.Generic;
using System.Net;

namespace EvaLabs.Common.ViewModels.Response
{
    public class ResponseVm
    {
        public string ExecutionTime { get; set; }
    }

    public partial class ResponseVm<TResult> : ResponseVm
    {
        public HttpStatusCode StatusCode { get; set; }
        public IList<string> Messages { get; set; }
        public TResult Data { get; set; }
    }
}
