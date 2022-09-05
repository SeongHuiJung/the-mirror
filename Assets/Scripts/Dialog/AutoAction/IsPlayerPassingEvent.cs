using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerPassingEvent : DialogEvent
{
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
            GameManager.dialogManager.StartConversation();
    }
    
}
