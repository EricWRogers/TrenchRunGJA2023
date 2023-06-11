using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMount : MonoBehaviour
{
    public void FaceTarget(Vector3 _target)
    {
        transform.LookAt(_target);
        transform.eulerAngles = new Vector3(
            0.0f,
            0.0f,
            transform.eulerAngles.z
        );
    }
}
