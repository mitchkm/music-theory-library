using System.Text.RegularExpressions;

namespace MegaMitch.MusicTheoryLibrary.Notes;

// Summary defined in Note.cs file
public sealed partial class Note
{
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

        int semitones = ToSemitones(pc, (Octave)octaveNum);

        // adjust based on accidentals
        if (!string.IsNullOrWhiteSpace(accidentals))
        {
            foreach (char accidental in accidentals)
            {
                switch (accidental)
                {
                    case '#': semitones += 1;
                        break;
                    case 'b': semitones -= 1;
                        break;
                    case 'x': semitones += 2;
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
}