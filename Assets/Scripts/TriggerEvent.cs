using System.Collections;
using UnityEngine;

public class TriggerEvent : MonoBehaviour {
    public GameObject[] targets;
    public float lowerAmount = 2f;
    public float lowerSpeed = 2;

    private Vector3[] startPositions;

    void Start() {
        startPositions = new Vector3[targets.Length];
        for (int i = 0; i < targets.Length; i++)
            startPositions[i] = targets[i].transform.position;
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Player entered!");
            foreach (GameObject obj in targets) obj.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(LowerObject());
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            StopAllCoroutines();
            StartCoroutine(RaiseObject());
        }
    }

    IEnumerator LowerObject() {
        Vector3[] endPositions = new Vector3[targets.Length];
        for (int i = 0; i < targets.Length; i++)
            endPositions[i] = startPositions[i] - new Vector3(0, lowerAmount, 0);

        bool moving = true;
        while (moving) {
            moving = false;
            for (int i = 0; i < targets.Length; i++) {
                if (targets[i].transform.position.y > endPositions[i].y) {
                    targets[i].transform.position = Vector3.MoveTowards(
                        targets[i].transform.position, endPositions[i], lowerSpeed * Time.deltaTime
                    );
                    moving = true;
                }
            }
            yield return null;
        }
    }

    IEnumerator RaiseObject() {
        bool moving = true;
        while (moving) {
            moving = false;
            for (int i = 0; i < targets.Length; i++) {
                if (targets[i].transform.position.y < startPositions[i].y) {
                    targets[i].transform.position = Vector3.MoveTowards(
                        targets[i].transform.position, startPositions[i], lowerSpeed * Time.deltaTime
                    );
                    moving = true;
                }
            }
            yield return null;
        }
        foreach (GameObject obj in targets) obj.SetActive(false);
    }
}