using System.Linq;

namespace Italbytz.Common.Random;

/// <summary>
/// Helpful extensions for <see cref="System.Random"/>.
/// </summary>
public static class RandomExtensions
{
    public static double NextDouble(this System.Random random, double max)
    {
        return random.NextDouble() * max;
    }

    public static double NextDouble(this System.Random random, double min, double max)
    {
        return min + (random.NextDouble() * (max - min));
    }

    public static int RollDie(this System.Random random, int sides)
    {
        return random.Next(0, sides) + 1;
    }

    public static void Shuffle<T>(this System.Random random, T[] items)
    {
        for (var i = 0; i < items.Length - 1; i++)
        {
            var j = random.Next(i, items.Length);
            (items[i], items[j]) = (items[j], items[i]);
        }
    }

    public static T RandomElement<T>(this System.Random random, T[] array)
    {
        return array[random.Next(0, array.Length)];
    }

    public static int[] NextIntArray(this System.Random random, int size, int min, int max)
    {
        return Enumerable
            .Repeat(0, size)
            .Select(_ => random.Next(min, max + 1))
            .ToArray();
    }

    public static int[] NextUniqueIntArray(this System.Random random, int size, int min, int max)
    {
        if (size > max)
        {
            throw new ArgumentException("Size is not allowed to be more than max", nameof(size));
        }

        var orderedList = Enumerable.Range(min, max + 1);
        return orderedList.OrderBy(_ => random.Next()).Take(size).ToArray();
    }

    public static string[] ShuffledStrings(this System.Random random, string[] array)
    {
        var pairs = array
            .Select(value => new KeyValuePair<int, string>(random.Next(), value))
            .OrderBy(pair => pair.Key)
            .ToArray();

        return pairs.Select(pair => pair.Value).ToArray();
    }
}
