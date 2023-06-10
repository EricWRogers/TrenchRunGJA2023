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
        controls = new MainPlayer();

        pCon = GetComponent<PlayerController>();
        playerInput = GetComponent<PlayerInput>();


        playerInput.onActionTriggered += Input_onActionTriggered;
    }

    private void OnDestroy()
    {
        playerInput.onActionTriggered -= Input_onActionTriggered;
    }


    private void Input_onActionTriggered(CallbackContext obj)
    {
        if(obj.action.name == controls.Player.Movement.name)
        {
            pCon.UpdateMoveInput(obj.ReadValue<Vector2>());
        }
        if(obj.action.name == controls.Player.Look.name)
        {
            pCon.UpdateLookInput(obj.ReadValue<Vector2>());
        }
    }


}
