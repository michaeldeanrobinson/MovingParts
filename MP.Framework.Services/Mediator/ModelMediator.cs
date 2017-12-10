using System;
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
            throw new NotImplementedException();
        }
    }
}
