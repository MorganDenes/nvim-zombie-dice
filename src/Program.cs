public enum MainMenuInput{
    Play,
    Help,
    End
}

internal class Program
{
    const int EasyCount = 7;
    const int MediumCount = 5;
    const int HardCount = 3;

    private static async Task Main(string[] args)
    {
        Console.CancelKeyPress += delegate { End(); };

        bool end = false;
        Console.CursorVisible = false;
        while (!end)
        {
            Output.PrintMenu();
            var inputs = Check<MainMenuInput>.Inputs(
                new(ConsoleKey.P, MainMenuInput.Play),
                new(ConsoleKey.H, MainMenuInput.Help),
                new(ConsoleKey.E, MainMenuInput.End));
            if (inputs.Any(i => i.value == MainMenuInput.Play))
            {
                await new Game(1, 3, 5, 7).Play();
            }
            else if (inputs.Any(i => i.value == MainMenuInput.Help))
            {
            }
            else if (inputs.Any(i => i.value == MainMenuInput.End))
            {
                end = true;
            }
            await Task.Delay(200);
        }
        End();
    }

    private static void End()
    {
        Console.Clear();
        Console.CursorVisible = true;
    }

    private static void sandbox()
    {
        Console.CursorVisible = false;
        while (true)
        {
            Console.Clear();
            Output.DrawSquare(6, 6, 8, 8);
            Output.DrawSquare(5, 5, 5, 5);
            Output.DrawSquare(16, 16, 8, 8);
            Output.DrawSquare(15, 15, 5, 5, true);
            Task.Delay(300).Wait();
        }
    }

    private static void Print(string value) => Console.WriteLine(value);
}
