using System.IO;
using System.IO.Compression;

namespace MP.Framework.Web.Compression
{
    public sealed class DeflateCompressionService : CompressionService
    {
        private const string DeflateEncoding = "deflate";

        public override string EncodingType
        {
            get { return DeflateEncoding; }
        }

        protected override Stream CreateCompressionStream(Stream output)
        {
            return new DeflateStream(output, CompressionMode.Compress, leaveOpen: true);
        }

        protected override Stream CreateDecompressionStream(Stream input)
        {
            return new DeflateStream(input, CompressionMode.Decompress, leaveOpen: true);
        }
    }
}
