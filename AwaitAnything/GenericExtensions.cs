using AwaitAnything.Awaiter;
using System;

namespace AwaitAnything
{
    public static class GenericExtensions
    {
        public static IAwaiter<TResult> GetAwaiter<TResult>(this Func<TResult> function) => 
            new FuncAwaiter<TResult>(function);
    }
}