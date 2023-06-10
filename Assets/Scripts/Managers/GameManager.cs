using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{

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
