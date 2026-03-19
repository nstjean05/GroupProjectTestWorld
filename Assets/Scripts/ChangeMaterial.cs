using UnityEngine;
using UnityEngine.InputSystem;
public class ChangeMaterial : MonoBehaviour {
    public Renderer rend;
    public Material matA, matB;
    private bool toggle;
    void Update() {
        if (Keyboard.current.cKey.wasPressedThisFrame) {
            toggle = !toggle;
            rend.material = toggle ? matB : matA;
        }
    }
}