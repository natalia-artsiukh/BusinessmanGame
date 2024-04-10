namespace BusinessmanGame;

public class Menu
{
    public Screens CurrentMenu { get; private set; } = Screens.BusinessesOverview;

    public void GoToMain()
    {
        var userChoice = Terminal.AskQuestion(new string[]
        {
            "What do you want next?",
            "1. Buy business",
            "2. Buy upgrade",
            "3. My businesses",
            "4. Quit"
        });
        switch (userChoice)
        {
            case "1":
                CurrentMenu = Screens.BusinessesOverview;
                break;
            case "2":
                CurrentMenu = Screens.UpgradesOverview;
                break;
            case "3":
                CurrentMenu = Screens.MyBusinesses;
                break;
            case "4":
                CurrentMenu = Screens.Quit;
                break;
        }
    }

    public void GoToBusinessesOverview(Shop shop)
    {
        shop.ShowBusinesses();
        var userChoice = Terminal.AskQuestion(new string[]
        {
            "What do you want next?",
            "1. Buy",
            "2. Quit"
        });
        switch (userChoice)
        {
            case "1":
                CurrentMenu = Screens.BuyBusiness;
                break;
            case "2":
                CurrentMenu = Screens.Main;
                break;
        }
    }

    public void GoToBuyBusiness(Shop shop, Player player)
    {
        var userChoice = Terminal.AskQuestion(new string[]
        {
            "Enter the business number or press other key to go back."
        });
        var isChoiceANumber = int.TryParse(userChoice, out var number);
        if (isChoiceANumber && number >= 1 && number <= shop.NumberOfBusinesses)
        {
            var isDealSuccessful = player.TryBuyBusiness(shop, number);
            if (!isDealSuccessful)
            {
                Terminal.WriteMessage("You have no enough money.");
            }
        }

        CurrentMenu = Screens.BusinessesOverview;
    }

    public void GoToMyBusiness(Player player)
    {
        player.ShowOwnBusinesses();
        Terminal.AskQuestion(new string[]
        {
            "Press any key to go back."
        });
        CurrentMenu = Screens.Main;
    }

    public void GoToUpgradesOverview(Shop shop)
    {
        shop.ShowUpgrades();
        var userChoice = Terminal.AskQuestion(new string[]
        {
            "What do you want next?",
            "1. Inspect",
            "2. Quit"
        });
        switch (userChoice)
        {
            case "1":
                CurrentMenu = Screens.InspectUpgrade;
                break;
            case "2":
                CurrentMenu = Screens.Main;
                break;
        }
    }

    public void GoToInspectUpgrade(Shop shop, Player player)
    {
        shop.ShowUpgrades();
        var selectedUpgrade = Terminal.AskQuestion(new string[]
        {
            "Enter the upgrade number or press other key to go back.",
        });
        var isSelectedANumber = int.TryParse(selectedUpgrade, out var number);
        if (isSelectedANumber && number >= 1 && number <= shop.NumberOfUpgrades)
        {
            shop.ShowUpgrade(number);
            var userChoice = Terminal.AskQuestion(new string[]
            {
                "What do you want next?",
                "1. Buy",
                "2. Quit"
            });
            switch (userChoice)
            {
                case "1":
                    GoToBusinessUpgrade(shop, player, number);
                    break;
                case "2":
                    return;
            }
        }
        else
        {
            CurrentMenu = Screens.UpgradesOverview;
        }
    }

    private void GoToBusinessUpgrade(Shop shop, Player player, int numberOfUpgrade)
    {
        player.ShowOwnBusinesses();
        var selectedBusiness = Terminal.AskQuestion(new string[]
        {
            "Enter the number of business which you want to upgrade.",
        });
        var isSelectedANumber = int.TryParse(selectedBusiness, out var numberOfBusiness);
        if (isSelectedANumber && numberOfBusiness >= 1 && numberOfBusiness <= player.OwnBusinesses.Count)
        {
            if (!player.IsUpgradePresent(shop, numberOfUpgrade, numberOfBusiness))
            {
                var isDealSuccessful = player.TryBuyUpgrade(shop, numberOfUpgrade, numberOfBusiness);
                if (!isDealSuccessful)
                {
                    Terminal.WriteMessage("You have no enough money.");
                } 
            }
            else
            {
                Terminal.WriteMessage("The upgrade has already been installed on this business.");
            }
        }
        else
        {
            CurrentMenu = Screens.InspectUpgrade;
        }
    }
}