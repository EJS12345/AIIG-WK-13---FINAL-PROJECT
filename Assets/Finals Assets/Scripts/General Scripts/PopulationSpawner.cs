using UnityEngine;

public class PopulationSpawner : MonoBehaviour
{
    [Header("Simulation Configuration")]
    public GameObject villagerPrefab;
    public int entityCount = 100; // Requirement: 100 entities minimum
    public Vector2 spawnBounds = new Vector2(40f, 40f);

    void Start()
    {
        if (villagerPrefab == null)
        {
            Debug.LogError("PopulationSpawner: Assign a Villager Prefab in the Inspector!");
            return;
        }

        for (int i = 0; i < entityCount; i++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(-spawnBounds.x / 2f, spawnBounds.x / 2f),
                0f,
                Random.Range(-spawnBounds.y / 2f, spawnBounds.y / 2f)
            );

            GameObject agent = Instantiate(villagerPrefab, randomPos, Quaternion.identity, transform);
            agent.name = "Simulated_Villager_" + (i + 1);
        }

        Debug.Log("<color=green>Successfully spawned " + entityCount + " GOAP entities!</color>");
    }
}