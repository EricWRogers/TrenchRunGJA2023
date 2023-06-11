using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;

    public void StartDialogue()
    {
        FindObjectOfType<DialogueManager>().OpenDialogue(messages, actors);
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("Hit");
            StartDialogue();
        }
    }
}

[System.Serializable]
public class Message
{
    public string message;
    public int actorId;
    
}

[System.Serializable]
public class Actor 
{
    public string name;
    public Sprite sprite;
}
