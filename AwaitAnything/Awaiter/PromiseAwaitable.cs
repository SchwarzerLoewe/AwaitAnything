using System;
using System.Collections.Generic;

namespace AwaitAnything.Awaiter
{
    internal static class ResultSet
    {
        public static Queue<object> Queue = new Queue<object>();
    }

    internal struct PromiseAwaitable<TResult> : IAwaitable<TResult>
    {
        private readonly Action<Promise<TResult>> function;

        public PromiseAwaitable(Action<Promise<TResult>> function)
        {
            this.function = function;
        }

        public IAwaiter<TResult> GetAwaiter() => new PromiseAwaiter<TResult>(function);
    }

    public struct PromiseAwaiter<TResult> : IAwaiter<TResult>
    {
        private readonly Deferred<TResult> task;

        public PromiseAwaiter(Action<Deferred<TResult>> function)
        {
            task = new Deferred<TResult>();
            function(task);
            isValueSet = false;
            Value = null;
        }

        bool IAwaiter<TResult>.IsCompleted => task.IsResolved;

        public object Value { get; private set; }

        public void OnCompleted(Action continuation)
        {
            var self = this;
            task.Done((_) =>
            {
                self.Value = _;
                self.SetIsValueSet(true);
            });

            continuation();
            isValueSet = true;
        }

        private void SetIsValueSet(bool val)
        {
            isValueSet = val;
        }

        bool isValueSet;

        TResult IAwaiter<TResult>.GetResult()
        {
            object res = null;
            if (isValueSet)
            {
                res = Value;
            }

            return (TResult)res;
        }
    }
}