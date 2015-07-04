﻿using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AwaitAnything
{
    public static class Extensions
    {
        public static TaskAwaiter GetAwaiter(this int v)
        {
            return Task.Delay(v).GetAwaiter();
        }

        public static TaskAwaiter GetAwaiter(this TimeSpan timeSpan)
        {
            return Task.Delay(timeSpan).GetAwaiter();
        }

        public static TaskAwaiter GetAwaiter(this DateTimeOffset dateTimeOffset)
        {
            return (dateTimeOffset - DateTimeOffset.UtcNow).GetAwaiter();
        }

        public static TaskAwaiter<int> GetAwaiter(this Process process)
        {
            var tcs = new TaskCompletionSource<int>();
            process.EnableRaisingEvents = true;
            process.Exited += (s, e) => tcs.TrySetResult(process.ExitCode);
            if (process.HasExited) tcs.TrySetResult(process.ExitCode);
            return tcs.Task.GetAwaiter();
        }

        public static TaskAwaiter GetAwaiter(this Action action)
        {
            Task task = new Task(action);
            task.Start();
            return task.GetAwaiter(); // Returns a TaskAwaiter.
        }
    }
}