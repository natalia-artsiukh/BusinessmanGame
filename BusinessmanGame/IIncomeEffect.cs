namespace BusinessmanGame;

public interface IIncomeEffect : IEffect
{
    int CalculateIncome(int currentIncome);
}