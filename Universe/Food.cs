using  Evo.Universe;
namespace Evo.Universe;



public class Food
{
    public Guid Id { get; set; }
    public int fuelValue { get; set; }

    public int wasteProduced { get; set; }

    public WorldPositions currentPosition { get; set; }


    //randomly generates a food item
    public static Food CreateRandom(WorldPositions worldPositions)
    {
        ArgumentNullException.ThrowIfNull(worldPositions);

        Random random = new Random();
        int fuelValue = random.Next(5, 21); // Random fuel value between 5 and 20
        int wasteProduced = random.Next(1, 6); // Random waste produced between 1 and 5

        if (worldPositions.OccupyingCreature != null)
        {
            throw new InvalidOperationException("Cannot place food on a position occupied by a creature.");
        }

        Food newFood = new Food
        {
            Id = Guid.NewGuid(),
            fuelValue = fuelValue,
            wasteProduced = wasteProduced,
            currentPosition = worldPositions
        };

        worldPositions.OccupyingFood = newFood;
        return newFood;
    }

    
}