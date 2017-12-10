using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MP.Framework.Web.Compression;
using MP.Framework.Web.Extensions;

namespace MP.Framework.Web.Handlers
{
    public sealed class ResponseCompressionHandler : DelegatingHandler
    {
        private static readonly Collection<ICompressionService> _compressionServices;

        static ResponseCompressionHandler()
        {
            _compressionServices = new Collection<ICompressionService>
            {
                new GZipCompressionService(),
                new DeflateCompressionService(),
                new BrotliCompressionService(),
            };
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            if (ShouldBeCompressed(response))
            {
                string encoding = request.Headers.AcceptEncoding != null && request.Headers.AcceptEncoding.Any()
                    ? request.Headers.AcceptEncoding.OrderByDescending(q => q.Quality ?? .1).First().Value
                    : null;

                if (!String.IsNullOrWhiteSpace(encoding))
                {
                    ICompressionService compressionService =
                        _compressionServices.FirstOrDefault(
                            c => c.EncodingType.Equals(encoding, StringComparison.InvariantCultureIgnoreCase));

                    if (compressionService != null && !IsAlreadyCompressed(response.Content, compressionService.EncodingType))
                    {
                        response.Content = new CompressedContent(response.Content, compressionService);
                    }
                }
            }

            return response;
        }

        private static bool IsAlreadyCompressed(HttpContent responseContent, string encodingType)
        {
            return responseContent?.Headers.ContentEncoding != null
                   && responseContent.Headers.ContentEncoding.Contains(encodingType, StringComparer.OrdinalIgnoreCase);
        }

        private static bool ShouldBeCompressed(HttpResponseMessage response)
        {
            return response?.Content != null
                && response.IsSuccessStatusCode
                && response.Content.IsCompressable();
        }
    }
}
