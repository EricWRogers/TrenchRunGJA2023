using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerController))]
public class PlayerInputHandler : MonoBehaviour
{
    public float doubleTapCooldown = 1f;

    [HideInInspector]
    public MainPlayer controls;

    private PlayerInput playerInput;

    private PlayerController pCon;
    private CombatHandler combatHandler;
    private float currentDoubleTapTime;
    private bool tappedOnce, releasedOnce;

    private void Awake()
    {
        pCon = GetComponent<PlayerController>();
        combatHandler = GetComponent<CombatHandler>();
    }

    private void Update()
    {
        if (tappedOnce)
        {
            currentDoubleTapTime += Time.deltaTime;

            if(currentDoubleTapTime >= doubleTapCooldown)
            {
                tappedOnce = false;
                releasedOnce = false;
                currentDoubleTapTime = 0;
            }
        }
    }

    public void InitializeInput()
    {
        Debug.Log("yay we did it");
        playerInput.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        if(obj.action.name == controls.Player.Movement.name)
        {
            pCon.UpdateMoveInput(obj.ReadValue<Vector2>());

            if(obj.ReadValue<Vector2>().x >= 0.85f && !tappedOnce || obj.ReadValue<Vector2>().x <= -0.85f && !tappedOnce)
            {
                tappedOnce = true;
            }
            if (obj.performed)
            {
                if (tappedOnce && releasedOnce)
                {
                    //DO A BARREL ROLL!
                    if(obj.ReadValue<Vector2>().x > 0)
                    {
                        pCon.DoABarrelRoll(false);
                    }
                    else
                    {
                        pCon.DoABarrelRoll(true);
                    }

                    tappedOnce = false;
                    releasedOnce = false;
                }
            }
            if(obj.canceled && tappedOnce)
            {
                releasedOnce = true;
            }
        }
        if(obj.action.name == controls.Player.Look.name)
        {
            pCon.UpdateLookInput(obj.ReadValue<Vector2>());
        }
        if(obj.action.name == controls.Player.FireLaser.name)
        {
            FireLaser(obj);
        }
        if(obj.action.name == controls.Player.FireRocket.name)
        {
            FireRocket(obj);
        }
        if(obj.action.name == controls.Player.Boost.name)
        {
            Boost(obj);
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
            combatHandler.FireRocket();
        }
    }

    private void Boost(CallbackContext obj)
    {
        if (obj.started)
        {
            pCon.ToggleBoost(true);
        }
        if (obj.canceled)
        {
            pCon.ToggleBoost(false);
        }
    }


}
