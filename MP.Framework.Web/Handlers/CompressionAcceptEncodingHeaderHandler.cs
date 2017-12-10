using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MP.Framework.Web.Handlers
{
    public sealed class CompressionAcceptEncodingHeaderHandler : DelegatingHandler
    {
        private const string AcceptEncodingHeaderName = "Accept-Encoding";
        private const double OneTenth = 0.1;
        private readonly IDictionary<string, double> _encodingQuality;

        public CompressionAcceptEncodingHeaderHandler(IDictionary<string, double> encodingQuality)
        {
            _encodingQuality = encodingQuality;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpHeaderValueCollection<StringWithQualityHeaderValue> encodings = request.Headers.AcceptEncoding;

            if (encodings != null && encodings.Any())
            {
                string encodingsWithQuality = AcceptEncodingHeaderBuild(encodings, _encodingQuality);

                if (!String.IsNullOrWhiteSpace(encodingsWithQuality))
                {
                    request.Headers.Remove(AcceptEncodingHeaderName);
                    request.Headers.Add(AcceptEncodingHeaderName, encodingsWithQuality);
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }

        private static string AcceptEncodingHeaderBuild(HttpHeaderValueCollection<StringWithQualityHeaderValue> encodings, IDictionary<string, double> encodingsQuality)
        {
            StringBuilder encodingsWithQuality = new StringBuilder();

            foreach (StringWithQualityHeaderValue encoding in encodings)
            {
                if (encoding.Quality.HasValue)
                {
                    encodingsWithQuality.Clear();
                    break;
                }

                encodingsWithQuality.Append(encoding.Value);
                encodingsWithQuality.Append(";q=");
                encodingsWithQuality.Append(
                    (encodingsQuality.ContainsKey(encoding.Value) ? encodingsQuality[encoding.Value] : OneTenth)
                    .ToString("0.0", System.Globalization.CultureInfo.InvariantCulture));
                encodingsWithQuality.Append(",");
            }

            return encodingsWithQuality.ToString();
        }
    }
}
