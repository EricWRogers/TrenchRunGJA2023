using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent Unlocked;


    public void Unlock() {
        Unlocked.Invoke();
        gameObject.SetActive(false);
    }
}
