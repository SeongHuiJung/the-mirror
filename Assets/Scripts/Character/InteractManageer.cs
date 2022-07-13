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
    
    public void Interact(int id) //0 : interact
    {
        switch (id)
        {
            case 0:
                if(gameObject == null)
                {
                    return;
                }
                ConversationInteractionEventReceiver conversationInteractionEventReceiver = otherObject.GetComponent<ConversationInteractionEventReceiver>();
                if(conversationInteractionEventReceiver != null)
                    conversationInteractionEventReceiver.Interact();
                break;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            otherObject = collision.gameObject;
        Debug.Log("enter");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
            otherObject = null;
        Debug.Log("finish");
    }
}
