using UnityEngine;
using UnityEngine.InputSystem;
public class ToggleObject : MonoBehaviour {
    public GameObject target;
    void Update() {
        if (Keyboard.current.tKey.wasPressedThisFrame)
            target.SetActive(!target.activeSelf);
    }
}