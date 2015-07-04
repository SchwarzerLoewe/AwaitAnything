using System;
using System.Threading.Tasks;

namespace AwaitAnything.Awaiter
{
    internal struct FuncAwaitable<TResult> : IAwaitable<TResult>
    {
        private readonly Func<TResult> function;

        public FuncAwaitable(Func<TResult> function)
        {
            this.function = function;
        }

        public IAwaiter<TResult> GetAwaiter()
        {
            return new FuncAwaiter<TResult>(function);
        }
    }

    public struct FuncAwaiter<TResult> : IAwaiter<TResult>
    {
        private readonly Task<TResult> task;

        public FuncAwaiter(Func<TResult> function)
        {
            task = new Task<TResult>(function);
            task.Start();
        }

        bool IAwaiter<TResult>.IsCompleted
        {
            get
            {
                return task.IsCompleted;
            }
        }

        public void OnCompleted(Action continuation)
        {
            new Task(continuation).Start();
        }

        TResult IAwaiter<TResult>.GetResult()
        {
            return task.Result;
        }
    }
}