namespace BusinessmanGame;

public class IncreaseIncome : Upgrade, IIncomeEffect
{
    public override string Information
    {
        get => "When purchasing this upgrade, your business income increases by 30%.";
    }
    
    public IncreaseIncome(string name, int cost) : base(name, cost)
    {
    }


    public int CalculateIncome(int currentIncome)
    {
        return (int)Math.Round(currentIncome * 1.3);
    }
}