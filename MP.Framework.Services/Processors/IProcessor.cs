using MP.Models;

namespace MP.Framework.Services.Processors
{
    public interface IProcessor
    {
        IResponseModel Execute(IRequestModel requestModel);
    }
}
