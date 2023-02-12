namespace MegaMitch.MusicTheoryLibrary.Notes;

/// <summary>
/// The class that represents a Note in music which is the combination of a Pitch Class and Octave.
/// </summary>
public sealed partial class Note : IComparable<Note>, IEquatable<Note>
{
    private const int NumberOfSemitones = 12;

    private static int ToSemitones(PitchClass pc, Octave octave)
    {
        return (int)pc + (int)octave * NumberOfSemitones;
    }

    // A Note can be represented by the distance in semitones/half steps from a specific Note.
    // In this case Pitch Class C at Octave 0 (Sub Contra) was chosen to make mathematical operations easier.
    private readonly int _semitonesFromC0;

    private Note(int semitonesFromC0)
    {
        _semitonesFromC0 = semitonesFromC0;
    }

    public Note(PitchClass pc, Octave octave) : this(ToSemitones(pc, octave)) { }

    #region Equality

    public override bool Equals(object obj)
    {
        return Equals(obj as Note);
    }

    public override int GetHashCode()
    {
        return _semitonesFromC0;
    }

    public bool Equals(Note? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return _semitonesFromC0 == other._semitonesFromC0;
    }

    public static bool operator ==(Note noteA, Note noteB)
    {
        return Equals(noteA, noteB);
    }
    
    public static bool operator !=(Note noteA, Note noteB)
    {
        return !Equals(noteA, noteB);
    }

    #endregion

    #region Comparisons

    /// <summary>
    /// Compares two <see cref="Note">Notes</see>.
    /// </summary>
    /// <param name="other">Other Note to compare.</param>
    /// <returns>
    ///     A signed number indicating the relative values of this instance and value.<br/>
    ///     <list type="bullet">
    ///         <listheader>Return Value Description</listheader>
    ///         <item>Less than zero: The Note is lower pitch than the other.</item>
    ///         <item>Zero: The Note is the same pitch as the other.</item>
    ///         <item>Greater than zero: The Note is higher pitch than the other.</item>
    ///     </list>
    /// </returns>
    public int CompareTo(Note other)
    {
        // check null
        if (ReferenceEquals(other, null))
        {
            return 1; // all instances are greater than null
        }

        return _semitonesFromC0.CompareTo(other._semitonesFromC0);
    }

    #endregion

    public override string ToString()
    {
        return _semitonesFromC0.ToString();
    }
}