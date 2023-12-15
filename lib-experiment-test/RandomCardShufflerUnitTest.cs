using Experiment;

namespace lib_experiment_test;

[TestClass]
public class RandomCardShufflerUnitTest
{
    [TestMethod]
    public void CheckShufflerOutput()
    {
        var shuffler = new RandomDeckShuffler();
        
        var output = shuffler.MakeShuffledCardDeck();
        
        Assert.IsNotNull(output);
        Assert.AreEqual(36, output.Count());
        Assert.AreEqual(18, output.Where(card => Card.IsBlack(card.Suit)).Count());
        Assert.AreEqual(4, output.Where(card => card.Value == Card.ValueType.Ace).Count());
    }
}
