using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float speed = 20f;
    public float cullTime = 10f;
    public LayerMask mask;
    public List<string> tags;
    public string impactEffectCode;

    private bool launched = false;
    private bool hit = false;
    private Vector3? positionLastFrame;
    private RaycastHit info;
    private Timer timer;

    private void Start()
    {
        timer = GetComponent<Timer>();
        timer.timeout.AddListener(DestroyBullet);
    }

    private void FixedUpdate()
    {
        if (positionLastFrame == null) {
            positionLastFrame = transform.position;
            timer.StartTimer();
        }
        
        transform.position += transform.forward * speed * Time.deltaTime;

        if (Physics.Linecast((Vector3)positionLastFrame, transform.position, out info, mask))
        {
            if (tags.Contains(info.transform.tag) || info.transform.GetComponentInParent<Health>() != null)
            {
                info.transform.GetComponentInParent<Health>().Damage(Mathf.RoundToInt(damage));
            }
            if(info.transform.gameObject.GetComponentInParent<CombatHandler>() != null)
            {
                info.transform.gameObject.GetComponentInParent<CombatHandler>().DowngradeWeapon();
            }

            if(impactEffectCode != "")
            {
                GameObject laserProj = SimpleObjectPool.instance.SpawnFromPool(impactEffectCode, this.transform.position, Quaternion.identity);
            }
            DestroyBullet();
        }

        positionLastFrame = transform.position;
    }

    private void DestroyBullet()
    {
        positionLastFrame = null;
        timer.StopTimer();
        gameObject.SetActive(false);
    }
}
