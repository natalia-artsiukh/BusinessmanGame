namespace BusinessmanGame;

public class Inheritance : Occasion
{
    public Inheritance(Player player)
    {
        _player = player;
    }
    public override void Happen()
    {
        Terminal.WriteMessage("The great-aunt of your grandfather’s great-nephew’s sister died suddenly and left you an inheritance of 1000.");
        _player.ReceiveMoney(1000);
    }
   
}