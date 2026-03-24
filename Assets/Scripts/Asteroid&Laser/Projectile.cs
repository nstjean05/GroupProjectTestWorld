using UnityEngine;

public class Projectile : MonoBehaviour {

    public AudioClip explosionSound;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("asteroidBoss"))
        {
            // Get AsteroidHealth from THIS specific asteroid we hit
           AsteroidBehaviour asteroid = collision.gameObject.GetComponent<AsteroidBehaviour>();
            if (asteroid != null)
            {
                asteroid.TakeHit();
            }
        }
            // Play explosion sound at the position before destroying
            if (explosionSound != null) {
                AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            }
            Destroy(gameObject); // destroy the projectile
        }
 }