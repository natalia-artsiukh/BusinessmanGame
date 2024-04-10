namespace BusinessmanGame;

public class ReportGenerator
{


    public List<string[]> BusinessReport(List<Business> businesses)
    {
        var report = new List<string[]>();
        var header = new string[] { "№", "Name", "Income", "Interval", "Cost" };
        report.Add(header);
        for (var i = 0; i < businesses.Count; i++)
        {
            var line = new string[]
            {
                $"{i + 1}",
                businesses[i].Name,
                $"{businesses[i].Income}",
                $"{businesses[i].Interval.Seconds}",
                $"{businesses[i].Cost}"
            };
            report.Add(line);
        }

        return report;
    }
    
    public List<string[]> UpgradeReport(List<Upgrade> upgrades)
    {
        var report = new List<string[]>();
        var header = new string[] { "№", "Name", "Cost" };
        report.Add(header);
        for (var i = 0; i < upgrades.Count; i++)
        {
            var line = new string[]
            {
                $"{i + 1}",
                upgrades[i].Name,
                $"{upgrades[i].Cost}"
            };
            report.Add(line);
        }

        return report;
    }
}