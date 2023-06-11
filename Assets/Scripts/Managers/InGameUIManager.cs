using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayMaker;

public class InGameUIManager : SingletonMonoBehaviour<InGameUIManager>
{
    public GameObject targetingRetical;
    public GameObject subTargetingRetical;
    public PlayMakerFSM pause;

    public Animator gameOver;
    public Animator winOver;

    public void GameOver()
    {
        gameOver.SetBool("GameOver", true);
    }

    public void WinGame()
    {
        winOver.SetBool("GameOver", true);
    }

    public void Pause()
    {
        pause.SendEvent("Pause");
        pause.SendEvent("Resume");
    }
}
