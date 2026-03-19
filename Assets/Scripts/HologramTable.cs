using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class HologramTable : MonoBehaviour {
    [Header("Views")]
    public GameObject planetView;
    public GameObject asteroidView;

    [Header("Asteroid Timer")]
    public TextMeshProUGUI impactTimerText;
    public float secondsUntilImpact = 3600f;

    private bool showingPlanets = true;
    private float countdown;

    void Start() {
        countdown = secondsUntilImpact;
        ShowPlanets();
    }

    void Update() {
        if (Keyboard.current.spaceKey.wasPressedThisFrame) {
            SwitchView();
        }

        if (!showingPlanets) {
            countdown -= Time.deltaTime;
            countdown = Mathf.Max(0, countdown);
            int hours   = (int)(countdown / 3600);
            int minutes = (int)(countdown % 3600 / 60);
            int seconds = (int)(countdown % 60);
            impactTimerText.text = string.Format("IMPACT IN\n{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
        }
    }

    void SwitchView() {
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