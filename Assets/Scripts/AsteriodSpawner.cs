using UnityEngine;
using TMPro;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Spawning")]
    public GameObject asteroidPrefab;

    [Header("Random Interval")]
    public float minInterval = 1f;
    public float maxInterval = 4f;

    [Header("Spawn Area")]
    public float spawnRangeX = 5f;
    public float spawnRangeZ = 5f;
    public float spawnHeight = 5f;

    [Header("Warning UI")]
    public TMP_Text warningText;
    public float warningDuration = 2f;

    private float timer = 0f;
    private float nextSpawnTime;
    private int activeAsteroids = 0;  //track live asteroids

    void Start()
    {
        nextSpawnTime = Random.Range(minInterval, maxInterval);
        if (warningText != null) warningText.gameObject.SetActive(false);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= nextSpawnTime)
        {
            if (activeAsteroids == 0)  // only spawn if none alive
            {
                SpawnCube();
            }
            timer = 0f;
            nextSpawnTime = Random.Range(minInterval, maxInterval);
        }
    }

    void SpawnCube()
    {
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        float randomZ = Random.Range(-spawnRangeZ, spawnRangeZ);
        Vector3 spawnPos = new Vector3(randomX, spawnHeight, randomZ);
        Quaternion randomRot = Random.rotation;
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPos, randomRot);
        float randomSize = Random.Range(200f, 500f);
        asteroid.transform.localScale = Vector3.one * randomSize;
        Destroy(asteroid, 120f);

        activeAsteroids++;  // count it
        asteroid.GetComponent<AsteroidBehaviour>().OnDestroyed += () => activeAsteroids--;  //uncount when destroyed

        ShowWarning();
    }

    void ShowWarning()
    {
        if (warningText == null) return;
        StopAllCoroutines();
        StartCoroutine(WarningRoutine());
    }

    System.Collections.IEnumerator WarningRoutine()
    {
        warningText.gameObject.SetActive(true);
        warningText.text = "WARNING: Asteroid Incoming!";
        warningText.fontSize = 72;
        warningText.color = new Color(0.6f, 0f, 0f, 1f);
        yield return new WaitForSeconds(warningDuration);
        warningText.gameObject.SetActive(false);
    }
}