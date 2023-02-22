namespace MegaMitch.MusicTheoryLibrary.Notes;

/// <summary>
/// The class that represents a Note in music which is the combination of a Pitch Class and Octave.
/// </summary>
public sealed partial class Note : IComparable<Note>, IEquatable<Note>
{
    private const int SemitonesInOctave = 12;
    private const int MiddleCMidiNumber = 60;
    private const int MidiNumberMin = 0;
    private const int MidiNumberMax = 127;

    private static readonly int A4Semitones = ToSemitones(PitchClass.A, Octave.OneLine);

    private static int ToSemitones(PitchClass pc, Octave octave)
    {
        return (int)pc + (int)octave * SemitonesInOctave;
    }

    // A Note can be represented solely by the distance in semitones/half steps from a specific Note.
    // In this case Pitch Class C at Octave 0 (Sub Contra) was chosen to make mathematical operations easier.
    private readonly int _semitonesFromC0;

    #region Properties

    public PitchClass PitchClass { get; }
    public Octave Octave { get; }

    #endregion

    #region Constructors
    
    private Note(int semitonesFromC0)
    {
        _semitonesFromC0 = semitonesFromC0;
        PitchClass = CalcPitchClass();
        Octave = CalcOctave();
    }

    private Note(PitchClass pc, Octave octave) : this(ToSemitones(pc, octave)) { }

    #endregion

    #region Equality

    public override bool Equals(object obj)
    {
        return Equals(obj as Note);
    }

    public override int GetHashCode()
    {
        return _semitonesFromC0.GetHashCode();
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
    public int CompareTo(Note? other)
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
        return ToName();
    }

    #region DerivedMusicValues
    public int ToMidi(Octave middleCOctave = Octave.OneLine)
    {
        Note middleC = GetNote(PitchClass.C, middleCOctave);
        int differenceFromMiddleC = _semitonesFromC0 - middleC._semitonesFromC0;
        int midiNum = MiddleCMidiNumber + differenceFromMiddleC;

        if (midiNum is < MidiNumberMin or > MidiNumberMax)
        {
            throw new ArgumentException(
                $"Calculated Midi number is out of range based on the middle C octave: {middleCOctave}");
        }

        return midiNum;
    }

    public double ToFrequency(double a4Frequency = 440.0)
    {
        int differenceFromA4 = _semitonesFromC0 - A4Semitones;

        return a4Frequency * Math.Pow(2, (double) differenceFromA4 / SemitonesInOctave);
    }

    public string ToName(bool useSharps = true)
    {
        string name = PitchClass switch
        {
            PitchClass.C => "C",
            PitchClass.CSharp or PitchClass.DFlat => useSharps ? "C#" : "Db",
            PitchClass.D  => "D",
            PitchClass.DSharp or PitchClass.EFlat => useSharps ? "D#" : "Eb",
            PitchClass.E => "E",
            PitchClass.F => "F",
            PitchClass.FSharp or PitchClass.GFlat => useSharps ? "F#" : "Gb",
            PitchClass.G => "G",
            PitchClass.GSharp or PitchClass.AFlat => useSharps ? "G#" : "Ab",
            PitchClass.A => "A",
            PitchClass.ASharp or PitchClass.BFlat => useSharps ? "A#" : "Bb",
            PitchClass.B => "B",
            _ => "",
        };
        name += ((int) Octave).ToString();
        return name;
    }
    
    private PitchClass CalcPitchClass()
    {
        // double modulus to properly handle negative values
        int pcValue = ((_semitonesFromC0 % SemitonesInOctave) + SemitonesInOctave) % SemitonesInOctave;
        return (PitchClass) pcValue;
    }

    private Octave CalcOctave()
    {
        int oct = (_semitonesFromC0 / SemitonesInOctave);
        oct -= _semitonesFromC0 < 0 ? 1 : 0;
        
        return (Octave) oct;
    }

    #endregion
}