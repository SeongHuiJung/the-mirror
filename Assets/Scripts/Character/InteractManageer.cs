using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManageer : MonoBehaviour
{
    GameObject otherObject; //��ȣ�ۿ� ������ ��� ������Ʈ, ������ null
    //bool interactable = false; //��ũ��Ʈ �޷��־ ��ȣ�ۿ� �Ұ��Ұ�� -> ���� ���� ����

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Interact(int id) //0 : interact 1, 1 : stopInteract
    {
        if (gameObject == null)
        {
            return;
        }

        ConversationInteractionEventReceiver conversationInteractionEventReceiver;

        if (otherObject.gameObject.name.Equals("Bed"))
        {
            GameObject character = transform.parent.gameObject;

            conversationInteractionEventReceiver = character.GetComponent<ConversationInteractionEventReceiver>();
            character.GetComponent<DialogManager>().SetId(6);
        }
        else
            conversationInteractionEventReceiver = otherObject.GetComponent<ConversationInteractionEventReceiver>();
        
        switch (id)
        {
            
            case 0:
                
                if(conversationInteractionEventReceiver != null)
                    conversationInteractionEventReceiver.Interact();
                break;
            case 1:
                if (conversationInteractionEventReceiver != null)
                    conversationInteractionEventReceiver.StopInteract();
                break;


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
