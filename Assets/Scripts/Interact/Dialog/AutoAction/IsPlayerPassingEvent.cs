using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerPassingEvent : InteractObjectScript
{
    [SerializeField] bool isPossibleDialog = true;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && isPossibleDialog)
        {
            isPossibleDialog = false;
            Interact();
            StartCoroutine(DialogCooltime());
        }
    }

    public override void Interact()
    {
        DialogManager dialogManager = GameManager.dialogManager;
        dialogManager.SetId(id);
        dialogManager.StartConversation();
    }

    IEnumerator DialogCooltime()
    {
        yield return new WaitForSeconds(300f * Time.deltaTime);
        isPossibleDialog = true;
    }
}
