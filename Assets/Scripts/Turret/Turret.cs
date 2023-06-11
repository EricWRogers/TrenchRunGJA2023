using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject target;
    public TurretMount mount;
    public TurretAxle axle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mount.FaceTarget(target.transform.position);
        axle.FaceTarget(target.transform.position);
    }
}
