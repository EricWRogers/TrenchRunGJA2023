using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class CombatHandler : MonoBehaviour
{
    public GameObject deathEffect;

    public LayerMask shootingMask;
    public float fallbackRange = 50f;
    public int startingUpgrades = 0;
    public float lookScaling = .75f;
    public float reticalSmoothness = .5f;
    public float reticalDistance = 50f;
    public Vector2 reticalBounds;

    public WeaponProfile laserProfile;
    public WeaponProfile rocketProfile;
    public Transform laserProjectileSpawn;
    public Transform rocketProjectileSpawn;

    private GameObject retical;
    private GameObject subReticals;

    private WeaponElement laserElement;
    private WeaponElement rocketElement;

    private Vector3 reticalPos;
    private Vector2 reticalLocal;
    private Vector2 lookSmoothed;
    private string recentUpgrade = "laser";

    private void Start()
    {
        retical = InGameUIManager.Instance.targetingRetical;
        subReticals = InGameUIManager.Instance.subTargetingRetical;
        //retical.transform.parent = GameManager.Instance.vCam.transform;
        InitializeWeapons();

    }

    private void FixedUpdate()
    {
        MoveRetical();
    }

    private void InitializeWeapons()
    {
        laserElement = new WeaponElement(laserProfile.weaponLevels);
        rocketElement = new WeaponElement(rocketProfile.weaponLevels);

        laserElement.currentLevelIndex = startingUpgrades;
    }

    public void UpgradeWeapon(string _weaponTag)
    {
        switch (_weaponTag)
        {
            case "laser":

                laserElement.currentLevelIndex++;

                if(laserElement.currentLevelIndex > laserElement.loadedLevelProfiles.Count)
                {
                    laserElement.currentLevelIndex = laserElement.loadedLevelProfiles.Count;
                }

                recentUpgrade = "laser";

                break;
            case "rocket":

                rocketElement.currentLevelIndex++;

                if (rocketElement.currentLevelIndex > rocketElement.loadedLevelProfiles.Count)
                {
                    rocketElement.currentLevelIndex = rocketElement.loadedLevelProfiles.Count;
                }

                recentUpgrade = "rocket";

                break;
        }
    }

    public void DowngradeWeapon()
    {
        Debug.Log("Downgrading");

        if (GetComponent<PlayerController>().rolling)
        {
            GetComponent<PlayerController>().anim.SetTrigger("Parry");
            return;
        }

        GetComponent<PlayerController>().anim.SetTrigger("Hit");

        string _weaponTag = recentUpgrade;

        switch (_weaponTag)
        {
            case "laser":
                laserElement.currentLevelIndex--;

                if(laserElement.currentLevelIndex < 0)
                {
                    laserElement.currentLevelIndex = 0;
                    Die();
                }
                break;
            case "rocket":
                rocketElement.currentLevelIndex--;

                if(rocketElement.currentLevelIndex < 0)
                {
                    rocketElement.currentLevelIndex = 0;
                    Die();
                }
                break;
        }
    }

    public void Die()
    {
        if (!GetComponent<PlayerController>().canMove)
        {
            return;
        }




        InGameUIManager.Instance.GameOver();
        GameObject effect = GameObject.Instantiate(deathEffect, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    public void FireLaser()
    {
        GameObject laserProj = SimpleObjectPool.Instance.SpawnFromPool(laserElement.GetCurrentLevelProfile().projectileObjCode, laserProjectileSpawn.transform.position, Quaternion.identity);
        laserProj.GetComponent<Bullet>().speed = laserElement.GetCurrentLevelProfile().weaponSpeed + GetComponent<PlayerController>().GetCurrentSpeed();
        laserProj.transform.forward =  GetTarget() - laserProj.transform.position;
    }

    public void FireRocket()
    {
        Debug.Log("ROCKET MAN");
        Debug.Log($"Rocket Code is {rocketElement.GetCurrentLevelProfile().projectileObjCode}");
        GameObject rocketProj = SimpleObjectPool.Instance.SpawnFromPool(rocketElement.GetCurrentLevelProfile().projectileObjCode, rocketProjectileSpawn.transform.position, Quaternion.identity);
        rocketProj.transform.forward = GetTarget() - rocketProj.transform.position;
        rocketProj.GetComponent<Missile>().speed = rocketElement.GetCurrentLevelProfile().weaponSpeed + GetComponent<PlayerController>().GetCurrentSpeed();
        rocketProj.GetComponent<Missile>().Fire((GetTarget() - rocketProjectileSpawn.position) * rocketElement.GetCurrentLevelProfile().range);
    }



    public Vector3 GetTarget()
    {
        Vector3 target = new Vector3();

        //raycast from the camera to the retical, ignore the retical, the hit is the target, if there is no hit it extends the direction at a point

        Vector3 directionToRetical = retical.transform.position - GameManager.Instance.vCam.transform.position;

        Debug.DrawRay(GameManager.Instance.vCam.transform.position, directionToRetical, Color.red);

        RaycastHit hit;

        if ((Physics.Raycast(GameManager.Instance.vCam.transform.position, directionToRetical.normalized, out hit, fallbackRange, ~shootingMask)))
        {
            Debug.Log("hit with bullker");

            target = hit.point;

        }
        else
        {
            target = GameManager.Instance.vCam.transform.position + directionToRetical.normalized * reticalDistance;
        }



        return target;
    }


    public void MoveRetical()
    {
        Vector2 look = GetComponent<PlayerController>().GetLookInput();

        look = new Vector2(-look.x, look.y);

        look *= lookScaling;

        lookSmoothed = Vector2.Lerp(lookSmoothed, look, reticalSmoothness * Time.deltaTime);

        reticalPos = GameManager.Instance.vCam.transform.position + (GameManager.Instance.vCam.transform.forward.normalized * reticalDistance);
        reticalLocal += lookSmoothed;
        reticalLocal = new Vector2(Mathf.Clamp(reticalLocal.x, -reticalBounds.x, reticalBounds.x), Mathf.Clamp(reticalLocal.y, -reticalBounds.y, reticalBounds.y));
        
        retical.transform.LookAt(GameManager.Instance.vCam.transform.position);
        reticalPos +=  (retical.transform.right.normalized * reticalLocal.x);
        reticalPos +=  (retical.transform.up.normalized * reticalLocal.y);

        retical.transform.position = reticalPos;

        //retical.transform.localPosition = new Vector3(reticalLocal.x, reticalLocal.y, retical.transform.localPosition.z);
        subReticals.transform.LookAt(this.transform.position);

        //camera position forward + 
    }
}

public class WeaponElement
{
    public int currentLevelIndex = 0;

    public List<LevelProfile> loadedLevelProfiles = new List<LevelProfile>();

    public WeaponElement(List<LevelProfile> _levelProfiles)
    {
        loadedLevelProfiles = _levelProfiles;
    }

    public LevelProfile GetCurrentLevelProfile()
    {
        return loadedLevelProfiles[currentLevelIndex];
    }
}
