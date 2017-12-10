using System.IO;
using System.IO.Compression;

namespace MP.Framework.Web.Compression
{
    public sealed class GZipCompressionService : CompressionService
    {
        internal const string GZipEncoding = "gzip";

        public override string EncodingType
        {
            get { return GZipEncoding; }
        }

        protected override Stream CreateCompressionStream(Stream output)
        {
            return new GZipStream(output, CompressionMode.Compress, leaveOpen: true);
        }

        protected override Stream CreateDecompressionStream(Stream input)
        {
            return new GZipStream(input, CompressionMode.Decompress, leaveOpen: true);
        }
    }
}
