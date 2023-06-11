using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    public List<Transform> points;
    public int targetIndex = 0;
    public GameObject platform;
    public float speed = 10.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveDistance = speed * Time.deltaTime;

        if (Vector3.Distance(platform.transform.position, points[targetIndex].position) < moveDistance) {
            platform.transform.position = points[targetIndex].position;
            targetIndex++;
            if (targetIndex >= points.Count) {
                targetIndex = 0;
            }
        }
        else {
            platform.transform.position += (points[targetIndex].position - platform.transform.position).normalized * moveDistance;
        }
    }
}
