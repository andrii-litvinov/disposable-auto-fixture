using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Primitives
{
    public abstract class Disposable : IAsyncLifetime
    {
        // Can be applied to both tests and fixtures, but does not guaranty fixture disposal.
        
        protected Func<Task> OnDispose { get; set; } = async () => { };
        Task IAsyncLifetime.InitializeAsync() => Initialize();

        async Task IAsyncLifetime.DisposeAsync()
        {
            foreach (var func in OnDispose.GetInvocationList().Cast<Func<Task>>()) await func.Invoke();
        }

        protected virtual async Task Initialize()
        {
        }
    }
}
