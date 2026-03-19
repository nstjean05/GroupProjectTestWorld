using UnityEngine;
public class CollisionFeedback : MonoBehaviour {
    public Renderer rend;
    public Material matA, matB;
    private bool toggle;
    void OnCollisionEnter(Collision col) {
        toggle = !toggle;
        rend.material = toggle ? matB : matA;
    }
}