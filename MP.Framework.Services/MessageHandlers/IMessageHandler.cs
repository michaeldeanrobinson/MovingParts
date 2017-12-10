using MP.Models;

namespace MP.Framework.Services.MessageHandlers
{
    public interface IMessageHandler
    {
        IResponseModel Handle(IRequestModel requestModel);
    }
}
