using Italbytz.Common.Random;

namespace Italbytz.Common.Random.Tests;

[TestClass]
public sealed class RandomExtensionsTests
{
    [TestMethod]
    public void RollDie_ReturnsValueWithinRange()
    {
        var random = new System.Random(42);

        var dieRoll = random.RollDie(6);

        Assert.IsTrue(dieRoll >= 1 && dieRoll <= 6);
    }

    [TestMethod]
    public void Shuffle_PreservesAllItems()
    {
        var random = new System.Random(42);
        var items = new[] { 1, 2, 3, 4, 5 };
        var expected = items.ToArray();

        random.Shuffle(items);

        CollectionAssert.AreEquivalent(expected, items);
    }
}
