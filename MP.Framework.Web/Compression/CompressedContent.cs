using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MP.Framework.Web.Compression
{
    public sealed class CompressedContent : HttpContent
    {
        private readonly HttpContent _content;
        private readonly ICompressionService _compressionService;

        public CompressedContent(HttpContent content, ICompressionService compressionService)
        {
            _content = content;
            _compressionService = compressionService;

            AddHeaders();
        }

        protected override bool TryComputeLength(out long length)
        {
            length = -1;

            return false;
        }

        protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            using (_content)
            {
                Stream contentStream = await _content.ReadAsStreamAsync();

                await _compressionService.Compress(contentStream, stream);
            }
        }

        private void AddHeaders()
        {
            foreach (KeyValuePair<string, IEnumerable<string>> header in _content.Headers)
            {
                Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            Headers.ContentEncoding.Add(_compressionService.EncodingType);
        }
    }
}
