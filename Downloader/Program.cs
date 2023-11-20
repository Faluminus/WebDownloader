using System;
using System.Collections.Immutable;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static System.Net.WebRequestMethods;
using System.Security.Permissions;

namespace Downloader
{
    class Program
    {
        
        public static async Task Main(string[] args)
        {
            WebDownloader down = new WebDownloader("https://kfc.cz");
            await down.Url("https://kfc.cz").Download("KFC",@"C:\Users\semra\source\repos\Downloader\Downloader\Stuff");
        }
    }

    class WebDownloader
    {
        public ImmutableList<string> urls { get; private set; }

        public WebDownloader(string url)
        {
            this.urls = ImmutableList.Create<string>(url);
        }

        private WebDownloader(ImmutableList<string> urls)
        {
            this.urls = urls;
        }

        public WebDownloader Url(string url)
        {
            return new WebDownloader(this.urls.Add(url));
        }

        public async Task Download(string fileName , string directory)
        {
            using (var client = new HttpClient())
            {
                foreach (var url in urls)
                {
                    var web = await client.GetStringAsync(url);
                    var filePath = Path.Combine(directory, fileName);
                    using (var writer = System.IO.File.CreateText(filePath))
                    {
                        await writer.WriteAsync(web);
                    }
                }
            }
        }
    }
}