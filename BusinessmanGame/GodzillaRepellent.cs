namespace BusinessmanGame;

public class GodzillaRepellent : Upgrade, IEffect
{
    public override string Information
    {
        get => "Will protect your business from the attack of GodzillaArrival.";
    }
    
    public GodzillaRepellent(string name, int cost) : base(name, cost)
    {
    }

}