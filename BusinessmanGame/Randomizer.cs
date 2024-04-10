namespace BusinessmanGame;

public static class Randomizer
{
    private static Random _random = new();

    public static int Next(int min, int max)
    {
        return _random.Next(min, max);
    }
    public static void Shuffle<T>(List<T> items)
    {
        for (var i = 0; i < items.Count - 1; i++)
        {
            var randomIndex = Randomizer.Next(i, items.Count);
            (items[i], items[randomIndex]) = (items[randomIndex], items[i]);
        }
    }
}