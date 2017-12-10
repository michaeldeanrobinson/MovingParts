using System.Threading.Tasks;
using MP.Models;

namespace MP.Framework.Services.Mediator
{
    public class ModelMediator : IModelMediator
    {
        public Task<TResponseModel> SendAsync<TRequestModel, TResponseModel>(TRequestModel requestModel)
            where TRequestModel : IRequestModel
            where TResponseModel : IResponseModel
        {
            // TODO: Add security calls here to verify if the creds in the AuthToken should be allowed to make this call

            Task<TResponseModel> task = Task.Factory.StartNew(() => (TResponseModel)ServiceContext.MessageHandlerRepository[requestModel.ModelType].Handle(requestModel));

            return task;
        }
    }
}
