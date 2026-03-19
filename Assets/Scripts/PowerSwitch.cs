using UnityEngine;
public class PowerSwitch : MonoBehaviour {
    public GameObject risingObject;
    public float riseHeight = 3f;
    public float riseSpeed = 2f;
    private bool rising = false;
    private Vector3 targetPos;

    public void OnPowered() {
        rising = true;
        targetPos = risingObject.transform.position + new Vector3(0, riseHeight, 0);
        risingObject.SetActive(true);
    }

    void Update() {
        if (rising) {
            risingObject.transform.position = Vector3.MoveTowards(
                risingObject.transform.position, 
                targetPos, 
                riseSpeed * Time.deltaTime
            );
            if (risingObject.transform.position == targetPos)
                rising = false;
        }
    }
}