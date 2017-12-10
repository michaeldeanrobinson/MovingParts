using System.Net.Http;

namespace MP.Framework.Web.Extensions
{
    internal static class HttpContentExtensions
    {
        internal static bool IsCompressable(this HttpContent content)
        {
            object @object = (content as ObjectContent)?.Value;

            if (@object != null)
            {
                // If we have type that should not be compressed, add them here
                // return @object.GetType() != ExampleType
                return true;
            }

            return false;
        }
    }
}
