using MegaMitch.MusicTheoryLibrary.Notes;

namespace MegaMitch.MusicTheoryLibrary.Test;

public class NoteTests
{
    private readonly Note _c4 = new Note(PitchClass.C, Octave.OneLine);
    private readonly Note _a4 = new Note(PitchClass.A, Octave.OneLine);
    private readonly Note _anotherA4 = new Note(PitchClass.A, Octave.OneLine);
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void EqualityTest()
    {
        Assert.AreEqual(_a4, _a4);
        Assert.AreEqual(_a4, _anotherA4);
        Assert.AreEqual(_anotherA4, _a4);
        
        Assert.AreNotEqual(_a4, null);
        Assert.AreNotEqual(_a4, _c4);
        Assert.AreNotEqual(_c4, _a4);
    }
}