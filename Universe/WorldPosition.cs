namespace Evo.Universe;

public class WorldPosition
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int X;
    public int Y;

    public Creature? currentOccCreature;
    public Foods? CurrentOccFood;

    public WorldPosition(int X , int Y)
    {

        this.X = X;
        this.Y = Y;
    }

}
