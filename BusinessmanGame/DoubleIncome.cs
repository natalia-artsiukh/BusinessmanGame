namespace BusinessmanGame;

public class DoubleIncome : Upgrade, IIncomeEffect
{
    public override string Information
    {
        get => "When you purchase this upgrade, you have a 20% chance of getting double business income.";
    }
    
    public DoubleIncome(string name, int cost) : base(name, cost)
    {
    }

    public int CalculateIncome(int currentIncome)
    {
        var chance = Randomizer.Next(0, 10);
        if (chance < 2)
        {
            return currentIncome * 2;
        }

        return currentIncome;
    }
}