using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Animator animator;
    public Image actorImage;
    public TMP_Text actorName;
    public TMP_Text messageText;
    public RectTransform backgroundBox;

    public Message[] currentMessages;
    public Actor[] currentActors;

    public float textSpeed;
    public int activeMessage = 0;

    public static bool isActive = false;

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        animator.SetBool("isOpen", true);
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;

        Debug.Log("Started Convserations. Loading conversation : " + messages.Length);
        DisplayMessage();
    }
    
    public void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
    }

    public void NextMessage()
    {
        activeMessage++;
        if(activeMessage < currentMessages.Length)
        {
            DisplayMessage();
            StopAllCoroutines();
            StartCoroutine(TypeMessage(currentMessages[activeMessage]));

        } 
        else
        {
            Debug.Log("Conversation ended");
            isActive = false;
            animator.SetBool("isOpen", false);
        }
    }

    IEnumerator TypeMessage(Message message)
    {
        //foreach(char letter in message.message)
        for(int i=0; i < message.message.Length; i++)
        {
            messageText.text += message.message[i];
            yield return new WaitForSeconds(textSpeed);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && isActive == true)
        {
            NextMessage();
        }
    }
}
