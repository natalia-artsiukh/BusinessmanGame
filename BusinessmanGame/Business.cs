namespace BusinessmanGame;

public class Business
{
    public string Name { get; }
    public int Income { get; }
    public TimeSpan Interval { get; }
    public int Cost { get; }
    public readonly List<IEffect> Effects = new();
    private DateTime _startTime;


    public Business(string name, int income, int interval, int cost)
    {
        Name = name;
        Income = income;
        Interval = TimeSpan.FromSeconds(interval);
        Cost = cost;
    }

    public bool IsIncomeReady()
    {
        if (DateTime.Now - _startTime >= GetFinalInterval())
        {
            _startTime = DateTime.Now;
            return true;
        }

        return false;
    }

    public void InstallEffect(IEffect effect)
    {
        Effects.Add(effect);
    }

    public void StartBusiness()
    {
        _startTime = DateTime.Now;
    }


    public int GetFinalIncome()
    {
        var finalIncome = Income;
        for (var i = 0; i < Effects.Count; i++)
        {
            if (Effects[i] is IIncomeEffect)
            {
                var currentIncomeEffect = Effects[i] as IIncomeEffect;
                finalIncome = currentIncomeEffect.CalculateIncome(finalIncome);
            }
        }

        return finalIncome;
    }

    private TimeSpan GetFinalInterval()
    {
        var finalInterval = Interval;
        for (var i = 0; i < Effects.Count; i++)
        {
            if (Effects[i] is IIntervalEffect)
            {
                var currentIntervalEffect = Effects[i] as IIntervalEffect;
                finalInterval = currentIntervalEffect.CalculateInterval(finalInterval);
            }
        }

        return finalInterval;
    }
}