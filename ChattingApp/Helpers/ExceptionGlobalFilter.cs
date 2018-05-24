using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace ChattingApp.Helpers
{
    public class ExceptionGlobalFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext == null) throw new ArgumentNullException(nameof(actionExecutedContext));

            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(actionExecutedContext.Exception.Message)
            };
        }
    }
}