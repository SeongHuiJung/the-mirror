using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IsPlayerInSightEvent : InteractObjectScript
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            Interact();
    }

    public override void Interact()
    {
        GameManager.dialogManager.StartConversation();
    }
}