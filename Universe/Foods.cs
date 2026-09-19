namespace Evo.Universe;

public class Foods
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Energy;
    public int age;
    public WorldPosition position;

    public Foods(WorldPosition worldposition)
    {

        Random random = new Random();

        this.Energy = random.Next(50, 100);
        this.age = 0;
        this.position = worldposition;
    }
}
