using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    public GameObject exit;
    public GameObject portExit = null;
    public List<GameObject> objectsToSetActive = {};

    public void SetObjectsActive()
    {
        foreach(GameObject go in objectsToSetActive) {
            go.SetActive(true);
        }
    }
}
