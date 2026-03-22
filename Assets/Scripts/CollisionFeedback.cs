//This is being applied to the ball to make it switch between blue and red when dropped


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