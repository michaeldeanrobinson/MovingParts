using MP.Framework.Logger;

namespace MP.Framework.Logging
{
    public interface ILogManager
    {
        ILogger Logger { get; }
        ILogger ErrorLogger { get; }
        ILogger DatabaseLogger { get; }
    }
}
