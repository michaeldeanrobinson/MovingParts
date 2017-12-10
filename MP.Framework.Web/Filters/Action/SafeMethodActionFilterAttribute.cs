using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using MP.Framework.Web.Attributes;
using MP.Models.Rest;

namespace MP.Framework.Web.Filters.Action
{
    public class SafeMethodActionFilterAttribute : ActionFilterAttribute, IOrderedFilter
    {
        public SafeMethodActionFilterAttribute(int order = 0)
        {
            Order = order;
        }

        public int Order { get; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ActionDescriptor.GetCustomAttributes<IgnoreActionFiltersAttribute>().Any())
            {
                // The controller action is decorated with the [IgnoreActionFiltersAttribute]
                // custom attribute => don't do anything.
                return;
            }

            actionContext.ControllerContext.RouteData.Values.Add("StartTime", DateTime.Now);
            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.ActionContext.ActionDescriptor.GetCustomAttributes<IgnoreActionFiltersAttribute>().Any())
            {
                // The controller action is decorated with the [IgnoreActionFiltersAttribute]
                // custom attribute => don't do anything.
                return;
            }

            DateTime start = (DateTime)actionExecutedContext.ActionContext.ControllerContext.RouteData.Values["StartTime"];

            if (actionExecutedContext.Exception != null)
            {
                Factory.LogManager.Logger.Error("Action failed: ", actionExecutedContext.Exception);

                Result result = ResultHandler.CreateResultError(actionExecutedContext.Exception.GetBaseException(), 500, ErrorLevel.Service, ErrorType.Error);

                result.ExecutionTime = DateTime.Now.Subtract(start);
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new ValueResultConverter<Result>().Convert(actionExecutedContext.ActionContext.ControllerContext, result).Content
                };
                actionExecutedContext.Exception = null;
                base.OnActionExecuted(actionExecutedContext);
                return;
            }
            else if (actionExecutedContext.Response == null)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NoContent);
                base.OnActionExecuted(actionExecutedContext);
                return;
            }

            if (typeof(Result).IsAssignableFrom(actionExecutedContext.ActionContext.ActionDescriptor.ReturnType))
            {
                if (actionExecutedContext.Response.Content is ObjectContent content && content.Value is Result result)
                {
                    result.ExecutionTime = DateTime.Now.Subtract(start);

                    if (result.Error?.Code.Number > 0)
                    {
                        actionExecutedContext.Response.StatusCode = (HttpStatusCode)result.Error.Code.Number;
                    }
                }
            }
            else if (typeof(HttpResponseMessage).IsAssignableFrom(actionExecutedContext.ActionContext.ActionDescriptor.ReturnType))
            {
                // do nothing to this type of response
            }
            else
            {
                Result<Object> result = new Result<Object>();

                ObjectContent content = actionExecutedContext.Response.Content as ObjectContent;
                result.Value = content?.Value ?? null;
                result.ExecutionTime = DateTime.Now.Subtract(start);
                actionExecutedContext.Response.StatusCode = HttpStatusCode.OK;
                ((ObjectContent)actionExecutedContext.Response.Content).Value = result;
            }

            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
