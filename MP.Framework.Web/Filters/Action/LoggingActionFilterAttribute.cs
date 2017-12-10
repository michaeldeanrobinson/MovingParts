using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using MP.Framework.Web.Attributes;

namespace MP.Framework.Web.Filters.Action
{
    public class LoggingActionFilterAttribute : ActionFilterAttribute, IOrderedFilter
    {
        public LoggingActionFilterAttribute(int order = 0)
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

            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
