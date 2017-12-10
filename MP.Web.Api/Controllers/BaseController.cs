using System.Threading.Tasks;
using System.Web.Http;
using MP.Framework.Services;
using MP.Models;
using MP.Models.Rest;

namespace MP.Web.Api.Controllers
{
    /// <summary>
    /// BaseController
    /// </summary>
    public abstract class BaseController : ApiController
    {
        internal async Task<Result<TResponseModel>> ProcessRequest<TRequestModel, TResponseModel>(TRequestModel requestModel)
          where TRequestModel : IRequestModel
          where TResponseModel : IResponseModel
        {
            // validate request
            if (!ModelState.IsValid)
            {
                return ResultHandler.CreateValidationResultError<TRequestModel, TResponseModel>(ModelState.Values, requestModel);
            }

            TResponseModel responseModel = await ServiceContext.ModelMediator.SendAsync<TRequestModel, TResponseModel>(requestModel);

            // validate response
            if (responseModel == null)
            {
                return ResultHandler.CreateResultError<TResponseModel>("Request failed to process.", 400, ErrorLevel.Data, ErrorType.Fatal);
            }

            IRequestModel componentRequestModel = requestModel as IRequestModel;

            return new Result<TResponseModel>
            {
                ProcessTag = componentRequestModel.ProcessTag,
                Value = responseModel,
            };
        }
    }
}