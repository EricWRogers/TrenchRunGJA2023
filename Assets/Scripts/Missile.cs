using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class Missile : MonoBehaviour
{
    public int damage;
    public GameObject target;
    public Vector3 lastTargetPosition;
    public float speed = 20f;
    public float rotationSpeed = 5.0f;
    public List<string> tags;
    public LayerMask mask;
    private Vector3 lastPosition;
    private RaycastHit info;
    void FixedUpdate()
    {
        if (target == null)
            Fire(lastTargetPosition);
        else
            lastTargetPosition = target.transform.position;
        
        // rotate
        Vector3 direction = lastTargetPosition - transform.position;
        Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        
        // move
        transform.position += transform.forward * speed * Time.deltaTime;

        // check for hit
        if (Physics.Linecast((Vector3)lastPosition, transform.position, out info, mask))
        {
            if (tags.Contains(info.transform.tag))
            {
                info.transform.GetComponentInParent<Health>().Damage(Mathf.RoundToInt(damage));
            }

            DestroyMissile();
        }

        lastPosition = transform.position;
    }

    public void Fire(Vector3 _target)
    {
        lastPosition = transform.position;

        // Find target

    }

    private void DestroyMissile()
    {
        gameObject.SetActive(false);
        // spawn explosion

    }
}
