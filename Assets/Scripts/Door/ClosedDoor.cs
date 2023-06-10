using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedDoor : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;
    public Transform openLeftDoor;
    public Transform openRightDoor;
    public float speed = 10.0f;

    private bool unlocked = false;

    void Update() {
        if (unlocked) {
            float moveDistance = speed * Time.deltaTime;

            if (leftDoor.transform.position != openLeftDoor.position) {
                if (Vector3.Distance(leftDoor.transform.position, openLeftDoor.position) < moveDistance)
                    leftDoor.transform.position = openLeftDoor.position;
                else
                    leftDoor.transform.position += (openLeftDoor.position - leftDoor.transform.position).normalized * moveDistance;

                if (Vector3.Distance(rightDoor.transform.position, openRightDoor.position) < moveDistance)
                    rightDoor.transform.position = openRightDoor.position;
                else
                    rightDoor.transform.position += (openRightDoor.position - rightDoor.transform.position).normalized * moveDistance; 
            }


        }
    }

    public void UnLocked() {
        unlocked = true;
    }
}
