public record Listener<T>(ConsoleKey key, T value) where T:Enum;
public record Input<T>(T value) where T:Enum;

public static class Check<T> where T:Enum
{
    public static List<Input<T>> Inputs(params Listener<T>[] listeners)
    {
        var inputs = new List<Input<T>>();
        while (Console.KeyAvailable)
        {
            // Todo: De-dup?
            var key = Console.ReadKey(true);
            inputs.AddRange(listeners
                .Where(l => l.key == key.Key)
                .ToHashSet()
                .Select(l => new Input<T>(l.value)));
        }
        return inputs;
    }

    public static async Task<List<Input<T>>> InputsWait(params Listener<T>[] listeners)
    {
        var inputs = new List<Input<T>>();
        while(!inputs.Any())
        {
            await Task.Delay(150);
            inputs = Inputs(listeners);
        }
        return inputs;
    }
}

