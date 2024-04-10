namespace BusinessmanGame;

public class ReduceTime : Upgrade, IIntervalEffect
{
    public override string Information
    {
        get => "When you purchase this upgrade, your waiting time for income from your business will decrease by 25%.";
    }
    
    public ReduceTime(string name, int cost) : base(name, cost)
    {
    }

    public TimeSpan CalculateInterval(TimeSpan currentInterval)
    {
        return currentInterval * 0.75;
    }
}