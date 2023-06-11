using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInParent<CombatHandler>() != null)
        {
            other.GetComponentInParent<CombatHandler>().Die();
        }
    }
}
