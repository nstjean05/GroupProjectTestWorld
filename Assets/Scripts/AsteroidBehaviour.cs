using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float warningDistance; // Distance at which sound starts
    public float VolumeControl; // Adjust this to control overall volume

    private Transform ship;
    private AudioSource audioSource;
    private Vector3 rotationSpeed;

    void Start()
    {
        // Find the ship
        GameObject shipObject = GameObject.FindWithTag("Ship");
        if (shipObject != null)
        {
            ship = shipObject.transform;
        }

        // Grab the Audio Source on this asteroid
        audioSource = GetComponent<AudioSource>();

        // Random spin
        rotationSpeed = new Vector3(
            Random.Range(-50f, 50f),
            Random.Range(-50f, 50f),
            Random.Range(-50f, 50f)
        );
    }

    void Update()
    {
        if (ship == null) return;

        // Move toward ship
        transform.position = Vector3.MoveTowards(
            transform.position,
            ship.position,
            moveSpeed * Time.deltaTime
        );

        // Rotate
        transform.Rotate(rotationSpeed * Time.deltaTime);

        // Calculate distance to ship
        float distance = Vector3.Distance(transform.position, ship.position);

        // Map distance to volume (closer = louder)
        if (distance < warningDistance)
        {
            float volume = (1f - (distance / warningDistance)) * VolumeControl; // Scale by VolumeControl
            audioSource.volume = volume;
        }
        else
        {
            audioSource.volume = 0f; // Silent when far away
        }
    }
}