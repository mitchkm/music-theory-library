namespace MegaMitch.MusicTheoryLibrary.Intervals;

public sealed partial class Interval
{
    public readonly int DistanceInSemitones;

    public readonly IntervalQuality Quality;

    public readonly uint Size;

    private Interval(int distanceInSemitones, IntervalQuality quality, uint size)
    {
        DistanceInSemitones = distanceInSemitones;
        Quality = quality;
        Size = size;
    }

    // #region Operations
    //
    // public static bool operator ==(Interval intervalA, Interval intervalB) =>
    //     intervalA.DistanceInSemitones == intervalB.DistanceInSemitones;
    //
    // public static bool operator !=(Interval intervalA, Interval intervalB) =>
    //     intervalA.DistanceInSemitones != intervalB.DistanceInSemitones;
    //
    // public static bool operator >(Interval intervalA, Interval intervalB) =>
    //     intervalA.DistanceInSemitones  > intervalB.DistanceInSemitones;
    //
    // public static bool operator <(Interval intervalA, Interval intervalB) =>
    //     intervalA.DistanceInSemitones < intervalB.DistanceInSemitones;
    //
    // public static bool operator >=(Interval intervalA, Interval intervalB) =>
    //     intervalA.DistanceInSemitones >= intervalB.DistanceInSemitones;
    //
    // public static bool operator <=(Interval intervalA, Interval intervalB) =>
    //     intervalA.DistanceInSemitones <= intervalB.DistanceInSemitones;
    //
    // public static Interval operator +(Interval interval) => interval;
    // public static Interval operator -(Interval interval) => new Interval(-interval.DistanceInSemitones);
    //
    // public static Interval operator +(Interval intervalA, Interval intervalB) =>
    //     new Interval(intervalA.DistanceInSemitones + intervalB.DistanceInSemitones);
    //
    // public static Interval operator -(Interval intervalA, Interval intervalB) =>
    //     new Interval(intervalA.DistanceInSemitones - intervalB.DistanceInSemitones);
    //
    // public static Interval operator *(Interval interval, int multiplier) =>
    //     new Interval(interval.DistanceInSemitones * multiplier);
    //
    // public static Interval operator *(int multiplier, Interval interval) =>
    //     interval * multiplier;
    //
    // #endregion
}