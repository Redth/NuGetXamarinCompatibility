using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;

namespace TestiOS
{
    class MainViewController : UIViewController
    {
        public MainViewController() : base()
        {
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            await GoogleFromPCL();
            await GoogleToPCL();

            HttpExtensionsFromPCL();
            HttpExtensionsToPCL();

			await HttpPrimitivesFromPCL();
            await HttpPrimitivesToPCL();
        }

        async Task GoogleFromPCL()
        {
            var http = Portable.Compatibility.CreateHttpClient();

            var google = await http.GetStringAsync("http://www.google.com");
            Console.WriteLine("iOS: Google: {0}", google.Contains("I'm Feeling Lucky"));
        }

        async Task GoogleToPCL()
        {
            var http = new HttpClient();
            await Portable.Compatibility.Google(http);
        }

        void HttpExtensionsFromPCL()
        {
            var httpClientHandler = Portable.Compatibility.CreateHttpClientHandler();

            Console.WriteLine("iOS: System.Net.Http.HttpClientHandlerExtensions.SupportsAllowAutoRedirect: {0}",
                System.Net.Http.HttpClientHandlerExtensions.SupportsAllowAutoRedirect(httpClientHandler));
        }

        void HttpExtensionsToPCL()
        {
            var httpClientHandler = new HttpClientHandler();

            Portable.Compatibility.TestHttpClientHandlerExtensions(httpClientHandler);
        }

        async Task HttpPrimitivesToPCL()
        {
            var handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip |
                                                 DecompressionMethods.Deflate;
            }

            await Portable.Compatibility.TestHttpPrimitives(handler);
        }

        async Task HttpPrimitivesFromPCL()
        {
            var handler = Portable.Compatibility.CreateHttpClientHandlerWithCompression();
            var httpClient = new HttpClient(handler);
            var str = await httpClient.GetStringAsync("http://en.wikipedia.org/wiki/Gzip");

            Console.WriteLine("iOS: Wikipedia: {0}", str.Contains("Wikipedia"));
        }
    }
}