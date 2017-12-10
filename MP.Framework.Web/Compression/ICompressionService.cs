using System.IO;
using System.Threading.Tasks;

namespace MP.Framework.Web.Compression
{
    public interface ICompressionService
    {
        string EncodingType { get; }

        Task Compress(Stream source, Stream destination);
        Task Decompress(Stream source, Stream destination);
    }
}
