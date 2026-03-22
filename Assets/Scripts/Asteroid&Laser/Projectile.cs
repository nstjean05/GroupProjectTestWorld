using UnityEngine;

public class Projectile : MonoBehaviour {

    void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Asteroid")) {
            AsteroidShooter shooter = FindObjectOfType<AsteroidShooter>();
            if (shooter != null) {
                shooter.HitAsteroid();
            }
            Destroy(gameObject);
        }
    }
}