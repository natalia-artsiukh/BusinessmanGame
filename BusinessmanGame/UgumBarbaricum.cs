namespace BusinessmanGame;

public class UgumBarbaricum : ContinuousOccasion, IIncomeEffect
{
    public UgumBarbaricum(Player player)
    {
        _player = player;
    }
    public override TimeSpan Duration => TimeSpan.FromSeconds(90);
    public override void Happen()
    {
        var protectedBusinesses = new List<string>();
        for (var i = 0; i < _player.OwnBusinesses.Count; i++)
        {
            if (_player.OwnBusinesses[i].Effects.Any(e => e is SecurityCompany))
            {
                protectedBusinesses.Add(_player.OwnBusinesses[i].Name);
            }
            else
            {
                _player.OwnBusinesses[i].InstallEffect(this);
            }
        }

        var message =
            "In your city, Genghis Khan and his army set up camp. Now everyone pays him tribute - 15% of profits.";
        if (protectedBusinesses.Count != 0)
        {
            message += $" Only the Magnolia agency has a jarlig and its client{(protectedBusinesses.Count > 1? "s":"")} {string.Join(", ", protectedBusinesses)} {(protectedBusinesses.Count == 1 ? "is":"are")} exempt from paying tribute.";
        }
        Terminal.WriteMessage(message);
    }

    public override void Finalize()
    {
        for (var i = 0; i < _player.OwnBusinesses.Count; i++)
        {
            var genghisKhanEffectIndex = _player.OwnBusinesses[i].Effects.FindIndex(e => e is UgumBarbaricum);
            if (genghisKhanEffectIndex >= 0)
            {
                _player.OwnBusinesses[i].Effects.RemoveAt(genghisKhanEffectIndex);
            }
        }
        Terminal.WriteMessage("The Mongols are running to hide in different parts of the world in order to gather strength to reunite again. In the meantime, businesses can breathe easy.");
    }

    public int CalculateIncome(int currentIncome)
    {
        return (int)Math.Round(currentIncome * 0.85);
    }
}