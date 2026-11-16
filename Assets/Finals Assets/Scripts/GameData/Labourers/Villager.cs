using UnityEngine;
using System.Collections.Generic;

public class Villager : Labourer
{
    /**
     * DYNAMIC GOAL SELECTION:
     * Goal 1: "collectLogs" -> Gather wood for the village (Work Goal)
     * Goal 2: "isFed"       -> Eat food when hungry (Survival Goal)
     */
    public override HashSet<KeyValuePair<string, object>> createGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();

        // If hunger is high, prioritize survival
        if (hunger >= 50f)
        {
            goal.Add(new KeyValuePair<string, object>("isFed", true));
        }
        else
        {
            // Default goal: Work and collect logs
            goal.Add(new KeyValuePair<string, object>("collectLogs", true));
        }

        return goal;
    }
}