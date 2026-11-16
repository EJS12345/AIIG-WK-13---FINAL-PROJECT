using UnityEngine;
using System.Collections.Generic;

public abstract class Labourer : MonoBehaviour, IGoap
{
    public BackpackComponent backpack;
    public float moveSpeed = 3f;

    [Header("Survival Mechanics (Goal 2)")]
    public float hunger = 0f;
    public float maxHunger = 100f;
    public float hungerRate = 5f; // Increases over time

    protected virtual void Start()
    {
        if (backpack == null)
            backpack = gameObject.AddComponent<BackpackComponent>();

        if (backpack.tool == null && !string.IsNullOrEmpty(backpack.toolType))
        {
            GameObject prefab = Resources.Load<GameObject>(backpack.toolType);
            if (prefab != null)
            {
                GameObject tool = Instantiate(prefab, transform.position, transform.rotation);
                backpack.tool = tool;
                tool.transform.parent = transform;
            }
        }
    }

    protected virtual void Update()
    {
        // Increase hunger over time
        hunger += hungerRate * Time.deltaTime;
    }

    public HashSet<KeyValuePair<string, object>> getWorldState()
    {
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();

        worldData.Add(new KeyValuePair<string, object>("hasOre", (backpack.numOre > 0)));
        worldData.Add(new KeyValuePair<string, object>("hasLogs", (backpack.numLogs > 0)));
        worldData.Add(new KeyValuePair<string, object>("hasFirewood", (backpack.numFirewood > 0)));
        worldData.Add(new KeyValuePair<string, object>("hasTool", (backpack.tool != null)));

        // World state flags for Hunger & Food
        worldData.Add(new KeyValuePair<string, object>("isHungry", (hunger >= 50f)));
        worldData.Add(new KeyValuePair<string, object>("isFed", (hunger < 20f)));

        return worldData;
    }

    public abstract HashSet<KeyValuePair<string, object>> createGoalState();

    public void planFailed(HashSet<KeyValuePair<string, object>> failedGoal)
    {
        Debug.LogWarning("<color=orange>Plan Failed for agent:</color> " + gameObject.name);
    }

    public void planFound(HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions)
    {
        Debug.Log("<color=green>Plan found:</color> " + GoapAgent.prettyPrint(actions));
    }

    public void actionsFinished()
    {
        Debug.Log("<color=blue>Actions completed</color>");
    }

    public void planAborted(GoapAction aborter)
    {
        Debug.LogWarning("<color=red>Plan Aborted:</color> " + GoapAgent.prettyPrint(aborter));
    }

    // FIXED: Uses distance threshold instead of exact Vector3.Equals
    public bool moveAgent(GoapAction nextAction)
    {
        if (nextAction.target == null) return false;

        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, nextAction.target.transform.position, step);

        // Threshold check prevents infinite stuck loop
        if (Vector3.Distance(transform.position, nextAction.target.transform.position) < 0.2f)
        {
            transform.position = nextAction.target.transform.position;
            nextAction.setInRange(true);
            return true;
        }

        return false;
    }
}