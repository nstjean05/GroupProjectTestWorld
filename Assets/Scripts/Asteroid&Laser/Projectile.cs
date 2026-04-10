using UnityEngine;

public class Projectile : MonoBehaviour
{
    public AudioClip explosionSound;

    void OnCollisionEnter(Collision collision)
    {
        // Handle regular asteroids
        if (collision.collider.CompareTag("Asteroid"))
        {
            if (explosionSound != null)
            {
                AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        // Handle asteroid boss
        else if (collision.collider.CompareTag("asteroidBoss"))
        {
            AsteroidBehaviour asteroid = collision.gameObject.GetComponent<AsteroidBehaviour>();
            if (asteroid != null)
            {
                asteroid.TakeHit();
            }
            Destroy(gameObject);
        }
    }
}