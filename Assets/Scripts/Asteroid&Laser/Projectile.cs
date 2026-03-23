using UnityEngine;

public class Projectile : MonoBehaviour {

    public AudioClip explosionSound;

    void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Asteroid")) {
            AsteroidShooter shooter = FindObjectOfType<AsteroidShooter>();
            if (shooter != null) {
                shooter.HitAsteroid();
            }

            // Play explosion sound at the position before destroying
            if (explosionSound != null) {
                AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            }

            Destroy(collision.gameObject); // destroy the asteroid
            Destroy(gameObject); // destroy the projectile
        }
    }
}