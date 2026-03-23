using UnityEngine;
using TMPro;

public class HologramTable : MonoBehaviour {
    [Header("Views")]
    public GameObject planetView;
    public GameObject asteroidView;

    [Header("Asteroid Timer")]
    public TextMeshPro impactTimerText;
    public float secondsUntilImpact = 600f;

    [Header("Solar System")]
    public Transform orbitCenter;
    public SolarBody[] solarBodies;

    [Header("Orbit Settings")]
    public float startingRadius = 1f;
    public float radiusStep = 0.5f;

    private bool showingPlanets = true;
    private float countdown;

    [System.Serializable]
    public class SolarBody {
        public string name;
        public GameObject sceneObject;
        public float orbitSpeed = 30f;
    }

    void Start() {
        countdown = secondsUntilImpact;
        SetupPlanets();
        ShowPlanets();
    }

    void SetupPlanets() {
        for (int i = 0; i < solarBodies.Length; i++) {
            if (solarBodies[i].sceneObject == null) continue;

            if (i == 0) {
                solarBodies[i].sceneObject.transform.position = orbitCenter != null ?
                    orbitCenter.position : planetView.transform.position;
                continue;
            }

            float radius = startingRadius + (i - 1) * radiusStep;

            OrbitPlanet orbit = solarBodies[i].sceneObject.GetComponent<OrbitPlanet>();
            if (orbit == null) orbit = solarBodies[i].sceneObject.AddComponent<OrbitPlanet>();

            orbit.center = orbitCenter != null ? orbitCenter : planetView.transform;
            orbit.speed = solarBodies[i].orbitSpeed;
            orbit.radius = radius;
        }
    }

    void Update() {
        // Always count down regardless of view
        countdown -= Time.deltaTime;
        countdown = Mathf.Max(0, countdown);
        int hours   = (int)(countdown / 3600);
        int minutes = (int)(countdown % 3600 / 60);
        int seconds = (int)(countdown % 60);
        if (impactTimerText != null)
            impactTimerText.text = string.Format("IMPACT IN\n{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
    }

    public void SwitchView() {
        showingPlanets = !showingPlanets;
        if (showingPlanets) ShowPlanets();
        else ShowAsteroid();
    }

    void ShowPlanets() {
        planetView.SetActive(true);
        asteroidView.SetActive(false);
    }

    void ShowAsteroid() {
        planetView.SetActive(false);
        asteroidView.SetActive(true);
    }
}