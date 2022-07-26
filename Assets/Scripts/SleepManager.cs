using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepManager : SelecteMoveScript
{
    void Update()
    {
        Move(1);
        Select();
    }

    public override void Select()
    {
        if (Input.GetKeyDown("e") || Input.GetKeyDown(KeyCode.Return))
        {
            if (index == -1)
            {
                AgainButTutorial();
            }
            else
            {
                if (index == 0)
                    YesSleep();

                else
                    NoSleep();
            }
        }
    }

    void YesSleep()
    {
        //Day++? Day7?
    }

    void NoSleep()
    {
        Destroy(this.gameObject);
    }

    void AgainButTutorial()
    {
        //Ʃ�丮�� �� ���� �߰�����
        DialogManager characterDialogManager = FindObjectOfType<PlayerControllerScript>().gameObject.GetComponent<DialogManager>();
        characterDialogManager.SetIndex(5);
        Destroy(this.gameObject);
    }
}
