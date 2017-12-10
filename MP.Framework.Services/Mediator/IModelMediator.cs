using System.Threading.Tasks;
using MP.Models;

namespace MP.Framework.Services.Mediator
{
    public interface IModelMediator
    {
        Task<TResponseModel> SendAsync<TRequestModel, TResponseModel>(TRequestModel requestModel)
            where TRequestModel : IRequestModel
            where TResponseModel : IResponseModel;
    }
}
