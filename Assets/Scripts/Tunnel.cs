using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class Tunnel : MonoBehaviour
{
    public GameObject exit;
    public GameObject portExit = null;
    public List<GameObject> objectsToSetActive = new List<GameObject>();

    public void SetObjectsActive()
    {
        foreach(GameObject go in objectsToSetActive) {
            go.SetActive(true);

            if (go.GetComponent<Health>() != null) {
                go.GetComponent<Health>().currentHealth = go.GetComponent<Health>().maxHealth;
            }
        }
    }
}
