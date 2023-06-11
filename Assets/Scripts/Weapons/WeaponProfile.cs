using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data/WeaponProfile")]
public class WeaponProfile : ScriptableObject
{
    public string weaponTag;
    public List<LevelProfile> weaponLevels;
}

[System.Serializable]
public class LevelProfile
{
    public string projectileObjCode;
    public int damage;
    public float range;
    public float weaponSpeed;
}
