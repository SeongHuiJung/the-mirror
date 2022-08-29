using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InteractManageer : MonoBehaviour
{
    GameObject otherObject; //��ȣ�ۿ� ������ ��� ������Ʈ, ������ null
    //bool interactable = false; //��ũ��Ʈ �޷��־ ��ȣ�ۿ� �Ұ��Ұ�� -> ���� ���� ����

    // Start is called before the first frame update
    Type[] interactionTypes = new Type[] { typeof(ConversationInteractionEventReceiver), typeof(TeleportEventReceiver) }; //��ȣ�ۿ� ������ ��� Ŭ���� ����
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Interact(int id) //0 : interact 1, 1 : stopInteract
    {
        if (otherObject == null)
        {
            return;
        }

        InteractionEvent conversationInteractionEventReceiver;

        if (otherObject.gameObject.name.Equals("Bed"))
        {
            GameObject character = GameManager.player;
            conversationInteractionEventReceiver = character.GetComponent<ConversationInteractionEventReceiver>();
            character.GetComponent<DialogManager>().SetId(2);
        }

        //�ӽ÷� �̷��� �ϰ� ���� ���� �� ��ӱ�� Ȱ���ؼ� ��ĥ ����
        
        else
        {
            Component c = null;
            foreach (Type t in interactionTypes) //��ȣ�ۿ� ������ ��� ������Ʈ Ž��
            {
                c = otherObject.GetComponent(t);
                if (c != null)
                    break;

            }
            if (c == null) //��ȣ�ۿ� ������ ��ü�� �ƴ�
            {
                
                return;
            }
            conversationInteractionEventReceiver = (InteractionEvent) c;


        }
        
        switch (id)
        {
            
            case 0:
                
                if(conversationInteractionEventReceiver != null)
                    conversationInteractionEventReceiver.Interact(transform.parent.gameObject);
                break;
            case 1:
                if (conversationInteractionEventReceiver != null)
                    conversationInteractionEventReceiver.StopInteract(transform.parent.gameObject);
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
