using UnityEngine;

public class SolarSystemSetup : MonoBehaviour
{
    [Header("Assign Your Objects")]
    public GameObject sun;
    public GameObject mercury;
    public GameObject venus;
    public GameObject earth;
    public GameObject mars;
    public GameObject jupiter;
    public GameObject saturn;
    public GameObject uranus;
    public GameObject neptune;

    [Header("Ship Reference")]
    public GameObject ship;

    [Header("Solar System Settings")]
    public float systemScale = 1f;

    // Circular distances from sun
    private float[] distances = { 8f, 12f, 16f, 22f, 32f, 42f, 52f, 62f };

    // Slow orbit speeds
    private float[] speeds = { 2.5f, 2.0f, 1.8f, 1.5f, 1.0f, 0.8f, 0.6f, 0.4f };

    // Relative size multipliers (applied on top of ship size x30)
    private float[] sizeMultipliers = { 0.4f, 0.6f, 0.65f, 0.4f, 2.5f, 2.2f, 1.8f, 1.7f };

    void Start()
    {
        // Get ship size
        float shipSize = ship != null ? ship.transform.localScale.magnitude : 1f;
        float planetBaseSize = shipSize * 30f;

        // Get ship position as center — ship is NOT moved
        Vector3 center = ship != null ? ship.transform.position : Vector3.zero;

        // Place sun near the ship
        float sunSize = planetBaseSize * 2f;
        sun.transform.position = center + new Vector3(sunSize * 2f, 0f, sunSize * 2f);
        sun.transform.localScale = Vector3.one * (planetBaseSize * 2f); // sun bigger than planets

        GameObject[] planets = { mercury, venus, earth, mars,
                                  jupiter, saturn, uranus, neptune };

        for (int i = 0; i < planets.Length; i++)
        {
            if (planets[i] == null) continue;

            // Scale each planet 30x ship size with its own multiplier
            planets[i].transform.localScale = Vector3.one * (planetBaseSize * sizeMultipliers[i]);

            // Remove any existing OrbitScript first
            OrbitScript existing = planets[i].GetComponent<OrbitScript>();
            if (existing != null) Destroy(existing);

            OrbitScript orbit = planets[i].AddComponent<OrbitScript>();
            orbit.sun = sun.transform;
            orbit.orbitRadius = distances[i] * systemScale * planetBaseSize;
            orbit.orbitSpeed = speeds[i];
        }
    }
}