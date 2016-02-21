using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwaitAnything
{
    public static class AsyncExtensions
    {
        public static Task<Socket> AcceptAsync(this Socket socket)
        {
            if (socket == null)
                throw new ArgumentNullException(nameof(socket));

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

        public static Task<TcpClient> ConnectAsync(this TcpClient client, string host, int port)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            var tcs = new TaskCompletionSource<TcpClient>();

            client.BeginConnect(host, port, new AsyncCallback((asyncResult) =>
            {
                try
                {
                    var s = asyncResult.AsyncState as TcpClient;
                    s.EndConnect(asyncResult);

                    tcs.SetResult(client);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            }), client);

            return tcs.Task;
        }

        public static Task<DialogResult> ShowDialogAsync(this Form frm)
        {
            TaskCompletionSource<DialogResult> taskCompletionSource = new TaskCompletionSource<DialogResult>();

            EventHandler eventHandler = null;
            eventHandler =
                (sender, e) =>
                {
                    frm.Closed -= eventHandler;
                    taskCompletionSource.SetResult(frm.DialogResult);
                };
            frm.Closed += eventHandler;
            frm.Show();

            return taskCompletionSource.Task;
        }
    }
}