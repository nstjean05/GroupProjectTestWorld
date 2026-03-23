using UnityEngine;

public class OrbitPlanet : MonoBehaviour {
    public Transform center;
    public float speed = 30f;
    public float radius = 2f;

    private float angle;

    void Start() {
        angle = Random.Range(0f, 360f); // each planet starts at a different point
    }

    void Update() {
        angle += speed * Time.deltaTime;
        float rad = angle * Mathf.Deg2Rad;
        transform.position = center.position + new Vector3(Mathf.Cos(rad) * radius, 0, Mathf.Sin(rad) * radius);
    }
}