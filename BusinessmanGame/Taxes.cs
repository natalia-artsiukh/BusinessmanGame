namespace BusinessmanGame;

public class Taxes : Occasion
{
    public Taxes(Player player) 
    {
        _player = player;
    }

    public override void Happen()
    {
        Terminal.WriteMessage("Pay your taxes and sleep well. 13% of taxes are deducted from your balance to account for the state.");
        _player.PayPercent(13);
    }
}