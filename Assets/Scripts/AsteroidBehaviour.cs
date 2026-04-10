using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AsteroidBehaviour : MonoBehaviour
{
    public float moveSpeed = 1.45f;
    public float warningDistance;
    public float VolumeControl;
    [Header("Asteroid")]
    public int health = 5;
    public GameObject asteroidPrefab;  // ✅ drag your asteroid prefab here

    private Transform ship;
    private AudioSource audioSource;
    private Vector3 rotationSpeed;

    public event System.Action OnDestroyed;

    void Start()
    {
        GameObject shipObject = GameObject.FindWithTag("Ship");
        if (shipObject != null) ship = shipObject.transform;
        audioSource = GetComponent<AudioSource>();
        rotationSpeed = new Vector3(
            Random.Range(-50f, 50f),
            Random.Range(-50f, 50f),
            Random.Range(-50f, 50f)
        );
    }

    void Update()
    {
        if (ship == null) return;
        transform.position = Vector3.MoveTowards(
            transform.position,
            ship.position,
            moveSpeed * Time.deltaTime
        );
        transform.Rotate(rotationSpeed * Time.deltaTime);
        float distance = Vector3.Distance(transform.position, ship.position);
        if (distance < warningDistance)
        {
            float volume = (1f - (distance / warningDistance)) * VolumeControl;
            audioSource.volume = volume;
        }
        else
        {
            audioSource.volume = 0f;
        }
    }

    public void TakeHit()
    {
        health--;
        if (health <= 0) DestroyAsteroid();
    }

    void DestroyAsteroid()
    {
        float currentSize = transform.localScale.x;
        if (currentSize > 350f && asteroidPrefab != null)
        {
            SpawnSplit(currentSize);
            SpawnSplit(currentSize);
        }

        OnDestroyed?.Invoke();
        OnDestroyed = null;  // clear it so OnDestroy doesn't fire it again
        Destroy(gameObject);
    }

   void SpawnSplit(float parentSize)
{
    // spawn exactly where parent died
    GameObject chunk = Instantiate(asteroidPrefab, transform.position, Random.rotation);

    float splitSize = (parentSize / 2f) * Random.Range(0.8f, 1.2f);
    chunk.transform.localScale = Vector3.one * splitSize;

    AsteroidBehaviour behaviour = chunk.GetComponent<AsteroidBehaviour>();
    if (behaviour != null)
    {
        behaviour.moveSpeed = moveSpeed * 1.3f;
        behaviour.asteroidPrefab = asteroidPrefab;
    }

    Destroy(chunk, 120f);
}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnDestroy()
    {
        OnDestroyed?.Invoke(); 
    }
}