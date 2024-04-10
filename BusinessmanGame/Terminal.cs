using System.Runtime.InteropServices.JavaScript;

namespace BusinessmanGame;

public static class Terminal
{
    private static bool _isBlocked = false;

    public static void DrawInterface()
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine(
            $"{(char)Ascii.LeftTopCorner}{new String((char)Ascii.Horizontal, 78)}{(char)Ascii.RightTopCorner}");
        Console.Write($"{(char)Ascii.Vertical}");
        Console.SetCursorPosition(79, 1);
        Console.WriteLine($"{(char)Ascii.Vertical}");
        Console.WriteLine(
            $"{(char)Ascii.VerticalLeft}{new String((char)Ascii.Horizontal, 13)}{(char)Ascii.HorizontalTop}{new String((char)Ascii.Horizontal, 64)}{(char)Ascii.VerticalRight}");
        for (var i = 0; i < 6; i++)
        {
            Console.Write($"{(char)Ascii.Vertical}");
            Console.SetCursorPosition(14, i + 3);
            Console.Write($"{(char)Ascii.Vertical}");
            Console.SetCursorPosition(79, i + 3);
            Console.WriteLine($"{(char)Ascii.Vertical}");
        }

        Console.Write($"{(char)Ascii.Vertical}");
        Console.SetCursorPosition(14, 9);
        Console.WriteLine(
            $"{(char)Ascii.VerticalLeft}{new String((char)Ascii.Horizontal, 64)}{(char)Ascii.VerticalRight}");
        for (var i = 0; i < 19; i++)
        {
            Console.Write($"{(char)Ascii.Vertical}");
            Console.SetCursorPosition(14, i + 10);
            Console.WriteLine(
                $"{(char)Ascii.Vertical}{new String(' ', 64)}{(char)Ascii.Vertical}");
        }

        Console.WriteLine(
            $"{(char)Ascii.LeftBottomCorner}{new String((char)Ascii.Horizontal, 13)}{(char)Ascii.HorizontalBottom}{new String((char)Ascii.Horizontal, 64)}{(char)Ascii.RightBottomCorner}");
        Console.SetCursorPosition(0, 0);
    }
    public static void DrawSheet(List<string[]> items)
    {
        while (_isBlocked)
        {
        }

        _isBlocked = true;
        DrawInterface();
        var nameColWidth = 60 - 10 * (items[0].Length - 2);
        Console.SetCursorPosition(14, 9);
        var topBorder =
            $"{(char)Ascii.VerticalLeft}{new String((char)Ascii.Horizontal, 3)}{(char)Ascii.HorizontalTop}{new String((char)Ascii.Horizontal, nameColWidth)}{(char)Ascii.HorizontalTop}";
        for (var i = 2; i < items[0].Length; i++)
        {
            topBorder +=
                $"{new String((char)Ascii.Horizontal, 9)}{(i + 1 == items[0].Length ? (char)Ascii.VerticalRight : (char)Ascii.HorizontalTop)}";
        }

        Console.WriteLine(topBorder);
        for (var i = 0; i < items.Count; i++)
        {
            Console.SetCursorPosition(14, 10 + i * 2);
            var dataLine =
                $"{(char)Ascii.Vertical} {items[i][0]} {(char)Ascii.Vertical} {items[i][1]}{new String(' ', nameColWidth - 1 - items[i][1].Length)}{(char)Ascii.Vertical}";
            for (var j = 2; j < items[i].Length; j++)
            {
                dataLine += $" {items[i][j]}{new String(' ', 8 - items[i][j].Length)}{(char)Ascii.Vertical}";
            }

            Console.Write(dataLine);
            Console.SetCursorPosition(14, 11 + i * 2);
            var specialBorderChar = i + 1 == items.Count ? (char)Ascii.HorizontalBottom : (char)Ascii.Cross;
            var bottomBorder =
                $"{(char)Ascii.VerticalLeft}{new String((char)Ascii.Horizontal, 3)}{specialBorderChar}{new String((char)Ascii.Horizontal, nameColWidth)}{specialBorderChar}";
            for (var j = 2; j < items[i].Length; j++)
            {
                bottomBorder +=
                    $"{new String((char)Ascii.Horizontal, 9)}{(j + 1 == items[i].Length ? (char)Ascii.VerticalRight : specialBorderChar)}";
            }

            Console.Write(bottomBorder);
        }

        _isBlocked = false;
    }
    public static string AskName()
    {
        while (_isBlocked)
        {
        }

        _isBlocked = true;
        var lines = new string[] { "Enter your name" };
        WriteQuestion(lines);
        var playerName = "";
        _isBlocked = false;
        do
        {
            Console.SetCursorPosition(2, 1);
            playerName = Console.ReadLine();
        } while (playerName == "");

        return playerName;
    }
    public static void WriteMessage(string text)
    {
        while (_isBlocked)
        {
        }

        _isBlocked = true;
        var currentCursorPosition = Console.GetCursorPosition();
        Console.CursorVisible = false;
        ClearCell(64, 4, 15, 3);
        var currentRow = 3;
        var strings = FitStrings(text, 62);
        for (var i = 0; i < strings.Count; i++)
        {
            Console.SetCursorPosition(16, currentRow);
            Console.Write(strings[i]);
            currentRow++;
        }

        Console.SetCursorPosition(currentCursorPosition.Left, currentCursorPosition.Top);
        Console.CursorVisible = true;
        _isBlocked = false;
    }
    public static string AskQuestion(string[] lines)
    {
        while (_isBlocked)
        {
        }

        _isBlocked = true;
        WriteQuestion(lines);
        _isBlocked = false;
        var answer = Console.ReadLine();
        return answer;
    }
    public static void PrintBalance(int balance)
    {
        while (_isBlocked)
        {
        }

        _isBlocked = true;
        var currentCursorPosition = Console.GetCursorPosition();
        Console.CursorVisible = false;
        ClearCell(5, 1, 73, 1);
        Console.SetCursorPosition(73, 1);
        Console.Write(balance);
        Console.SetCursorPosition(currentCursorPosition.Left, currentCursorPosition.Top);
        Console.CursorVisible = true;
        _isBlocked = false;
    }
    private static List<string> FitStrings(string text, int width)
    {
        var strings = new List<string>();
        var words = text.Split(' ');
        var newString = words[0];
        for (var i = 0; i < words.Length - 1; i++)
        {
            if ((newString + " " + words[i + 1]).Length <= width)
            {
                newString += " " + words[i + 1];
            }
            else
            {
                strings.Add(newString);
                newString = words[i + 1];
            }
        }

        strings.Add(newString);
        return strings;
    }
    private static void WriteQuestion(string[] text)
    {
        ClearCell(11, 26, 2, 3);
        var currentRow = 3;
        for (var i = 0; i < text.Length; i++)
        {
            var strings = FitStrings(text[i], 11);
            for (var j = 0; j < strings.Count; j++)
            {
                Console.SetCursorPosition(2, currentRow);
                Console.Write(strings[j]);
                currentRow++;
            }
        }

        Console.SetCursorPosition(2, currentRow);
    }
    private static void ClearCell(int width, int height, int startCol, int startRow)
    {
        for (var i = startRow; i < startRow + height; i++)
        {
            for (var j = startCol; j < startCol + width; j++)
            {
                Console.SetCursorPosition(j, i);
                Console.Write(" ");
            }
        }
    }
}