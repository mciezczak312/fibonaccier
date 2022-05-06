Console.Write("N = ");
var n = Convert.ToInt32(Console.ReadLine());

var tasks = new Dictionary<Task<int>, string>
{
    { FibSolutions.FibAsyncWithRandomDelay(n), "task_id_1" },
    { FibSolutions.FibAsyncWithRandomDelay(n), "task_id_2" }
};

var taskResults = new Dictionary<string, FibResult>();

while (tasks.Count > 0)
{
    var t = await Task.WhenAny(tasks.Keys);
    var taskId = tasks[t];
    tasks.Remove(t);
    await t;
    taskResults.Add(taskId,
        new FibResult { CompletedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(), NthFibNumber = t.Result });
}

var firstCompletedTask = taskResults.First();

Console.WriteLine(
    $"Task: {firstCompletedTask.Key} completed first at {firstCompletedTask.Value.CompletedAt}. Fib({n}) = {firstCompletedTask.Value.NthFibNumber}");

public class FibResult
{
    public long CompletedAt { get; set; }

    public int NthFibNumber { get; set; }
}

public class FibSolutions
{
    public static Random Rnd = new();
    const int MAX_DELAY_MS = 1000;
    
    public static async Task<int> FibAsyncWithRandomDelay(int n)
    {
        var delay = Rnd.Next(0, MAX_DELAY_MS);
        await Task.Delay(delay);
        return await FibAsync(n);
    }

    
    public static async Task<int> FibAsync(int n)
    {
        if (n < 0)
        {
            throw new Exception("Extension to negative numbers not implemented");
        }

        if (n == 0)
        {
            return 0;
        }

        if (n == 1)
        {
            return 1;
        }

        var results = await Task.WhenAll(Task.Run(() => FibAsync(n - 1)), Task.Run(() => FibAsync(n - 2)));
        return results[0] + results[1];
    }
    
    public static int FibSync(int n)
    {
        if (n < 0)
        {
            throw new Exception("Extension to negative numbers not implemented");
        }

        if (n == 0)
        {
            return 0;
        }

        if (n == 1)
        {
            return 1;
        }

        return FibSync(n - 1) + FibSync(n - 2);
    }

}