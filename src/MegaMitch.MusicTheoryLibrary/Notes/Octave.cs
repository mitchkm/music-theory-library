namespace MegaMitch.MusicTheoryLibrary.Notes;

/// <summary>
/// Enum representing the Octaves in music ranging from [-1, 9].
/// Source: <a href="https://en.wikipedia.org/wiki/Octave">https://en.wikipedia.org/wiki/Octave</a><br/>
/// <list type="bullet">
///     <listheader>
///         Octave Names:
///     </listheader>
///     <item>[-1] Double Contra</item>
///     <item>[0] Sub Contra</item>
///     <item>[1] Contra</item>
///     <item>[2] Great</item>
///     <item>[3] Small</item>
///     <item>[4] One Line</item>
///     <item>[5] Two Line</item>
///     <item>[6] Three Line</item>
///     <item>[7] Four Line</item>
///     <item>[8] Five Line</item>
///     <item>[9] Six Line</item>
/// </list>
/// </summary>
public enum Octave
{
    DoubleContra = -1,
    SubContra = 0,
    Contra = 1,
    Great = 2,
    Small = 3,
    OneLine = 4,
    TwoLine = 5,
    ThreeLine = 6,
    FourLine = 7,
    FiveLine = 8,
    SixLine = 9,
}