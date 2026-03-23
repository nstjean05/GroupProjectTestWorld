using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Spawning")]
    public GameObject asteroidPrefab;        

    [Header("Random Interval")]
    public float minInterval = 1f;       // Minimum seconds between spawns
    public float maxInterval = 4f;       // Maximum seconds between spawns

    [Header("Spawn Area")]
    public float spawnRangeX = 5f;       // How wide the spawn zone is
    public float spawnRangeZ = 5f;       // How deep the spawn zone is
    public float spawnHeight = 5f;       // How high up cubes appear

    private float timer = 0f;
    private float nextSpawnTime;         

    void Start()
    {
        // Pick the first random interval right away
        nextSpawnTime = Random.Range(minInterval, maxInterval);
    }

    void Update()
    {
        timer += Time.deltaTime;         // Count up every frame

        if (timer >= nextSpawnTime)
        {
            SpawnCube();
            timer = 0f;                  // Reset the clock
            nextSpawnTime = Random.Range(minInterval, maxInterval);
        }
    }

    void SpawnCube()
    {
        // Pick a random position in the spawn zone
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        float randomZ = Random.Range(-spawnRangeZ, spawnRangeZ);
        Vector3 spawnPos = new Vector3(randomX, spawnHeight, randomZ);

        // Pick a random rotation
        Quaternion randomRot = Random.rotation;
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPos, randomRot);

        // Random size 
        float randomSize = Random.Range(50f, 100f);
        asteroid.transform.localScale = Vector3.one * randomSize;

        Destroy(asteroid, 120f);
    }
}