using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAxle : MonoBehaviour
{
    public void FaceTarget(Vector3 _target)
    {
        gameObject.transform.LookAt( _target ) ;
    }
}
