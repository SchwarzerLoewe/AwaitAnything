using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace AwaitAnything
{
    public static class AsyncExtensions
    {
        public static Task<Socket> AcceptAsync(this Socket socket)
        {
            if (socket == null)
                throw new ArgumentNullException("socket");

            var tcs = new TaskCompletionSource<Socket>();

            socket.BeginAccept(asyncResult =>
            {
                try
                {
                    var s = asyncResult.AsyncState as Socket;
                    var client = s.EndAccept(asyncResult);

                    tcs.SetResult(client);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }

            }, socket);

            return tcs.Task;
        }

        public static async Task ForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> body)
        {
            await Task.Run(() =>
             {
                 return from item in source
                        select body(item);
             });
        }
    }
}