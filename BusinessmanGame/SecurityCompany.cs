namespace BusinessmanGame;

public class SecurityCompany : Upgrade, IEffect
{
    public override string Information
    {
        get => "Not a single infidel can take away your hard-earned money.";
    }
    
    public SecurityCompany(string name, int cost) : base(name, cost)
    {
    }

}