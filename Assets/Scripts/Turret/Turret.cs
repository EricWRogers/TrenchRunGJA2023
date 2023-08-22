using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject target;
    public TurretMount mount;
    public TurretAxle axle;
    public Transform firePoint;
    public float range;
    public float accurecy = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameManager.Instance.GetPlayer().gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        mount.FaceTarget(target.transform.position);
        axle.FaceTarget(target.transform.position);
    }

    public void Fire()
    {
        Debug.Log("Fire");
        if (Vector3.Distance(target.transform.position, firePoint.position) > range)
            return;
        Debug.Log("Bang");
        GameObject bullet = SuperPupSystems.Helper.SimpleObjectPool.instance.SpawnFromPool(
            "enemy_bullet",
            firePoint.position,
            Quaternion.identity
        );

        Vector3 positionOnPoint = RandomPointOnPlane(
            target.transform.position,
            firePoint.forward,
            accurecy
        );

        bullet.transform.LookAt(positionOnPoint);
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
