using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace EvaLabs.Common.ViewModels
{
    public partial class Result<TResult>
    {
        public static Result<TResult> Success(TResult data)
        {
            return Success(data, new List<string>());
        }

        public static Result<TResult> Success(TResult data, List<string> messages)
        {
            return new()
            {
                Data = data,
                IsSucceeded = true,
                Messages = messages,
                StatusCode = HttpStatusCode.OK
            };
        }

        public static Result<TResult> Failed(params string[] messages)
        {
            return Failed(messages.ToList());
        }

        public static Result<TResult> Failed(Exception exception)
        {
            var messages = new List<string> {exception.Message};
            if (exception.InnerException != null)
            {
                messages.AddRange(exception.InnerException.Message.Split(Environment.NewLine));
            }

            return Failed(messages);
        }

        public static Result<TResult> Failed(List<string> messages)
        {
            return new()
            {
                Messages = messages,
                IsSucceeded = false,
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        public static Result<TResult> NotFound(params string[] messages)
        {
            return NotFound(messages.ToList());
        }

        public static Result<TResult> NotFound(List<string> messages)
        {
            return new()
            {
                Messages = messages,
                IsSucceeded = false,
                StatusCode = HttpStatusCode.NotFound
            };
        }
    }
}