using Xunit;

namespace Rampart;

public class StringTest
{
    [Fact]
    public void InstanceTest()
    {
        var i0 = new Interval<string>("0", "0");
        Assert.True(i0.IsEmpty);
        Assert.False(i0.IsNonEmpty);
        Assert.Equal("0", i0.Lesser);
        Assert.Equal("0", i0.Greater);

        var i1 = new Interval<string>("0", "1");
        Assert.False(i1.IsEmpty);
        Assert.True(i1.IsNonEmpty);
        Assert.Equal("0", i1.Lesser);
        Assert.Equal("1", i1.Greater);

        var i2 = new Interval<string>("1", "0");
        Assert.False(i2.IsEmpty);
        Assert.True(i2.IsNonEmpty);
        Assert.Equal("0", i2.Lesser);
        Assert.Equal("1", i2.Greater);
    }

    [Fact]
    public void RelationTest()
    {
        var y = new Interval<string>("3", "7");
        var xBefore = new Interval<string>("1", "2");
        var xMeets = new Interval<string>("2", "3");
        var xOverlaps = new Interval<string>("2", "4");
        var xFinishedBy = new Interval<string>("2", "7");
        var xContains = new Interval<string>("2", "8");
        var xStarts = new Interval<string>("3", "4");
        var xEqual = new Interval<string>("3", "7");
        var xStartedBy = new Interval<string>("3", "8");
        var xDuring = new Interval<string>("4", "6");
        var xFinishes = new Interval<string>("6", "7");
        var xOverlappedBy = new Interval<string>("6", "8");
        var xMetBy = new Interval<string>("7", "8");
        var xAfter = new Interval<string>("8", "9");

        Assert.Equal(Relation.Before, xBefore.Relate(y));
        Assert.Equal(Relation.Meets, xMeets.Relate(y));
        Assert.Equal(Relation.Overlaps, xOverlaps.Relate(y));
        Assert.Equal(Relation.FinishedBy, xFinishedBy.Relate(y));
        Assert.Equal(Relation.Contains, xContains.Relate(y));
        Assert.Equal(Relation.Starts, xStarts.Relate(y));
        Assert.Equal(Relation.Equal, xEqual.Relate(y));
        Assert.Equal(Relation.StartedBy, xStartedBy.Relate(y));
        Assert.Equal(Relation.During, xDuring.Relate(y));
        Assert.Equal(Relation.Finishes, xFinishes.Relate(y));
        Assert.Equal(Relation.OverlappedBy, xOverlappedBy.Relate(y));
        Assert.Equal(Relation.MetBy, xMetBy.Relate(y));
        Assert.Equal(Relation.After, xAfter.Relate(y));
    }

    [Fact]
    public void EmptyRelationTest()
    {
        var xEmpty0 = new Interval<string>("3", "3");
        var xEmpty1 = new Interval<string>("7", "7");
        var yInterval = new Interval<string>("3", "7");
        Assert.Equal(Relation.Overlaps, xEmpty0.Relate(yInterval));
        Assert.Equal(Relation.OverlappedBy, xEmpty1.Relate(yInterval));

        var xInterval = new Interval<string>("3", "7");
        var yEmpty0 = new Interval<string>("3", "3");
        var yEmpty1 = new Interval<string>("7", "7");
        Assert.Equal(Relation.OverlappedBy, xInterval.Relate(yEmpty0));
        Assert.Equal(Relation.Overlaps, xInterval.Relate(yEmpty1));
    }
}
