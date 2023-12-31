using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using UnityEngine.VFX;

public class Missile : MonoBehaviour 
{ 
    public int damage; 
    public GameObject target; 
    public Vector3 lastTargetPosition; 
    public float speed = 20f; 
    public float rotationSpeed = 5.0f; 
    public List<string> tags; 
    public LayerMask mask; 
    public float radius; 
    private Vector3 lastPosition; 
    private RaycastHit info; 
    
    void FixedUpdate() 
    { 
        if (target == null) 
        {
            FindTarget(transform.position); 
        }
        else {
            if (target.active == true) {
                lastTargetPosition = target.transform.position;
                transform.LookAt(lastTargetPosition); 
            }
            else {
                target = null;
                FindTarget(transform.position);
                Debug.Log("Find New target");
            }
        }
         
        // rotate 
        //Vector3 direction = lastTargetPosition - transform.position; 
        //Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction); 
        //transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime); 
        
         
        // move 
        transform.position += transform.forward * speed * Time.deltaTime; 
 
        // check for hit 
        if (Physics.Linecast((Vector3)lastPosition, transform.position, out info, mask)) 
        { 
            Debug.Log("HIT"); 
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
        GetComponent<Timer>().StartTimer(); 
 
        // Find target 
        FindTarget(_target);
    } 

    void FindTarget(Vector3 _target) {
        Collider[] hitColliders = Physics.OverlapSphere(_target, radius, mask); 
        foreach (var hitCollider in hitColliders) 
        { 
            if (tags.Contains(hitCollider.tag)) { 
                if (target == null) { 
                    target = hitCollider.gameObject; 
                } else { 
                    if (Vector3.Distance(hitCollider.gameObject.transform.position, _target) <  
                        Vector3.Distance(target.transform.position, _target)) { 
                            target = hitCollider.gameObject; 
                            Debug.Log("Fire : " + target.name);
                        } 
                } 
            } 
        } 
    }
 
    public void DestroyMissile() 
    { 
        gameObject.SetActive(false); 
        // spawn explosion 
        GameObject explosion = SuperPupSystems.Helper.SimpleObjectPool.instance.SpawnFromPool( 
            "missile_explosion", 
            transform.position, 
            Quaternion.identity 
        ); 
 
        explosion.GetComponent<VisualEffect>().Play(); 
    } 
} 


