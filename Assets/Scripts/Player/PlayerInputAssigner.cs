using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInputAssigner : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.Instance.GetPlayer() != null)
        {
            Debug.Log("Got Player");
            GameManager.Instance.GetPlayer().GetComponent<PlayerInputHandler>().playerInput = this.GetComponent<PlayerInput>();
            GameManager.Instance.GetPlayer().GetComponent<PlayerInputHandler>().InitializeInput();
        }
    }
}
