using UnityEngine;
using System.Collections.Generic;

public class Villager : Labourer
{
    /**
     * DUAL-GOAL GOAP SETUP (Requirement: 2 Goals Minimum)
     * Goal 1 ("collectLogs" / "collectOre"): Fulfills village economic supply chain.
     * Goal 2 ("isFed"): Triggers when hunger >= 50 to force the agent to eat.
     */
    public override HashSet<KeyValuePair<string, object>> createGoalState()
    {
        HashSet<KeyValuePair<string, object>> goals = new HashSet<KeyValuePair<string, object>>();

        if (hunger >= 50f)
        {
            // Goal 2: Survival
            goals.Add(new KeyValuePair<string, object>("isFed", true));
        }
        else
        {
            // Goal 1: Labor/Work
            goals.Add(new KeyValuePair<string, object>("collectLogs", true));
        }

        return goals;
    }
}