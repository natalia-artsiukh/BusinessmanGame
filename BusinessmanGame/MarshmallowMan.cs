namespace BusinessmanGame;

public class MarshmallowMan : ContinuousOccasion, IIntervalEffect
{
    public override TimeSpan Duration => TimeSpan.FromSeconds(30);

    public MarshmallowMan(Player player)
    {
        _player = player;
    }
    public override void Happen()
    {
        var protectedBusinesses = new List<string>();
        for (var i = 0; i < _player.OwnBusinesses.Count; i++)
        {
            if (_player.OwnBusinesses[i].Effects.Any(e => e is AntiMarshmallowField))
            {
                protectedBusinesses.Add(_player.OwnBusinesses[i].Name);
            }
            else
            {
                _player.OwnBusinesses[i].InstallEffect(this);
            }
        }

        var message =
            "The brave ghost hunters destroyed the Marshmallow Man. Alas, the heroes did not clean up after themselves and piles of sticky marshmallows covered all surfaces in the city, which is why all life in the city came to a standstill.";
        if (protectedBusinesses.Count != 0)
        {
            message += $" The anti-marshmallow field has protected you and your {string.Join(", ", protectedBusinesses)} continue{(protectedBusinesses.Count == 1 ? "s":"")} to generate income.";
        }
        Terminal.WriteMessage(message);
    }
    
    public TimeSpan CalculateInterval(TimeSpan currentInterval)
    {
        return currentInterval * 3;
    }
    
    public override void Finalize()
    {
        for (var i = 0; i < _player.OwnBusinesses.Count; i++)
        {
            var marshmallowManEffectIndex = _player.OwnBusinesses[i].Effects.FindIndex(e => e is MarshmallowMan);
            if (marshmallowManEffectIndex >= 0)
            {
                _player.OwnBusinesses[i].Effects.RemoveAt(marshmallowManEffectIndex);
            }
        }
        Terminal.WriteMessage("The marshmallow harvest in the city has ended. Businesses are operating as usual.");
    }
}