using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

//[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerController))]
public class PlayerInputHandler : MonoBehaviour
{
    [HideInInspector]
    public MainPlayer controls;

    [HideInInspector]
    public PlayerInput playerInput;

    private PlayerController pCon;
    private CombatHandler combatHandler;

    private void Awake()
    {
        controls = new MainPlayer();

        pCon = GetComponent<PlayerController>();
        combatHandler = GetComponent<CombatHandler>();
    }

    public void InitializeInput()
    {
        Debug.Log("yay we did it");
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
        if(obj.action.name == controls.Player.FireLaser.name)
        {
            FireLaser(obj);
        }
    }

    private void FireLaser(CallbackContext obj)
    {
        if (obj.started)
        {
            combatHandler.FireLaser();
        }
    }

    private void FireRocket(CallbackContext obj)
    {
        if (obj.started)
        {
            combatHandler.FireLaser();
        }
    }


}
