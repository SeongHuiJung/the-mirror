using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogEvent;

public class DialogEventHandler : MonoBehaviour
{
    //dialog manager가 부모 오브젝트에 있을 때 오브젝트 연결
    [SerializeField]
    GameObject dialogManagerObject;
    EventCallDelegate eventCallDelegate;
    [SerializeField]

    DialogEvent[] dialogEvents;
    DialogManager dialogManager;
    // Start is called before the first frame update
    void Start()
    {
        if (dialogManagerObject)
            dialogManager = dialogManagerObject.GetComponent<DialogManager>();
        else
            dialogManager = GetComponent<DialogManager>(); //value does not fall in ???
        //eventCallDelegate = dialogManager.CallDialogByEvent;
        
        for(int i=0; i<dialogEvents.Length; i++)
        {
            dialogEvents[i].InitEvent(eventCallDelegate);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
