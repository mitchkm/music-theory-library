using System.Text.RegularExpressions;

namespace MegaMitch.MusicTheoryLibrary.Notes;

// Summary defined in Note.cs file
public sealed partial class Note
{
    #region NoteCache

    // Dictionary for caching notes by semitone; Notes are immutable only one reference per pitch is needed.
    private static Dictionary<int, Note> _notesBySemitones = new Dictionary<int, Note>();

    public static void CacheNotesInSemitonesRange(int minSemitone, int maxSemitone)
    {
        for (int s = minSemitone; s < maxSemitone; s++)
        {
            GetNote(s);
        }
    }

    private static Note GetNote(int semitones)
    {
        if (_notesBySemitones.TryGetValue(semitones, out Note note))
        {
            return note;
        }

        // new semitone, make new note
        Note newNote = new Note(semitones);
        _notesBySemitones[semitones] = newNote;
        return newNote;
    }

    #endregion

    #region Parse

    private static readonly Regex NoteStringNameFormat = new Regex(
        @"^(?<note>[ABCDEFGabcdefg]{1})(?<accidentals>[#|b|x]?)(?<octave>-?\d+)$",
        RegexOptions.IgnoreCase);

    public static Note Parse(string noteName)
    {
        Match match = NoteStringNameFormat.Match(noteName);

        if (!match.Success)
        {
            throw new ArgumentException($"String '{noteName}' does not match Note name format");
        }

        GroupCollection groups = match.Groups;
        string pcStr = groups["note"].Value.ToUpper();
        string accidentals = groups["accidentals"].Value;
        int octaveNum = int.Parse(groups["octave"].Value.ToLower());

        if (!Enum.TryParse(pcStr, out PitchClass pc))
        {
            throw new ArgumentException($"Could not parse Pitch Class `{pcStr}`");
        }

        if (!Enum.IsDefined(typeof(Octave), octaveNum))
        {
            throw new ArgumentException($"Could not parse Octave from value: {octaveNum}");
        }

        int semitones = ToSemitones(pc, (Octave) octaveNum);

        // adjust based on accidentals
        if (!string.IsNullOrWhiteSpace(accidentals))
        {
            foreach (char accidental in accidentals)
            {
                switch (accidental)
                {
                    case '#':
                        semitones += 1;
                        break;
                    case 'b':
                        semitones -= 1;
                        break;
                    case 'x':
                        semitones += 2;
                        break;
                }
            }
        }

        return new Note(semitones);
    }

    public static bool TryParse(string noteName, out Note? note)
    {
        note = default;

        try
        {
            note = Parse(noteName);
        }
        catch (Exception _)
        {
            return false;
        }


        return true;
    }

    #endregion

    /// <summary>
    /// Get Note given a Pitch Class and Octave.
    /// </summary>
    /// <param name="pc">The Pitch Class of the note.</param>
    /// <param name="octave">The Octave of the note.</param>
    /// <returns>Note representing the Pitch Class and Octave.</returns>
    public static Note GetNote(PitchClass pc, Octave octave) => GetNote(ToSemitones(pc, octave));
}