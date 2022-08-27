public enum DiceDifficulty { Easy, Medium, Hard }
public enum DiceFace { Brain, Skull, Footprint }

public record DiceResult(DiceDifficulty Difficulty, DiceFace Face);

public static class DiceFaces
{
    // public static char brain = 'b';
    // public static char skull = 's';
    // public static char footprint = 'f';
    public static char brain = '';
    public static char skull = 'ﮊ';
    public static char footprint = '';
}

public record GameDice(int EasyCount, int MediumCount, int HardCount)
{
    private Random random = new();
    private int easyLeft, mediumLeft, hardLeft;
    public List<DiceResult> Results { private set; get; } = new();
    public List<DiceResult> LastResults { private set; get; } = new();

    public void ResetRoll()
    {
        easyLeft =EasyCount;
        mediumLeft = MediumCount;
        hardLeft = HardCount;
        Results = new();
        LastResults = new();
    }

    private void ResetRollSoft()
    {
        var brains = Results.Where(r => r.Face == DiceFace.Brain).ToList();
        Results.RemoveAll(r => r.Face == DiceFace.Brain);
        easyLeft = brains.Where(b => b.Difficulty == DiceDifficulty.Easy).Count();
        mediumLeft = brains.Where(b => b.Difficulty == DiceDifficulty.Medium).Count();
        hardLeft = brains.Where(b => b.Difficulty == DiceDifficulty.Hard).Count();
    }

    public List<DiceResult> GetNewRoll()
    {
        Results.AddRange(LastResults.Where(r => r.Face != DiceFace.Footprint));
        LastResults = LastResults.Where(r => r.Face == DiceFace.Footprint)
            .Select(RollDie).ToList();
        while (LastResults.Count < 3)
        {
            LastResults.Add(GetDie());
        }
        return LastResults;
    }

    private DiceResult GetDie()
    {
        var left = easyLeft + mediumLeft + hardLeft;
        if (left == 0)
        {
            ResetRollSoft();
        }

        var die = random.Next(left);
        if (die < easyLeft)
        {
            easyLeft--;
            return RollEasy();
        }
        else if (die < easyLeft + mediumLeft)
        {
            mediumLeft--;
            return RollMedium();
        }
        else
        {
            Console.WriteLine("Hard die");
            hardLeft--;
            return RollHard();
        }
    }

    private DiceResult RollDie(DiceResult die)
        => RollDie(die.Difficulty);

    private DiceResult RollDie(DiceDifficulty difficulty)
        => difficulty switch {
            DiceDifficulty.Easy => RollEasy(),
            DiceDifficulty.Medium => RollMedium(),
            DiceDifficulty.Hard => RollHard(),
            _ => throw new("Impossible")};

    public DiceResult RollHard()
        => DeterminResult(1, DiceDifficulty.Hard);

    public DiceResult RollMedium()
        => DeterminResult(2, DiceDifficulty.Medium);

    public DiceResult RollEasy()
        => DeterminResult(3, DiceDifficulty.Easy);

    private const int BrainSkulls = 4;
    private DiceResult DeterminResult(int brains, DiceDifficulty difficulty)
    {
        var result = random.Next(6);
        if (result <= brains)
        {
            return new(difficulty, DiceFace.Brain);
        }
        else if (result <= BrainSkulls)
        {
            return new(difficulty, DiceFace.Footprint);
        }
        else
        {
            return new(difficulty, DiceFace.Skull);
        }
    }
}

