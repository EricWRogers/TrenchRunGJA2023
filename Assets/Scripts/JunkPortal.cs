using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkPortal : MonoBehaviour
{
    public Transform eye;
    public Transform radiusHandle;
    public GameObject junk;
    public float minForce = 5.0f;
    public float maxForce = 10.0f;

    public void SpawnJunk()
    {
        Vector3 positionOnPoint = RandomPointOnPlane(
            transform.position,
            transform.forward,
            Vector3.Distance(transform.position,radiusHandle.position)
        );

        GameObject gO = SuperPupSystems.Helper.SimpleObjectPool.instance.SpawnFromPool(
            "trash_code",
            eye.transform.position,
            Quaternion.identity
        );
        
        gO.transform.LookAt(positionOnPoint);

        Rigidbody rb = gO.GetComponent<Rigidbody>();

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.AddForce(
            gO.transform.forward * Random.Range(minForce, maxForce),
            ForceMode.Impulse
        );
    }

    private Vector3 RandomPointOnPlane(Vector3 position, Vector3 normal, float radius)
    {
        Vector3 randomPoint;

        do
        {
            randomPoint = Vector3.Cross(Random.insideUnitSphere, normal);
        } while (randomPoint == Vector3.zero);

        randomPoint.Normalize();
        randomPoint *= radius;
        randomPoint += position;

        return randomPoint;
    }
}
