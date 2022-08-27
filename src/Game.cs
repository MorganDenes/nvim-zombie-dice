public enum GameInput{ Roll, Stop, Check }

public record Game(int players, int HardCount, int MediumCount, int EasyCount)
{
    private GameDice dice = new GameDice(HardCount, MediumCount, EasyCount);

    public async Task Play()
    {
        var quit = false;
        var currentPlayer = 0;
        var inputs = new List<Input<GameInput>>();
        while (!quit || inputs.Any(i => i.value == GameInput.Stop))
        {
            dice.ResetRoll();
            var pressLuck = true;
            while (pressLuck)
            {
                var results = dice.GetNewRoll();
                PrintBrains();
                PrintSkull();
                PrintResults();
                inputs = await Check<GameInput>.InputsWait(
                    new(ConsoleKey.R, GameInput.Roll),
                    new(ConsoleKey.S, GameInput.Stop));
            }
            currentPlayer = currentPlayer++ % players;
        }
    }

    private void PrintBrains()
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write(new String(DiceFaces.brain, dice.Results.Count(r => Difficulty(r, DiceDifficulty.Easy))));
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write(new String(DiceFaces.brain, dice.Results.Count(r => Difficulty(r, DiceDifficulty.Medium))));
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(new String(DiceFaces.brain, dice.Results.Count(r => Difficulty(r, DiceDifficulty.Hard))));

        bool Difficulty(DiceResult die, DiceDifficulty difficulty)
            => die.Face == DiceFace.Brain && die.Difficulty == difficulty;
    }

    private void PrintSkull()
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write(new String(DiceFaces.skull, dice.Results.Count(r => Difficulty(r, DiceDifficulty.Easy))));
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write(new String(DiceFaces.skull, dice.Results.Count(r => Difficulty(r, DiceDifficulty.Medium))));
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(new String(DiceFaces.skull, dice.Results.Count(r => Difficulty(r, DiceDifficulty.Hard))));

        bool Difficulty(DiceResult die, DiceDifficulty difficulty)
            => die.Face == DiceFace.Skull && die.Difficulty == difficulty;
    }

    private void PrintResults()
    {
        foreach (var die in dice.LastResults)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor =  die.Difficulty switch {
                DiceDifficulty.Easy => ConsoleColor.DarkGreen,
                DiceDifficulty.Medium => ConsoleColor.DarkYellow,
                DiceDifficulty.Hard => ConsoleColor.DarkRed,
                _ => throw new("Impossible")
            };
            Console.Write(die.Face switch {
                DiceFace.Brain => DiceFaces.brain,
                DiceFace.Footprint => DiceFaces.footprint,
                DiceFace.Skull => DiceFaces.skull,
                _ => throw new("Impossible")
            });
        }
        Console.WriteLine("");
    }
}

