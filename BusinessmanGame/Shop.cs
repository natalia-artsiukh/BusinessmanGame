using System.Runtime.InteropServices.JavaScript;

namespace BusinessmanGame;

public class Shop
{
    public int NumberOfBusinesses => _businesses.Count;
    public int NumberOfUpgrades => _upgrades.Count;
    private List<Business> _businesses = new();
    private List<Upgrade> _upgrades = new();
    private ReportGenerator _reportGenerator;

    public Shop(ReportGenerator reportGenerator)
    {
        _reportGenerator = reportGenerator;
        initBusinesses();
        initUpgrades();
    }

    public void ShowBusinesses()
    {
        var report = _reportGenerator.BusinessReport(_businesses);
        Terminal.DrawSheet(report);
    }

    public void ShowUpgrades()
    {
        var report = _reportGenerator.UpgradeReport(_upgrades);
        Terminal.DrawSheet(report);
    }

    public int ShowBusinessPrice(int numberOfBusiness)
    {
        return _businesses[numberOfBusiness-1].Cost;
    }

    public int ShowUpgradePrice(int numberOfUpgrade)
    {
        return _upgrades[numberOfUpgrade-1].Cost;
    }

    public Business SellBusiness(int serialNumber)
    {
        var soldBusiness = _businesses[serialNumber - 1];
        _businesses.RemoveAt(serialNumber - 1);
        return soldBusiness;
    }

    public Business GiveBusiness()
    {
        var cheap = _businesses[0];
        var index = 0;
        for (var i = 1; i < _businesses.Count; i++)
        {
            if (_businesses[i].Cost < cheap.Cost)
            {
                cheap = _businesses[i];
                index = i;
            }
        }
        _businesses.RemoveAt(index);
        return cheap;
    }

    public Upgrade GiveUpgrade(int serialNumber)
    {
        return _upgrades[serialNumber - 1];
    }

    public void ShowUpgrade(int upgradeIndex)
    {
        Terminal.WriteMessage(
            $"{_upgrades[upgradeIndex-1].Name}: {_upgrades[upgradeIndex-1].Information}");
    }

    private void initBusinesses()
    {
        _businesses.Add(new Business("Cinema", 100, 10, 1000));
        _businesses.Add(new Business("Cafe", 50, 5, 500));
        _businesses.Add(new Business("Bar", 60, 6, 600));
        _businesses.Add(new Business("Car showroom", 1000, 30, 10000));
        _businesses.Add(new Business("Resort", 500, 20, 5000));
        _businesses.Add(new Business("Grocery", 10, 2, 100));
        _businesses.Add(new Business("Book shop", 15, 5, 150));
        _businesses.Add(new Business("Bowling", 40, 4, 400));
    }

    private void initUpgrades()
    {
        _upgrades.Add(new IncreaseIncome("Increase income", 1000));
        _upgrades.Add(new ReduceTime("Reduce income waiting time", 800));
        _upgrades.Add(new DoubleIncome("Chance of double income", 500));
        _upgrades.Add(new GodzillaRepellent("GodzillaArrival repellent", 5000));
        _upgrades.Add(new AntiMarshmallowField("Anti-marshmallow field", 3000));
        _upgrades.Add(new SecurityCompany("Security company \"Magnolia\"", 4000));
    }
}