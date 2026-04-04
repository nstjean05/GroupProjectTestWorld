using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class BinocularsView : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public Camera mainCamera;
    public Camera binocularCamera;
    public Transform cameraRig;

    private bool isActive = false;
    private Quaternion initialRigRotation;

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
    }
}