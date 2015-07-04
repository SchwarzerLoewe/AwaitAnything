using AwaitAnything.Awaiter;
using System;

namespace AwaitAnything
{
    public static class GenericExtensions
    {
        public static IAwaiter<TResult> GetAwaiter<TResult>(this Func<TResult> function)
        {
            return new FuncAwaiter<TResult>(function);
        }
    }
}