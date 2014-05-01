using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;

namespace Portable
{
    public class Compatibility
    {
        public static HttpClient CreateHttpClient()
        {
            return new HttpClient();
        }

        public static HttpClientHandler CreateHttpClientHandler()
        {
            return new HttpClientHandler();
        }
        
		public static HttpClientHandler CreateHttpClientHandlerWithCompression()
        {
            var handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
            {
				handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip |
												 System.Net.DecompressionMethods.Deflate;
            }
            return handler;
        }

        public static async Task Google(HttpClient client)
        {
            var google = await client.GetStringAsync("http://google.com");

            Debug.WriteLine("PCL: Google: {0}", google.Contains("I'm Feeling Lucky"));
        }

        public static void TestHttpClientHandlerExtensions(HttpClientHandler clientHandler)
        {
            Debug.WriteLine("PCL: System.Net.Http.HttpClientHandlerExtensions.SupportsAllowAutoRedirect: {0}",
                System.Net.Http.HttpClientHandlerExtensions.SupportsAllowAutoRedirect(clientHandler));
        }

        public static async Task TestHttpPrimitives(HttpClientHandler handler)
        {
            var httpClient = new HttpClient(handler);
            var str = await httpClient.GetStringAsync("http://en.wikipedia.org/wiki/Gzip");

            Debug.WriteLine("PCL: Wikipedia: {0}", str.Contains("Wikipedia"));
        }
    }
}
