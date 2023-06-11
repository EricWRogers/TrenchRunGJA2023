using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    public string weaponTag = "laser";
    public GameObject graphic;
    public float spinSpeed = 1f;
    public void Update()
    {
        this.transform.Rotate(new Vector3(0, spinSpeed * Time.deltaTime, 0));
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInParent<CombatHandler>() != null)
        {
            other.GetComponentInParent<CombatHandler>().UpgradeWeapon(weaponTag);
            Destroy(this.gameObject);
        }
    }
}
