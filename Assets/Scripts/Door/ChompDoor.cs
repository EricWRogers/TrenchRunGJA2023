using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChompDoor : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;
    public Transform openLeftDoor;
    public Transform openRightDoor;
    public float closingSpeed = 10.0f;
    public float openingSpeed = 10.0f;

    public bool closing = false;
    public Vector3 closedLeftDoorPosition;
    public Vector3 closedRightDoorPosition;

    void Start() {
        closedLeftDoorPosition = leftDoor.transform.position;
        closedRightDoorPosition = rightDoor.transform.position;
    }

    void Update() {
        if (closing) {
            Debug.Log("1");
            float moveDistance = closingSpeed * Time.deltaTime;

            if (leftDoor.transform.position != closedLeftDoorPosition) {
                if (Vector3.Distance(leftDoor.transform.position, closedLeftDoorPosition) < moveDistance)
                    leftDoor.transform.position = closedLeftDoorPosition;
                else
                    leftDoor.transform.position += (closedLeftDoorPosition - leftDoor.transform.position).normalized * moveDistance;
                Debug.Log("2");
                if (Vector3.Distance(rightDoor.transform.position, closedRightDoorPosition) < moveDistance) {
                    rightDoor.transform.position = closedRightDoorPosition;
                }
                else {
                    rightDoor.transform.position += (closedRightDoorPosition - rightDoor.transform.position).normalized * moveDistance;
                }
            } else { closing = false; }
        } else {
            float moveDistance = openingSpeed * Time.deltaTime;

            if (leftDoor.transform.position != openLeftDoor.position) {
                if (Vector3.Distance(leftDoor.transform.position, openLeftDoor.position) < moveDistance)
                    leftDoor.transform.position = openLeftDoor.position;
                else
                    leftDoor.transform.position += (openLeftDoor.position - leftDoor.transform.position).normalized * moveDistance;

                if (Vector3.Distance(rightDoor.transform.position, openRightDoor.position) < moveDistance) {
                    rightDoor.transform.position = openRightDoor.position;
                }
                else {
                    rightDoor.transform.position += (openRightDoor.position - rightDoor.transform.position).normalized * moveDistance;
                } 
            } else { closing = true; }
        }
    }
}
