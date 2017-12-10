using System.IO;
using System.Threading.Tasks;

namespace MP.Framework.Web.Compression
{
    public abstract class CompressionService : ICompressionService
    {
        public abstract string EncodingType { get; }

        public Task Compress(Stream source, Stream destination)
        {
            Stream compressed = CreateCompressionStream(destination);

            return Pump(source, compressed).ContinueWith(task => compressed.Dispose());
        }

        public Task Decompress(Stream source, Stream destination)
        {
            Stream decompressed = CreateDecompressionStream(source);

            return Pump(decompressed, destination).ContinueWith(task => decompressed.Dispose());
        }

        protected abstract Stream CreateCompressionStream(Stream output);
        protected abstract Stream CreateDecompressionStream(Stream input);

        private Task Pump(Stream input, Stream output)
        {
            return input.CopyToAsync(output);
        }
    }
}
