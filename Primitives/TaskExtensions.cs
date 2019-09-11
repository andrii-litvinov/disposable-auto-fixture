using System;
using System.Threading.Tasks;

namespace Primitives
{
    public static class TaskExtensions
    {
        public static async Task SwallowCancellation(this Task task)
        {
            try
            {
                await task;
            }
            catch (OperationCanceledException)
            {
            }
        }
        
        public static async Task<T> SwallowCancellation<T>(this Task<T> task)
        {
            try
            {
                return await task;
            }
            catch (OperationCanceledException)
            {
                return default;
            }
        }
    }
}
