
public static class Output
{
    private static int Width => Console.WindowWidth;
    private static int Height => Console.WindowHeight;
    private static int Top => Console.WindowTop;

    private const char corner = '+';
    private const char ver = '|';
    private const char hor = '-';

    public static void PrintGame() { }

    public static void PrintMenu()
    {
        Console.Clear();
        int x = (int)(Width / 2) - 10;
        int width = 20 + (Width % 2);
        int y = (int)(Height / 2) - 10;
        int height = 12;
        DrawCenteredText(y - 3, "ZOMBIE DICE");
        DrawSquare(x, y, width, height);
        DrawCenteredText(y + 2, "[P]lay Game");
        DrawHorLine(y + 4, x+1, width-2);
        DrawCenteredText(y + 6, "[H]elp");
        DrawHorLine(y + 8, x+1, width-2);
        DrawCenteredText(y + 10, "[E]xit");
    }

    public static void DrawCenteredText(int y, string s)
    {
        int offset = (int)(Width / 2 - s.Length / 2) - s.Length % 2;
        DrawText(offset, y, s);
    }

    public static void DrawHorLine(int y, int x = 0, int length = 0)
    {
        DrawText(x, y, new String(hor, length == 0 ? Width - x : length));
    }

    public static void DrawSquare(int _x, int _y, int _width, int _height, bool infill = false)
    {
        var width = _x + _width;
        var height = _y + _height;
        var line = $"|{new String(' ', _width - 2)}|";

        DrawText(_x, _y, $"+{new String('-', _width - 2)}+");
        DrawText(_x, height, $"+{new String('-', _width - 2)}+");

        Action<int> print = infill ? PrintInfill : PrintClear;
        for(var y = _y + 1; y < height; y++)
        {
            print(y);
        }

        void PrintInfill(int y) => DrawText(_x, y, line);

        void PrintClear(int y)
        {
            PrintChar(_x, y, ver);
            PrintChar(width - 1, y, ver);
        }
    }

    private static void PrintChar(char c)
        => Console.Write(c);

    private static void PrintChar(int x, int y, char c)
    {
        Console.SetCursorPosition(x, y);
        PrintChar(c);
    }

    private static void DrawText(int x, int y, string s)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(s);
    }
}

