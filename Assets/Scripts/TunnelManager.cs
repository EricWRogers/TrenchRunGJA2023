using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelManager : MonoBehaviour
{
    public GameObject lastTunnel;
    public float distanceToEnd = 200.0f;
    void FixedUpdate()
    {
        Vector3 tempPlayer = GameManager.Instance.GetPlayer().transform.position;
        tempPlayer.x = 0.0f;
        tempPlayer.y = 0.0f;

        Vector3 exit = lastTunnel.GetComponent<Tunnel>().exit.transform.position;
        exit.x = 0.0f;
        exit.y = 0.0f;

        if (Vector3.Distance(tempPlayer, exit) < distanceToEnd) {
            lastTunnel = SuperPupSystems.Helper.SimpleObjectPool.Instance.SpawnFromPool(
                "tunnel",
                lastTunnel.GetComponent<Tunnel>().exit.transform.position,
                Quaternion.identity
            );
        }
    }
}
