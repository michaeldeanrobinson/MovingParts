using System.IO;
using System.IO.Compression;
using Brotli;

namespace MP.Framework.Web.Compression
{
    public sealed class BrotliCompressionService : CompressionService
    {
        private const string BrotliEncoding = "br";

        public override string EncodingType
        {
            get { return BrotliEncoding; }
        }

        protected override Stream CreateCompressionStream(Stream output)
        {
            return new BrotliStream(output, CompressionMode.Compress, true);
        }

        protected override Stream CreateDecompressionStream(Stream input)
        {
            return new BrotliStream(input, CompressionMode.Decompress, true);
        }
    }
}
