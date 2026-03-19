using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class AsteroidShooter : MonoBehaviour {
    [Header("Shooting")]
    public Transform muzzlePoint;
    public GameObject projectilePrefab;
    public float shootSpeed = 20f;
    public float fireRate = 0.5f;

    [Header("Laser")]
    public LineRenderer lineRenderer;

    [Header("Asteroid")]
    public GameObject asteroidObject;
    public int asteroidHealth = 5;

    private float nextFireTime;
    private int currentHealth;

    void Start() {
        currentHealth = asteroidHealth;
    }

    void Update() {
        UpdateLaser();

        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed);

        if (triggerPressed && Time.time >= nextFireTime) {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void UpdateLaser() {
        if (lineRenderer == null || muzzlePoint == null) return;

        lineRenderer.SetPosition(0, muzzlePoint.position);

        Ray ray = new Ray(muzzlePoint.position, muzzlePoint.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) {
            lineRenderer.SetPosition(1, hit.point);
            lineRenderer.startColor = hit.collider.CompareTag("Asteroid") ? Color.red : Color.cyan;
            lineRenderer.endColor = lineRenderer.startColor;
        } else {
            lineRenderer.SetPosition(1, muzzlePoint.position + muzzlePoint.forward * 100f);
            lineRenderer.startColor = Color.cyan;
            lineRenderer.endColor = Color.cyan;
        }
    }

    void Shoot() {
        // Visual projectile
        if (projectilePrefab != null && muzzlePoint != null) {
            GameObject projectile = Instantiate(projectilePrefab, muzzlePoint.position, muzzlePoint.rotation);
            projectile.GetComponent<Rigidbody>().linearVelocity = muzzlePoint.forward * shootSpeed;
            Destroy(projectile, 5f);
        }

        // Instant raycast hit
        Ray ray = new Ray(muzzlePoint.position, muzzlePoint.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) {
            if (hit.collider.CompareTag("Asteroid")) {
                HitAsteroid();
            }
        }

        // Haptic feedback
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        rightHand.SendHapticImpulse(0, 0.5f, 0.1f);
    }

    void HitAsteroid() {
        currentHealth--;
        Debug.Log("Asteroid hit! Health: " + currentHealth);

        if (currentHealth <= 0) {
            Debug.Log("Asteroid destroyed!");
            if (asteroidObject != null) asteroidObject.SetActive(false);
        }
    }
}