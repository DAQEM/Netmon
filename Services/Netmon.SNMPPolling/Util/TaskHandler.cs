namespace Netmon.SNMPPolling.Util;

public class TaskHandler
{
    public static async Task<T?> ExecuteWithTimeoutAsync<T>(Task<T> task, TimeSpan timeout, T? defaultValue)
    {
        if (task == null) throw new ArgumentNullException(nameof(task));

        using (CancellationTokenSource cancellationTokenSource = new())
        {
            Task completedTask = await Task.WhenAny(task, Task.Delay(timeout, cancellationTokenSource.Token));
            if (completedTask == task)
            {
                cancellationTokenSource.Cancel();
                return await task;
            }

            return defaultValue;
        }
    }
}