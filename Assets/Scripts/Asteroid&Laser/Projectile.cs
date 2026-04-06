using UnityEngine;

public class Projectile : MonoBehaviour {

    public AudioClip explosionSound;

    void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Asteroid")) {
            if (explosionSound != null) {
                AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}