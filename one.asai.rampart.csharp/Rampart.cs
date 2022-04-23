namespace Rampart;

/// <summary>
/// This type represents an interval bounded by two values, the lesser and the greater.
/// </summary>
/// <typeparam name="T"><see cref="IComparable{T}"/> instance</typeparam>
public struct Interval<T> where T : IComparable<T>, IComparable
{
    /// <summary>
    /// Gets the lesser value.
    /// </summary>
    public readonly T Lesser;

    /// <summary>
    /// Gets the greater value.
    /// </summary>
    public readonly T Greater;

    /// <summary>
    /// Interval can be sorted on construction.
    /// </summary>
    /// <param name="x">value 1</param>
    /// <param name="y">value 2</param>
    public Interval(T x, T y) =>
        (Lesser, Greater) = x.CompareTo(y) <= 0 ? (x, y) : (y, x);

    /// <summary>
    /// Returns True if the given Interval is empty, False otherwise.
    /// An Interval is empty if its lesser equals its greater.
    /// </summary>
    public bool IsEmpty => Lesser.CompareTo(Greater) == 0;

    /// <summary>
    /// Returns True if the given Interval is non-empty, False otherwise.
    /// An Interval is non-empty if its lesser is not equal to its greater.
    /// </summary>
    public bool IsNonEmpty => !IsEmpty;

    /// <summary>
    /// Relates another Interval.
    /// </summary>
    /// <param name="other">Interval</param>
    /// <returns>Consult the <see cref="Relation"/> documentation for an explanation of all the possible results.</returns>
    public Relation Relate(Interval<T> other) =>
        Relate(this, other);

    /// <summary>
    /// Relates two Intervals.
    /// Calling relate x y tells you how Interval x relates to Interval y.
    /// </summary>
    /// <param name="x">Interval x</param>
    /// <param name="y">Interval y</param>
    /// <returns>Consult the <see cref="Relation"/> documentation for an explanation of all the possible results.</returns>
    public static Relation Relate(Interval<T> x, Interval<T> y)
    {
        var lxly = x.Lesser.CompareTo(y.Lesser);
        var lxgy = x.Lesser.CompareTo(y.Greater);
        var gxly = x.Greater.CompareTo(y.Lesser);
        var gxgy = x.Greater.CompareTo(y.Greater);

        switch ((lxly, lxgy, gxly, gxgy))
        {
            case (0, _, _, 0):
                return Relation.Equal;
            case (_, _, < 0, _):
                return Relation.Before;
            case ( < 0, _, 0, < 0):
                return Relation.Meets;
            case (_, _, 0, _):
                return Relation.Overlaps;
            case ( > 0, 0, _, > 0):
                return Relation.MetBy;
            case (_, 0, _, _):
                return Relation.OverlappedBy;
            case (_, > 0, _, _):
                return Relation.After;
            case ( < 0, _, _, < 0):
                return Relation.Overlaps;
            case ( < 0, _, _, 0):
                return Relation.FinishedBy;
            case ( < 0, _, _, > 0):
                return Relation.Contains;
            case (0, _, _, < 0):
                return Relation.Starts;
            case (0, _, _, > 0):
                return Relation.StartedBy;
            case ( > 0, _, _, < 0):
                return Relation.During;
            case ( > 0, _, _, 0):
                return Relation.Finishes;
            case ( > 0, _, _, > 0):
                return Relation.OverlappedBy;
        }
    }
}

/// <summary>
/// This type describes how two Intervals relate to each other.
/// Each constructor represents one of the 13 possible relations.
/// Taken together these relations are mutually exclusive and exhaustive.
/// </summary>
public enum Relation
{
    /// <summary>
    /// Interval x is before Interval y.
    /// </summary>
    /// <remarks>
    /// <code>
    ///     +---+
    ///     | x |
    ///     +---+
    ///           +---+
    ///           | y |
    ///           +---+
    /// </code>
    /// </remarks>
    Before,

    /// <summary>
    /// Interval x meets Interval y.
    /// </summary>
    /// <remarks>
    /// <code>
    ///     +---+
    ///     | x |
    ///     +---+
    ///         +---+
    ///         | y |
    ///         +---+
    /// </code>
    /// </remarks>
    Meets,

    /// <summary>
	///	Interval x overlaps Interval y.
    /// </summary>
    /// <remarks>
    /// <code>
	///	    +---+
	///	    | x |
	///	    +---+
	///	      +---+
	///	      | y |
	///	      +---+
    /// </code>
    /// </remarks>
    Overlaps,

    /// <summary>
	///	Interval x is finished by Interval y.
    /// </summary>
    /// <remarks>
    /// <code>
	///	    +-----+
	///	    |  x  |
	///	    +-----+
	///	      +---+
	///	      | y |
	///	      +---+
    /// </code>
    /// </remarks>
    FinishedBy,

    /// <summary>
	///	Interval x contains Interval y.
    /// </summary>
    /// <remarks>
    /// <code>
	///	    +-------+
	///	    |   x   |
	///	    +-------+
	///	      +---+
	///	      | y |
	///	      +---+
    /// </code>
    /// </remarks>
    Contains,

    /// <summary>
	///	Interval x starts Interval y.
    /// </summary>
    /// <remarks>
    /// <code>
	///	    +---+
	///	    | x |
	///	    +---+
	///	    +-----+
	///	    |  y  |
	///	    +-----+
    /// </code>
    /// </remarks>
    Starts,

    /// <summary>
	///	Interval x is equal to Interval y.
    /// </summary>
    /// <remarks>
    /// <code>
	///	    +---+
	///	    | x |
	///	    +---+
	///	    +---+
	///	    | y |
	///	    +---+
    /// </code>
    /// </remarks>
    Equal,

    /// <summary>
	///	Interval x is started by Interval y.
    /// </summary>
    /// <remarks>
    /// <code>
	///	    +-----+
	///	    |  x  |
	///	    +-----+
	///	    +---+
	///	    | y |
	///	    +---+
    /// </code>
    /// </remarks>
    StartedBy,

    /// <summary>
	///	Interval x is during Interval y.
    /// </summary>
    /// <remarks>
    /// <code>
	///	      +---+
	///	      | x |
	///	      +---+
	///	    +-------+
	///	    |   y   |
	///	    +-------+
    /// </code>
    /// </remarks>
    During,

    /// <summary>
	///	Interval x finishes Interval y.
    /// </summary>
    /// <remarks>
    /// <code>
	///	      +---+
	///	      | x |
	///	      +---+
	///	    +-----+
	///	    |  y  |
	///	    +-----+
    /// </code>
    /// </remarks>
    Finishes,

    /// <summary>
	///	Interval x is overlapped by Interval y.
    /// </summary>
    /// <remarks>
    /// <code>
	///	      +---+
	///	      | x |
	///	      +---+
	///	    +---+
	///	    | y |
	///	    +---+
    /// </code>
    /// </remarks>
    OverlappedBy,

    /// <summary>
	///	Interval x is met by Interval y.
    /// </summary>
    /// <remarks>
    /// <code>
	///	        +---+
	///	        | x |
	///	        +---+
	///	    +---+
	///	    | y |
	///	    +---+
    /// </code>
    /// </remarks>
    MetBy,

    /// <summary>
	///	Interval x is after Interval y.
    /// </summary>
    /// <remarks>
    /// <code>
	///	          +---+
	///	          | x |
	///	          +---+
	///	    +---+
	///	    | y |
	///	    +---+
    /// </code>
    /// </remarks>
    After,
}
