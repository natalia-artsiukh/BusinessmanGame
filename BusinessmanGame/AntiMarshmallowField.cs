namespace BusinessmanGame;

public class AntiMarshmallowField : Upgrade, IEffect
{
    public override string Information
    {
        get => "Will keep your business running even when marshmallows are raining from the sky.";
    }
    
    public AntiMarshmallowField(string name, int cost) : base(name, cost)
    {
    }
}