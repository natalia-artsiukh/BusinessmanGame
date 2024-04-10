namespace BusinessmanGame;

public abstract class ContinuousOccasion : Occasion
{
    public abstract TimeSpan Duration { get; }
    public abstract void Finalize();
}