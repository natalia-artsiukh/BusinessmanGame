namespace BusinessmanGame;

public class World
{
    private List<Occasion> _occasions = new();
    private Shop _shop;
    private Player _player;
    private DateTime _lastOccasionTime;
    private ContinuousOccasion _underwayOccasion;

    public World(Shop shop, Player player)
    {
        _shop = shop;
        _player = player;
        _lastOccasionTime = DateTime.Now;
        InitOccasions();
    }
    public void UpdateOccasions()
    {
        if (_underwayOccasion == null)
        {
            var randomInterval = Randomizer.Next(40, 80);
            var isOccasionReady = DateTime.Now >= _lastOccasionTime + TimeSpan.FromSeconds(randomInterval);

            if (isOccasionReady)
            {
                if (_occasions[0] is ContinuousOccasion)
                {
                    _underwayOccasion = _occasions[0] as ContinuousOccasion;
                }

                _occasions[0].Happen();
                _occasions.RemoveAt(0);
                if (_occasions.Count == 0)
                {
                    InitOccasions();
                }
                _lastOccasionTime = DateTime.Now;
            }
        }
        else
        {
            var isOccasionReady = DateTime.Now >= _lastOccasionTime + _underwayOccasion.Duration;
            if (isOccasionReady)
            {
                _underwayOccasion.Finalize();
                _underwayOccasion = null;
                _lastOccasionTime = DateTime.Now;
            }
        }
    }
    private void InitOccasions()
    {
        _occasions.Add(new Taxes(_player));
        _occasions.Add(new GodzillaArrival(_player));
        _occasions.Add(new UgumBarbaricum(_player));
        _occasions.Add(new MarshmallowMan(_player));
        _occasions.Add(new Inheritance(_player));
        _occasions.Add(new Gift(_player, _shop));
        Randomizer.Shuffle(_occasions);
    }
}