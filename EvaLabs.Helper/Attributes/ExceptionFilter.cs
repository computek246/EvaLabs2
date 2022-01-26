﻿using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace EvaLabs.Helper.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ExceptionFilter : Attribute, IExceptionFilter, IActionFilter
    {

        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception.ToString().Replace(Environment.NewLine, "\\n"));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation(new string('-', 196));
            _logger.LogInformation($"OnActionExecuting :{context.ActionDescriptor.DisplayName}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"OnActionExecuted :{context.ActionDescriptor.DisplayName}");
        }
    }
}
