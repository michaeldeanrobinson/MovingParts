using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MP.Framework.Web.Handlers
{
    public class GZipDecompressionHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Content.Headers.ContentType == null ||
                request.Content.Headers.ContentType.MediaType != "application/gzip")
            {
                return await base.SendAsync(request, cancellationToken);
            }

            Stream outputStream = new MemoryStream();
            request.Content.ReadAsStreamAsync().ContinueWith(t =>
            {
                Stream inputStream = t.Result;
                var gzipStream = new GZipStream(inputStream, CompressionMode.Decompress);

                gzipStream.CopyTo(outputStream);
                gzipStream.Dispose();

                outputStream.Seek(0, SeekOrigin.Begin);
            }).Wait();

            HttpContentHeaders originalHeaders = request.Content.Headers;
            originalHeaders.Remove("Content-Type");

            request.Content = new StreamContent(outputStream);

            foreach (var header in originalHeaders)
            {
                request.Content.Headers.Add(header.Key, header.Value);
            }

            request.Content.Headers.Add("Content-Type", "application/json");

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
