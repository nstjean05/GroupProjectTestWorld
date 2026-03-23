using UnityEngine;

public class Projectile : MonoBehaviour
{
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
            Destroy(gameObject);
        }
    }
}   