namespace Evo.AI;

public class GoapPlanner
{
    public GoapAction? MakePlan(
        Dictionary<string, bool> currentState,
        List<GoapAction> actions,
        Dictionary<string, bool> goal)
    {
        GoapAction? bestAction = null;
        int bestCost = int.MaxValue;

        foreach (GoapAction action in actions)
        {
            bool canUseAction = true;

            foreach (var precondition in action.Preconditions)
            {
                if (!currentState.ContainsKey(precondition.Key))
                {
                    canUseAction = false;
                    break;
                }

                if (currentState[precondition.Key] != precondition.Value)
                {
                    canUseAction = false;
                    break;
                }
            }

            if (!canUseAction)
                continue;

            bool helpsGoal = false;

            foreach (var effect in action.Effects)
            {
                if (goal.ContainsKey(effect.Key) &&
                    goal[effect.Key] == effect.Value)
                {
                    helpsGoal = true;
                    break;
                }
            }

            if (!helpsGoal)
                continue;

            if (action.Cost < bestCost)
            {
                bestCost = action.Cost;
                bestAction = action;
            }
        }

        return bestAction;
    }
}