using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    public float reticalDistance = 50f;
    public Vector2 reticalBounds;

    private GameObject retical;
    private GameObject subReticals;

    private Vector3 reticalPos;
    private Vector2 reticalLocal;


    private void Start()
    {
        retical = InGameUIManager.Instance.targetingRetical;
        subReticals = InGameUIManager.Instance.subTargetingRetical;
        //retical.transform.parent = GameManager.Instance.vCam.transform;
    }

    private void FixedUpdate()
    {
        MoveRetical();
    }


    public void MoveRetical()
    {
        Vector2 look = GetComponent<PlayerController>().GetLookInput();

        look = new Vector2(-look.x, look.y);

        reticalPos = GameManager.Instance.vCam.transform.position + (GameManager.Instance.vCam.transform.forward.normalized * reticalDistance);
        reticalLocal += look;
        reticalLocal = new Vector2(Mathf.Clamp(reticalLocal.x, -reticalBounds.x, reticalBounds.x), Mathf.Clamp(reticalLocal.y, -reticalBounds.y, reticalBounds.y));
        
        reticalPos +=  (retical.transform.right.normalized * reticalLocal.x);
        reticalPos +=  (retical.transform.up.normalized * reticalLocal.y);

        retical.transform.position = reticalPos;

        //retical.transform.localPosition = new Vector3(reticalLocal.x, reticalLocal.y, retical.transform.localPosition.z);
        retical.transform.LookAt(GameManager.Instance.vCam.transform.position);
        subReticals.transform.LookAt(this.transform.position);

        //camera position forward + 
    }
}
