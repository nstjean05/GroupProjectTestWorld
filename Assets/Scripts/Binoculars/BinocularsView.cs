using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using TMPro;

public class BinocularsView : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public Camera mainCamera;
    public Camera binocularCamera;
    public Transform cameraRig;

    [Header("HUD")]
    public GameObject hudPanel;
    public TextMeshProUGUI planetNameText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI diameterText;
    public LineRenderer hudLine;

    private bool isActive = false;
    private Quaternion initialRigRotation;
    private PlanetData currentPlanet;

    void OnEnable()
    {
        var interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnGrab);
        interactable.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        var interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        interactable.selectEntered.RemoveListener(OnGrab);
        interactable.selectExited.RemoveListener(OnRelease);
    }

    void Start()
    {
        binocularCamera.gameObject.SetActive(false);
        if (hudPanel != null) hudPanel.SetActive(false);
        if (hudLine != null) hudLine.enabled = false;
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        isActive = true;
        initialRigRotation = cameraRig.rotation;
        mainCamera.enabled = false;
        binocularCamera.gameObject.SetActive(true);
    }

    void OnRelease(SelectExitEventArgs args)
    {
        isActive = false;
        mainCamera.enabled = true;
        binocularCamera.gameObject.SetActive(false);
        if (hudPanel != null) hudPanel.SetActive(false);
        if (hudLine != null) hudLine.enabled = false;
    }

    void Update()
    {
        if (!isActive) return;

        Quaternion rotationDelta = cameraRig.rotation * Quaternion.Inverse(initialRigRotation);
        binocularCamera.transform.rotation = rotationDelta * binocularCamera.transform.rotation;
        initialRigRotation = cameraRig.rotation;

        float speed = 60f * Time.deltaTime;
        if (Keyboard.current.leftArrowKey.isPressed)
            binocularCamera.transform.Rotate(Vector3.up, -speed, Space.World);
        if (Keyboard.current.rightArrowKey.isPressed)
            binocularCamera.transform.Rotate(Vector3.up, speed, Space.World);
        if (Keyboard.current.upArrowKey.isPressed)
            binocularCamera.transform.Rotate(Vector3.right, -speed, Space.Self);
        if (Keyboard.current.downArrowKey.isPressed)
            binocularCamera.transform.Rotate(Vector3.right, speed, Space.Self);

        PlanetData closestPlanet = null;
        float closestAngle = 10f;

        foreach (var planet in FindObjectsByType<PlanetData>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
        {
            Vector3 dirToPlanet = (planet.transform.position - binocularCamera.transform.position).normalized;
            float angle = Vector3.Angle(binocularCamera.transform.forward, dirToPlanet);
            if (angle < closestAngle)
            {
                closestAngle = angle;
                closestPlanet = planet;
            }
        }

        if (closestPlanet != null)
            ShowHUD(closestPlanet);
        else
        {
            if (hudPanel != null) hudPanel.SetActive(false);
            if (hudLine != null) hudLine.enabled = false;
        }
    }

    void ShowHUD(PlanetData data)
    {
        if (hudPanel != null) hudPanel.SetActive(true);
        if (planetNameText != null) planetNameText.text = data.planetName;
        if (distanceText != null) distanceText.text = "Distance: " + data.distanceFromSun;
        if (diameterText != null) diameterText.text = "Diameter: " + data.diameter;

        if (hudLine != null)
        {
            hudLine.enabled = true;
            hudLine.SetPosition(0, data.transform.position);
            hudLine.SetPosition(1, hudPanel.transform.position);
        }
    }
}