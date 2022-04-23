using Xunit;

namespace Rampart;

public class DoubleTest
{
    [Fact]
    public void InstanceTest()
    {
        var i0 = new Interval<double>(0.0, 0.0);
        Assert.True(i0.IsEmpty);
        Assert.False(i0.IsNonEmpty);
        Assert.Equal(0.0, i0.Lesser);
        Assert.Equal(0.0, i0.Greater);

        var i1 = new Interval<double>(0.0, 1.0);
        Assert.False(i1.IsEmpty);
        Assert.True(i1.IsNonEmpty);
        Assert.Equal(0.0, i1.Lesser);
        Assert.Equal(1.0, i1.Greater);

        var i2 = new Interval<double>(1.0, 0.0);
        Assert.False(i2.IsEmpty);
        Assert.True(i2.IsNonEmpty);
        Assert.Equal(0.0, i2.Lesser);
        Assert.Equal(1.0, i2.Greater);
    }

    [Fact]
    public void RelationTest()
    {
        var y = new Interval<double>(3.0, 7.0);
        var xBefore = new Interval<double>(1.0, 2.0);
        var xMeets = new Interval<double>(2.0, 3.0);
        var xOverlaps = new Interval<double>(2.0, 4.0);
        var xFinishedBy = new Interval<double>(2.0, 7.0);
        var xContains = new Interval<double>(2.0, 8.0);
        var xStarts = new Interval<double>(3.0, 4.0);
        var xEqual = new Interval<double>(3.0, 7.0);
        var xStartedBy = new Interval<double>(3.0, 8.0);
        var xDuring = new Interval<double>(4.0, 6.0);
        var xFinishes = new Interval<double>(6.0, 7.0);
        var xOverlappedBy = new Interval<double>(6.0, 8.0);
        var xMetBy = new Interval<double>(7.0, 8.0);
        var xAfter = new Interval<double>(8.0, 9.0);

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
        var xEmpty0 = new Interval<double>(3.0, 3.0);
        var xEmpty1 = new Interval<double>(7.0, 7.0);
        var yInterval = new Interval<double>(3.0, 7.0);
        Assert.Equal(Relation.Overlaps, xEmpty0.Relate(yInterval));
        Assert.Equal(Relation.OverlappedBy, xEmpty1.Relate(yInterval));

        var xInterval = new Interval<double>(3.0, 7.0);
        var yEmpty0 = new Interval<double>(3.0, 3.0);
        var yEmpty1 = new Interval<double>(7.0, 7.0);
        Assert.Equal(Relation.OverlappedBy, xInterval.Relate(yEmpty0));
        Assert.Equal(Relation.Overlaps, xInterval.Relate(yEmpty1));
    }
}
