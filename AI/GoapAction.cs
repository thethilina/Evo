namespace Evo.AI;

public class GoapAction
{
    public string Name;

    public Dictionary<string, bool> Preconditions = new();
    public Dictionary<string, bool> Effects = new();

    public int Cost;

    public GoapAction(string name, int cost)
    {
        Name = name;
        Cost = cost;
    }
}
