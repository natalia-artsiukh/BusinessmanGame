namespace BusinessmanGame;

public interface IIntervalEffect : IEffect
{
    TimeSpan CalculateInterval(TimeSpan currentInterval);
}