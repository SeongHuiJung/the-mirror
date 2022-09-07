using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogSelectEventScript : MonoBehaviour
{
    DialogManager dialogManager;
    SelecteMoveScript selecteMoveScript;

    private void Awake()
    {
        dialogManager = GameManager.dialogManager;
        selecteMoveScript = GetComponent<SelecteMoveScript>();

        selecteMoveScript.selectActionList = new List<Action>();

        switch (dialogManager.GetIndex())
        {
            case 27:
                selecteMoveScript.selectActionList.Add(Uda);
                selecteMoveScript.selectActionList.Add(Nephilim);
                break;
        }
        
    }

    public void Uda()
    {
        
    }

    public void Nephilim()
    {
        dialogManager.SetIndex(dialogManager.GetIndex() + 1);
    }
}
