using CryptoApp.Models;
using CryptoApp.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace CryptoApp.Filters
{
    public class ExceptionLoggingAttribute : ExceptionFilterAttribute
    {
        public readonly ExceptionLogService _exceptionLogService;
        public ExceptionLoggingAttribute()
        {
          
        }
        public override void OnException(ExceptionContext context)
        {
            var errorLog = new CreateExceptionLogCommand
            {
                LogTime = DateTime.Now,
                HttpStatusCode = context.Exception.HResult,
                
                ExceptionMessage = context.Exception.Message,
                ExceptionStackTrace = context.Exception.StackTrace
            };

            _exceptionLogService.CreateExceptionRecord(errorLog);
            //need to execute the service somehow

            context.ExceptionHandled = true;
        }
    }
}
