namespace BusinessmanGame;

public class Player
{
    public int Balance { get; private set; }
    private bool _isReadyToGetIncome;
    public List<Business> OwnBusinesses = new();
    private ReportGenerator _reportGenerator;
    private string _name;
    

    public Player(string name, int balance, ReportGenerator reportGenerator)
    {
        _name = name;
        Balance = balance;
        _isReadyToGetIncome = true;
        _reportGenerator = reportGenerator;
    }

    public bool TryBuyBusiness(Shop shop, int numberOfBusiness)
    {
        var businessPrice = shop.ShowBusinessPrice(numberOfBusiness);
        var isEnoughMoney = Balance >= businessPrice;
        if (isEnoughMoney)
        {
            _isReadyToGetIncome = false;
            var business = shop.SellBusiness(numberOfBusiness);
            OwnBusinesses.Add(business);
            Balance -= businessPrice;
            business.StartBusiness();
            Terminal.PrintBalance(Balance);
            _isReadyToGetIncome = true;
        }

        return isEnoughMoney;
    }

    public bool TryBuyUpgrade(Shop shop, int numberOfUpgrade, int numberOfBusiness)
    {
        var upgradePrice = shop.ShowUpgradePrice(numberOfUpgrade);
        var isEnoughMoney = Balance >= upgradePrice;
        if (isEnoughMoney)
        {
            _isReadyToGetIncome = false;
            var upgrade = shop.GiveUpgrade(numberOfUpgrade) as IEffect;
            Balance -= upgradePrice;
            OwnBusinesses[numberOfBusiness - 1].InstallEffect(upgrade);
            Terminal.PrintBalance(Balance);
            _isReadyToGetIncome = true;
        }

        return isEnoughMoney;
    }

    public bool IsUpgradePresent(Shop shop, int numberOfUpgrade, int numberOfBusiness)
    {
        return OwnBusinesses[numberOfBusiness - 1].Effects.Contains((IEffect)shop.GiveUpgrade(numberOfUpgrade));
    }

    public void ShowOwnBusinesses()
    {
        var report = _reportGenerator.BusinessReport(OwnBusinesses);
        Terminal.DrawSheet(report);
    }

    public void GetIncomes()
    {
        if (!_isReadyToGetIncome)
        {
            return;
        }

        for (var i = 0; i < OwnBusinesses.Count; i++)
        {
            if (OwnBusinesses[i].IsIncomeReady())
            {
                Balance += OwnBusinesses[i].GetFinalIncome();
                Terminal.PrintBalance(Balance);
            }
        }
    }

    public void PayPercent(int percent)
    {
        Balance = (int)Math.Round((double)Balance * (100 - percent) / 100);
    }

    public void ReceiveMoney(int money)
    {
        Balance += money;
        Terminal.PrintBalance(Balance);
    }
}