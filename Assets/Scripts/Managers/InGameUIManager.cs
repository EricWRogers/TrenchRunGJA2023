using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIManager : SingletonMonoBehaviour<InGameUIManager>
{
    public GameObject targetingRetical;
    public GameObject subTargetingRetical;

    public Animator gameOver;

    public void GameOver()
    {
        gameOver.SetBool("GameOver", true);
    }
}
