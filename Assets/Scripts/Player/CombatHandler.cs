using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    public LayerMask shootingMask;
    public float fallbackRange = 50f;

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

    public Vector3 GetTarget()
    {
        Vector3 target = new Vector3();

        //raycast from the camera to the retical, ignore the retical, the hit is the target, if there is no hit it extends the direction at a point

        Vector3 directionToRetical = GameManager.Instance.vCam.transform.position - retical.transform.position;

        RaycastHit hit;

        if ((Physics.Raycast(GameManager.Instance.vCam.transform.position, directionToRetical, out hit, fallbackRange, ~shootingMask)))
        {
            Debug.Log("hit with bullker");

            target = hit.point;

        }
        else
        {
            Vector3 tpoint = GameManager.Instance.vCam.transform.position + directionToRetical * fallbackRange;
        }



        return target;
    }


    public void MoveRetical()
    {
        Vector2 look = GetComponent<PlayerController>().GetLookInput();

        look = new Vector2(-look.x, look.y);

        reticalPos = GameManager.Instance.vCam.transform.position + (GameManager.Instance.vCam.transform.forward.normalized * reticalDistance);
        reticalLocal += look;
        reticalLocal = new Vector2(Mathf.Clamp(reticalLocal.x, -reticalBounds.x, reticalBounds.x), Mathf.Clamp(reticalLocal.y, -reticalBounds.y, reticalBounds.y));
        
        retical.transform.LookAt(GameManager.Instance.vCam.transform.position);
        reticalPos +=  (retical.transform.right.normalized * reticalLocal.x);
        reticalPos +=  (retical.transform.up.normalized * reticalLocal.y);

        retical.transform.position = reticalPos;

        //retical.transform.localPosition = new Vector3(reticalLocal.x, reticalLocal.y, retical.transform.localPosition.z);
        subReticals.transform.LookAt(this.transform.position);

        //camera position forward + 
    }
}
