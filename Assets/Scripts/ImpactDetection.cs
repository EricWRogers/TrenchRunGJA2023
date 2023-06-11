using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactDetection : MonoBehaviour
{
    public float impactMultiplier = 1.0f;
    public float minForce = 2.0f;
    public float lastImpactMag = 0.0f;

    private Rigidbody rgbd;

    void Start()
    {
        rgbd = gameObject.GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        lastImpactMag = collision.relativeVelocity.magnitude;
        if (collision.relativeVelocity.magnitude >= minForce) {
            gameObject.GetComponent<SuperPupSystems.Helper.Health>().Damage((int)(collision.relativeVelocity.magnitude * impactMultiplier));
        }
    }
}
