using System.Collections;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// AsteroidShooter.cs
/// Attach this to the player's dominant hand controller object.
/// 
/// Setup:
///   - Assign 'muzzlePoint'     → empty GameObject at the tip of your weapon
///   - Assign 'lineRenderer'    → LineRenderer component for the laser sight
///   - Assign 'projectilePrefab'→ your bullet/laser bolt prefab (needs Rigidbody)
///   - Assign 'asteroidObject'  → the Asteroid GameObject in your scene
///   - Assign 'holographicTable'→ your HolographicTable script (optional)
///   - Tag your asteroid GameObject with the tag "Asteroid"
/// </summary>
public class AsteroidShooter : MonoBehaviour
{
    [Header("Weapon Settings")]
    public Transform muzzlePoint;
    public GameObject projectilePrefab;
    public float shootSpeed = 50f;
    public float fireRate = 0.4f;           // seconds between shots
    private float nextFireTime = 0f;

    [Header("Laser Sight")]
    public LineRenderer lineRenderer;
    public float laserRange = 200f;

    [Header("Asteroid")]
    public GameObject asteroidObject;
    public int asteroidHealth = 5;
    private bool asteroidDestroyed = false;

    [Header("Holographic Table (optional)")]
    public HologramTable HologramTable;   // remove if not yet implemented

    [Header("Haptics")]
    [Range(0f, 1f)] public float hapticAmplitude = 0.6f;
    public float hapticDuration = 0.1f;

    // Internal
    private InputDevice rightHand;
    private bool triggerWasPressed = false;

    // ─────────────────────────────────────────────────────────────
    void Start()
    {
        // Grab the right-hand XR device
        rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.enabled = true;
        }
    }

    // ─────────────────────────────────────────────────────────────
    void Update()
    {
        if (asteroidDestroyed) 
        {
            if (lineRenderer != null) lineRenderer.enabled = false;
            return;
        }

        // Re-acquire device if not valid yet
        if (!rightHand.isValid)
            rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        UpdateLaserSight();
        HandleShooting();
    }

    // ─────────────────────────────────────────────────────────────
    // LASER SIGHT
    // ─────────────────────────────────────────────────────────────
    void UpdateLaserSight()
    {
        if (lineRenderer == null || muzzlePoint == null) return;

        Ray ray = new Ray(muzzlePoint.position, muzzlePoint.forward);
        Vector3 endPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, laserRange))
        {
            endPoint = hit.point;

            // Highlight laser red when aimed at asteroid
            lineRenderer.startColor = hit.collider.CompareTag("Asteroid")
                ? Color.red : Color.cyan;
            lineRenderer.endColor = lineRenderer.startColor;
        }
        else
        {
            endPoint = muzzlePoint.position + muzzlePoint.forward * laserRange;
            lineRenderer.startColor = Color.cyan;
            lineRenderer.endColor = Color.cyan;
        }

        lineRenderer.SetPosition(0, muzzlePoint.position);
        lineRenderer.SetPosition(1, endPoint);
    }

    // ─────────────────────────────────────────────────────────────
    // SHOOTING INPUT
    // ─────────────────────────────────────────────────────────────
    void HandleShooting()
    {
        rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed);

        // Fire on trigger press (not hold) with fire rate limiting
        if (triggerPressed && !triggerWasPressed && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }

        triggerWasPressed = triggerPressed;
    }

    // ─────────────────────────────────────────────────────────────
    // FIRE
    // ─────────────────────────────────────────────────────────────
    void Fire()
    {
        if (muzzlePoint == null) return;

        // Spawn projectile
        if (projectilePrefab != null)
        {
            GameObject proj = Instantiate(projectilePrefab, muzzlePoint.position, muzzlePoint.rotation);
            Rigidbody rb = proj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.linearVelocity = muzzlePoint.forward * shootSpeed;
            }
            Destroy(proj, 5f); // clean up after 5s
        }

        // Raycast hit check (instant-hit fallback / backup)
        Ray ray = new Ray(muzzlePoint.position, muzzlePoint.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, laserRange))
        {
            if (hit.collider.CompareTag("Asteroid"))
                RegisterHit();
        }

        // Haptic feedback
        TriggerHaptics();
    }

    // ─────────────────────────────────────────────────────────────
    // HIT DETECTION
    // ─────────────────────────────────────────────────────────────
    void RegisterHit()
    {
        asteroidHealth--;
        Debug.Log($"Asteroid hit! Health remaining: {asteroidHealth}");

        // Notify holographic table (e.g. flash or update trajectory display)
        if (holographicTable != null)
            holographicTable.OnAsteroidHit(asteroidHealth);

        if (asteroidHealth <= 0)
            DestroyAsteroid();
    }

    // Called by projectile's OnCollisionEnter if using physical projectiles
    public void OnProjectileHit()
    {
        RegisterHit();
    }

    // ─────────────────────────────────────────────────────────────
    // ASTEROID DESTRUCTION
    // ─────────────────────────────────────────────────────────────
    void DestroyAsteroid()
    {
        asteroidDestroyed = true;
        Debug.Log("Asteroid destroyed! Player wins.");

        if (asteroidObject != null)
        {
            // TODO: swap Destroy() for a particle explosion + audio trigger
            Destroy(asteroidObject);
        }

        if (holographicTable != null)
            holographicTable.OnAsteroidDestroyed();

        // Optional: trigger win state here
        // GameManager.Instance.TriggerWin();
    }

    // ─────────────────────────────────────────────────────────────
    // HAPTICS
    // ─────────────────────────────────────────────────────────────
    void TriggerHaptics()
    {
        if (rightHand.isValid)
            StartCoroutine(HapticPulse());
    }

    IEnumerator HapticPulse()
    {
        rightHand.SendHapticImpulse(0, hapticAmplitude, hapticDuration);
        yield return new WaitForSeconds(hapticDuration);
    }
}