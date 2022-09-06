using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class ConversationInteraction : InteractionEvent
{
    [SerializeField]
    DialogManager dialogManager;
    public ConversationInteraction(DialogManager originDialogManager, GameObject obj)
    {
        dialogManager = originDialogManager;
    }
    public override void Approach(GameObject player)
    {

    }

    public override void Interact(GameObject player)
    {

        dialogManager.StartConversation();
    }

    public override void StopInteract(GameObject player)
    {
        dialogManager.EndConversation();
    }
}
*/
public class ConversationInteractionEventReceiver : InteractionEvent //��ȣ�ۿ� �޴� ����, ������ �ѹ� �� monobehavior�� ���ֺ���. player�� ������ �׸��̴�
{
    DialogManager dialogManager;
    //ConversationInteraction conversationInteractionClass;
    public void Awake()
    {
        dialogManager = GetComponent<DialogManager>();
        //conversationInteractionClass = new ConversationInteraction(GetComponent<DialogManager>(), gameObject);
        
    }
    public override void Interact(GameObject player)
    {
        dialogManager.StartConversation();
        //conversationInteractionClass.Interact(player);
    }
    public override void StopInteract(GameObject player)
    {
        dialogManager.EndConversation();
        //conversationInteractionClass.StopInteract(player);
    }
    public override void Approach(GameObject player)
    {

    }

}