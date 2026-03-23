using UnityEngine;

public class OrbitScript : MonoBehaviour
{
    public Transform sun;
    public float orbitSpeed = 2f;
    public float orbitRadius = 10f;
    private float orbitAngle;

    void Start()
    {
        orbitAngle = Random.Range(0f, 360f);
    }

    void Update()
    {
        orbitAngle += orbitSpeed * Time.deltaTime;
        float x = sun.position.x + Mathf.Cos(orbitAngle * Mathf.Deg2Rad) * orbitRadius;
        float z = sun.position.z + Mathf.Sin(orbitAngle * Mathf.Deg2Rad) * orbitRadius;
        transform.position = new Vector3(x, sun.position.y, z);
    }
}