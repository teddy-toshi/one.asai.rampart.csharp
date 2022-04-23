using Xunit;

namespace Rampart;

public class DateTimeOffsetTest
{
    readonly DateTimeOffset d0 = new (2000, 1, 1, 0, 0, 0, new(0));
    readonly DateTimeOffset d1 = new (2000, 1, 1, 0, 0, 1, new(0));
    readonly DateTimeOffset d2 = new (2000, 1, 1, 0, 0, 2, new(0));
    readonly DateTimeOffset d3 = new (2000, 1, 1, 0, 0, 3, new(0));
    readonly DateTimeOffset d4 = new (2000, 1, 1, 0, 0, 4, new(0));
    readonly DateTimeOffset d6 = new (2000, 1, 1, 0, 0, 6, new(0));
    readonly DateTimeOffset d7 = new (2000, 1, 1, 0, 0, 7, new(0));
    readonly DateTimeOffset d8 = new (2000, 1, 1, 0, 0, 8, new(0));
    readonly DateTimeOffset d9 = new (2000, 1, 1, 0, 0, 9, new(0));

    [Fact]
    public void InstanceTest()
    {
        var i0 = new Interval<DateTimeOffset>(d0, d0);
        Assert.True(i0.IsEmpty);
        Assert.False(i0.IsNonEmpty);
        Assert.Equal(d0, i0.Lesser);
        Assert.Equal(d0, i0.Greater);

        var i1 = new Interval<DateTimeOffset>(d0, d1);
        Assert.False(i1.IsEmpty);
        Assert.True(i1.IsNonEmpty);
        Assert.Equal(d0, i1.Lesser);
        Assert.Equal(d1, i1.Greater);

        var i2 = new Interval<DateTimeOffset>(d1, d0);
        Assert.False(i2.IsEmpty);
        Assert.True(i2.IsNonEmpty);
        Assert.Equal(d0, i2.Lesser);
        Assert.Equal(d1, i2.Greater);
    }

    [Fact]
    public void RelationTest()
    {
        var y = new Interval<DateTimeOffset>(d3, d7);
        var xBefore = new Interval<DateTimeOffset>(d1, d2);
        var xMeets = new Interval<DateTimeOffset>(d2, d3);
        var xOverlaps = new Interval<DateTimeOffset>(d2, d4);
        var xFinishedBy = new Interval<DateTimeOffset>(d2, d7);
        var xContains = new Interval<DateTimeOffset>(d2, d8);
        var xStarts = new Interval<DateTimeOffset>(d3, d4);
        var xEqual = new Interval<DateTimeOffset>(d3, d7);
        var xStartedBy = new Interval<DateTimeOffset>(d3, d8);
        var xDuring = new Interval<DateTimeOffset>(d4, d6);
        var xFinishes = new Interval<DateTimeOffset>(d6, d7);
        var xOverlappedBy = new Interval<DateTimeOffset>(d6, d8);
        var xMetBy = new Interval<DateTimeOffset>(d7, d8);
        var xAfter = new Interval<DateTimeOffset>(d8, d9);

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
        var xEmpty0 = new Interval<DateTimeOffset>(d3, d3);
        var xEmpty1 = new Interval<DateTimeOffset>(d7, d7);
        var yInterval = new Interval<DateTimeOffset>(d3, d7);
        Assert.Equal(Relation.Overlaps, xEmpty0.Relate(yInterval));
        Assert.Equal(Relation.OverlappedBy, xEmpty1.Relate(yInterval));

        var xInterval = new Interval<DateTimeOffset>(d3, d7);
        var yEmpty0 = new Interval<DateTimeOffset>(d3, d3);
        var yEmpty1 = new Interval<DateTimeOffset>(d7, d7);
        Assert.Equal(Relation.OverlappedBy, xInterval.Relate(yEmpty0));
        Assert.Equal(Relation.Overlaps, xInterval.Relate(yEmpty1));
    }
}
