using UnityEngine;
using System.Collections;

public class TargetManager : MonoBehaviour 
{
    public GameObject[] prefabs; 
    public Transform[] spawnPoints; 
    
    // Time range for random respawning
    public float minRespawnTime = 1f;
    public float maxRespawnTime = 10f;

    void Start() 
    {
        // Each spawn point starts its own independent cycle
        foreach (Transform p in spawnPoints) StartCoroutine(SpawnRoutine(p));
    }

    IEnumerator SpawnRoutine(Transform p) 
    {
        while (true) 
        {
            // Wait for a random interval before the next target appears
            float randomWait = Random.Range(minRespawnTime, maxRespawnTime);
            yield return new WaitForSeconds(randomWait);

            // Spawn only if the game session is currently active
            if (GameManager.instance != null && GameManager.instance.IsGameActive()) 
            {
                // Instantiate a random character (enemy or innocent)
                GameObject t = Instantiate(prefabs[Random.Range(0, prefabs.Length)], p.position, p.rotation);
                
                // Keep the location occupied until the target object is destroyed
                while (t != null) yield return null; 
            }
        }
    }
}
