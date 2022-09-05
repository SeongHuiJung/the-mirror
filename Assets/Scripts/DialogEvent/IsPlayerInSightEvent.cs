using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IsPlayerInSightEvent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            GameManager.dialogManager.StartConversation();
    }
}
