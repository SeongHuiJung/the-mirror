using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InteractManageer : MonoBehaviour
{
    GameObject otherObject; //상호작용 가능한 상대 오브젝트, 없으면 null
    
    public void Interact(int id) //0 : interact 1, 1 : stopInteract
    {
        if (otherObject == null)
        {
            return;
        }

        InteractObjectScript interactObjectScript = otherObject.GetComponent<InteractObjectScript>();
        if (interactObjectScript != null)
        {
            DialogManager dialogManager = GameManager.dialogManager;
            dialogManager.SetPreindex(dialogManager.GetId());
            dialogManager.SetId(interactObjectScript.GetId());

            switch (id)
            {
                case 0:
                    GameManager.dialogManager.StartConversation();
                    break;
                case 1:
                    GameManager.dialogManager.EndConversation();
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        otherObject = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Interact(1);
        otherObject = null;
    }
}
