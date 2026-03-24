using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class AsteroidShooter : MonoBehaviour {
    [Header("Shooting")]
    public Transform muzzlePoint;
    public GameObject projectilePrefab;
    public float shootSpeed = 20f;
    public float fireRate = 0.5f;
    public AudioClip shootSound;

    [Header("Laser")]
    public LineRenderer lineRenderer;

    [Header("Glow")]
    public Light laserGlow;

    private float nextFireTime;
    
    void Update() {
        UpdateLaser();

        if (Keyboard.current.fKey.wasPressedThisFrame) {
            Shoot();
        }

        UnityEngine.XR.InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        rightHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out bool triggerPressed);

        if (triggerPressed && Time.time >= nextFireTime) {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void UpdateLaser() {
        if (lineRenderer == null || muzzlePoint == null) return;

        lineRenderer.SetPosition(0, muzzlePoint.position);

        Color laserColor;
        Ray ray = new Ray(muzzlePoint.position, muzzlePoint.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) {
            lineRenderer.SetPosition(1, hit.point);
            laserColor = hit.collider.CompareTag("asteroidBoss") ? Color.red : Color.cyan;
        } else {
            lineRenderer.SetPosition(1, muzzlePoint.position + muzzlePoint.forward * 100f);
            laserColor = Color.cyan;
        }

        lineRenderer.startColor = laserColor;
        lineRenderer.endColor = laserColor;
        lineRenderer.material.SetColor("_TintColor", laserColor);

        if (laserGlow != null) {
            laserGlow.color = laserColor;
        }
    }

    void Shoot() {
	if (shootSound != null) GetComponent<AudioSource>().PlayOneShot(shootSound);
        if (projectilePrefab != null && muzzlePoint != null) {
            GameObject projectile = Instantiate(projectilePrefab, muzzlePoint.position, muzzlePoint.rotation);
            projectile.GetComponent<Rigidbody>().linearVelocity = muzzlePoint.forward * shootSpeed;
            Destroy(projectile, 5f);
        }

        UnityEngine.XR.InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        rightHand.SendHapticImpulse(0, 0.5f, 0.1f);
    }
}