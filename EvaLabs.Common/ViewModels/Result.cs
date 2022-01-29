using System.Collections.Generic;
using System.Net;

namespace EvaLabs.Common.ViewModels
{

    public partial class Result<TResult>
    {
        public Result()
        {
            Messages = new List<string>();
        }

        public TResult Data { get; set; }
        public bool IsSucceeded { get; set; }
        public List<string> Messages { get; set; }
        public HttpStatusCode StatusCode { get; set; }

    }
}
