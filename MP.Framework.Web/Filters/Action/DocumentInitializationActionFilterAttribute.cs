using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using MP.Models;
using MP.Models.Helpers;

namespace MP.Framework.Web.Filters.Action
{
    public class DocumentInitializationActionFilterAttribute : ActionFilterAttribute, IOrderedFilter
    {
        public DocumentInitializationActionFilterAttribute(int order = 0)
        {
            Order = order;
        }

        public int Order { get; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            object requestObject = actionContext.ActionArguments.FirstOrDefault().Value;

            if (requestObject is IRequestModel)
            {
                IRequestModel requestModel = requestObject as IRequestModel;

                DocumentHelper.Initialize(requestModel, actionContext.RequestContext.Principal);
            }

            base.OnActionExecuting(actionContext);
        }
    }
}
