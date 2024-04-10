namespace BusinessmanGame;

public class Gift : Occasion
{
    private Shop _shop;

    public Gift(Player player, Shop shop)
    {
        _player = player;
        _shop = shop;
    }
    public override void Happen()
    {
        var gift = _shop.GiveBusiness();
        _player.OwnBusinesses.Add(gift); 
        Terminal.WriteMessage($"Incredible luck! The mayor of the city confused you with a famous philanthropist and gave you a {gift.Name}.");
    }
}