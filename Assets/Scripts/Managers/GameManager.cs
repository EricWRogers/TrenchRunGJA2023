using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public CinemachineVirtualCamera vCam;

    public PlayerController player;

    new void Awake()
    {
        base.Awake();


        Debug.Log("Awake");
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RegisterPlayer(PlayerController _player)
    {
        Debug.Log("Player Registered");
        player = _player;
    }

    public PlayerController GetPlayer()
    {
        return player;
    }

}
