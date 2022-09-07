using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;

public class SelecteMoveScript : MonoBehaviour
{
    [SerializeField] protected int index ;
    public int maxindex;
    public static int impossibleindex;

    [SerializeField] protected List<Image> panelList;
    [SerializeField] protected List<Sprite> currentImageList;
    [SerializeField] protected List<Sprite> selectedImageList;
    [SerializeField] protected List<Sprite> unSelectedImageList;

    public List<Action> selectActionList;

    DialogManager dialogManager;

    private void Awake()
    {
        dialogManager = GameManager.dialogManager;

        index = -1;
        maxindex = 1;
    }

    protected void Update()
    {
        Move(1);
        Select();
    }

    //override
    public virtual void Move(int maxIndex)
    {
        if (Input.GetKeyDown("w"))
        {
            if (index == -1)
            {
                index = 1;
                ChangePanel(index, index);
            }
            else if (index > 0)
                ChangePanel(index, --index);
            Debug.Log("w");
        }
        if (Input.GetKeyDown("s"))
        {
            if (index == -1)
            {
                index = 0;
                //ChangePanel(index, index);
            }
            else if (index < maxindex)
                ChangePanel(index, ++index);
        }
    }

    //override�ؼ� ����Ͻø� �˴ϴ�.
    public virtual void Select()
    {
        if (Input.GetKeyDown("e") || Input.GetKeyDown(KeyCode.Return))  // e, enter �Է� ���̵��Դϴ�.
        {
            //Ʃ�丮�� ���� �߰� ����
            if (index == impossibleindex)
            {
                dialogManager.isImpossiblenext = true;
                return;
            }
            else {
                dialogManager.isImpossiblenext = false;

                if (index == -1)
                    AgainButTutorial();
                else
                    selectActionList[index]();

                Destroy(this.gameObject);
            }
        }
    }

    //color�� ���߿� �̹����� �ٲ� �����Դϴ�.
    public void ChangePanel(int preIndex, int index)
    {
        if (panelList[preIndex] != null) panelList[preIndex].color = new Color(0.23f, 0.23f, 0.7f); //unselectedimage
        if (panelList[preIndex] != null) panelList[index].color = new Color(1, 1, 1);    //selectedimage
    }

    //��������Ʈ ��ȯ
    public void ChangeSprite(int preIndex, int index)
    {
        currentImageList[preIndex] = unSelectedImageList[preIndex];
        currentImageList[index] = selectedImageList[index];
    }

    void AgainButTutorial()
    {
        DialogManager dialogManager = GameManager.dialogManager;
        dialogManager.bedSettutorialindex = true;
        dialogManager.isDeleteSelect = true;
    }
}
