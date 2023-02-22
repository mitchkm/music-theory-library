using MegaMitch.MusicTheoryLibrary.Notes;

namespace MegaMitch.MusicTheoryLibrary.Test;

public class NoteTests
{
    private readonly Note _c4 = Note.GetNote(PitchClass.C, Octave.OneLine);
    private readonly Note _a4 = Note.GetNote(PitchClass.A, Octave.OneLine);
    private readonly Note _anotherA4 = Note.GetNote(PitchClass.A, Octave.OneLine);

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

    [Test]
    public void ParseTest()
    {
        for (int i = 0; i < 12; i++)
        {
            int temp = i - 12;
            Console.WriteLine($"{temp} % 12 = {(temp % 12) + 12}");
        }

        Console.WriteLine(Note.Parse("Bb-1"));
    }

    [Test]
    public void PropertiesTest()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_a4.PitchClass, Is.EqualTo(PitchClass.A));
            Assert.That(_a4.Octave, Is.EqualTo(Octave.OneLine));
            Assert.That(_c4.PitchClass, Is.EqualTo(PitchClass.C));
            Assert.That(_c4.Octave, Is.EqualTo(Octave.OneLine));
        });
        Note noteDoubleContra = Note.GetNote(PitchClass.D, Octave.DoubleContra);
        Assert.That(noteDoubleContra.PitchClass, Is.EqualTo(PitchClass.D));
        Assert.That(noteDoubleContra.Octave, Is.EqualTo(Octave.DoubleContra));
    }
}