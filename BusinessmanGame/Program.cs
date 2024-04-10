namespace BusinessmanGame;

public class Program
{
    public static void Main()
    {
        var reportGenerator = new ReportGenerator();
        var shop = new Shop(reportGenerator);
        var menu = new Menu();
        Terminal.DrawInterface();
        var playerName = Terminal.AskName();
        var level = "";
        var startBalance = 0;
        while (level != "1" && level != "2" && level != "3" )
        {
            level = Terminal.AskQuestion(new string[]{
                "Choose difficulty level:",
                "1. Easy",
                "2. Medium",
                "3. Hard"
            });
            switch (level)
            {
                case "1":
                    startBalance = 1000;
                    break;
                case "2":
                    startBalance = 650;
                    break;
                case "3":
                    startBalance = 250;
                    break;
                default:
                    Terminal.WriteMessage("You have chosen a level that does not exist. Try again.");
                    break;
            }
        }

        var player = new Player(playerName, startBalance, reportGenerator);
        var world = new World(shop, player);
        var startTime = DateTime.Now;
        Terminal.PrintBalance(player.Balance);
       
        var isGameActive = true;
        var incomeThread = new Thread(() =>
        {
            while (true)
            {
                player.GetIncomes();
                world.UpdateOccasions();
                if (player.Balance > 50000)
                {
                    isGameActive = false;
                    Terminal.WriteMessage("You were able to overcome all difficulties and accumulated a substantial fortune. Congratulations on your victory!");
                }

                if (startTime + TimeSpan.FromMinutes(10) < DateTime.Now)
                {
                    isGameActive = false;
                    Terminal.WriteMessage("Various adversities never allowed you to taste the sweet life. Alas, you lost.");
                }
                
                if (!isGameActive)
                    break;
            }
        });
        incomeThread.Start();

        while (isGameActive)
        {
            switch (menu.CurrentMenu)
            {
                case Screens.Main:
                    menu.GoToMain();
                    break;
                case Screens.BusinessesOverview:
                    menu.GoToBusinessesOverview(shop);
                    break;
                case Screens.BuyBusiness:
                    menu.GoToBuyBusiness(shop, player);
                    break;
                case Screens.UpgradesOverview:
                    menu.GoToUpgradesOverview(shop);
                    break;
                case Screens.InspectUpgrade:
                    menu.GoToInspectUpgrade(shop, player);
                    break;
                case Screens.MyBusinesses:
                    menu.GoToMyBusiness(player);
                    break;
                case Screens.Quit:
                default:
                    isGameActive = false;
                    break;
            }
        }

        
    }
}