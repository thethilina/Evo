using Evo.Universe;
namespace Evo.Universe;

public class WorldPositions
{
    public Guid Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public Creature OccupyingCreature { get; set; }
    public Food OccupyingFood { get; set; }

    public WorldPositions(int x, int y)
    {
        Id = Guid.NewGuid();
        X = x;
        Y = y;
        OccupyingCreature = null;
    }

 

}
