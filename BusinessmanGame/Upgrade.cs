namespace BusinessmanGame;

public abstract class Upgrade
{
    public string Name { get; }
    public int Cost { get; }
    public abstract string Information { get; }

    protected Upgrade(string name, int cost)
    {
        Name = name;
        Cost = cost;
    }

}