namespace BusinessmanGame;

public class GodzillaArrival : Occasion
{
    public GodzillaArrival(Player player)
    {
        _player = player;
    }
    public override void Happen()
    {
        var message = "Godzilla has emerged from the depths of the sea and is destroying the city. ";
        if (_player.OwnBusinesses.Count == 0) return;
        var godzillasTargetIndex = Randomizer.Next(0, _player.OwnBusinesses.Count);
        var godzillasTarget = _player.OwnBusinesses[godzillasTargetIndex];
        if (godzillasTarget.Effects.Any(e => e is GodzillaRepellent))
        {
            message +=
                $"The houses around you are collapsing, but Godzilla's Repellent did a great job and your {godzillasTarget.Name} is safe.";
        }
        else
        {
            _player.OwnBusinesses.RemoveAt(godzillasTargetIndex);
            message += $"Now, in the place of your {godzillasTarget.Name}, there is only a trace of a huge paw.";
        }
        Terminal.WriteMessage(message);
    }
}