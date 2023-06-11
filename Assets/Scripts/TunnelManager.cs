using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelManager : MonoBehaviour
{
    public GameObject lastTunnel;
    public float distanceToEnd = 200.0f;
    public float courseLength = 1500.0f;

    public List<string> tunnelCodes;
    public string lastTunnelPlaced;
    public string finalCode;
    public string repeatCode;
    public bool endZone;
    public Vector3 initPlayerPos;

    void Start() {
        initPlayerPos = GameManager.Instance.GetPlayer().transform.position;
    }
    void FixedUpdate()
    {
        Vector3 tempPlayer = GameManager.Instance.GetPlayer().transform.position;
        tempPlayer.x = 0.0f;
        tempPlayer.y = 0.0f;

        Vector3 exit = lastTunnel.GetComponent<Tunnel>().exit.transform.position;
        exit.x = 0.0f;
        exit.y = 0.0f;

        if (endZone) {
            GameManager.Instance.GetPlayer().transform.LookAt(
                lastTunnel.GetComponent<Tunnel>().exit.transform.position
            );
        }

        if (Vector3.Distance(tempPlayer, exit) < distanceToEnd) {
            if ((Vector3.Distance(tempPlayer, initPlayerPos) > courseLength) && endZone == false) {
                endZone = true;
            }

            string code = lastTunnelPlaced;

            if (!endZone) {
                while(code == lastTunnelPlaced)
                {
                    code = tunnelCodes[Random.Range(0, tunnelCodes.Count)];
                }
            } else {
                if (code == finalCode)
                    code = repeatCode;
                else
                    code = finalCode;
            }

            lastTunnelPlaced = code;

            if (lastTunnel.GetComponent<Tunnel>().portExit != null) {
                lastTunnel.GetComponent<Tunnel>().portExit.SetActive(false);
            }

            lastTunnel = SuperPupSystems.Helper.SimpleObjectPool.Instance.SpawnFromPool(
                code,
                lastTunnel.GetComponent<Tunnel>().exit.transform.position,
                Quaternion.identity
            ); 

            if (lastTunnel.GetComponent<Tunnel>().portExit != null) {
                lastTunnel.GetComponent<Tunnel>().portExit.SetActive(true);
            }     
        }
    }
}
