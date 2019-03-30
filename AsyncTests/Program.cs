using AwaitAnything;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var res = await fetch("http://google.de/");
        }


        private static Promise<string> fetch(string url)
        {
            var def = new Deferred<string>(_ =>
            {
                var wc = new WebClient();
                wc.DownloadStringCompleted += (__, e) =>
                {
                    if (e.Error != null)
                    {
                        _.Reject(e.Error.ToString());
                    }
                    else
                    {
                        _.Resolve(e.Result);
                    }
                };

                wc.DownloadStringAsync(new Uri(url));
            });

            return def.Promise();
        }
    }
}
