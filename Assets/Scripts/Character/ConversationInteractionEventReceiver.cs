using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ConversationInteraction : InteractionEvent
{
    [SerializeField]
    DialogManager dialogManager;
    public ConversationInteraction(DialogManager originDialogManager, GameObject obj)
    {
        dialogManager = originDialogManager;
    }
    public void Approach()
    {

    }

    public void Interact()
    {

        dialogManager.StartConversation();
    }

    public void StopInteract()
    {
        dialogManager.EndConversation();
    }
}
public class ConversationInteractionEventReceiver : MonoBehaviour //��ȣ�ۿ� �޴� ����
{
    
    ConversationInteraction conversationInteractionClass;
    public void Awake()
    {
        conversationInteractionClass = new ConversationInteraction(GetComponent<DialogManager>(), gameObject);
    }
    public void Interact()
    {
        conversationInteractionClass.Interact();
    }
    public void StopInteract()
    {
        conversationInteractionClass.StopInteract();
    }
        
}