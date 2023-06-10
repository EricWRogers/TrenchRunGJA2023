using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerController))]
public class PlayerInputHandler : MonoBehaviour
{
    [HideInInspector]
    public MainPlayer controls;

    private PlayerInput playerInput;

    private PlayerController pCon;

    private void Awake()
    {
        pCon = GetComponent<PlayerController>();
        playerInput = GetComponent<PlayerInput>();
    }


    private void Input_onActionTriggered(CallbackContext obj)
    {
        if(obj.action.name == controls.Player.Movement.name)
        {
            pCon.UpdateMoveInput(obj.ReadValue<Vector2>());
        }
    }


}
