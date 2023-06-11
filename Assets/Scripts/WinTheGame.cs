using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTheGame : MonoBehaviour
{
    public Transform playerGuide;
    private bool wonAlreadyDude = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PlayerController>() != null && !wonAlreadyDude)
        {

            PlayerController player = other.GetComponentInParent<PlayerController>();

            player.canMove = false;
            player.transform.forward = playerGuide.transform.forward;
            player.transform.position = playerGuide.transform.position;
            InGameUIManager.Instance.WinGame();

            wonAlreadyDude = true;
        }
    }

}
