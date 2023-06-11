using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public CinemachineVirtualCamera vCam;

    private PlayerController player;
    public static Action onGMReady;

    new void Awake()
    {
        base.Awake();

        onGMReady?.Invoke();
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
