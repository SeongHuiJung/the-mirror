using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class SelecteMoveScript : MonoBehaviour
{
    public int index;
    public List<Image> panelList;

    public List<Sprite> currentImageList;
    public List<Sprite> selectedImageList;
    public List<Sprite> unSelectedImageList;
    public Image unselectedImage, selectedImage;

    public List<Action> selectActionList;

    public void Start()
    {
        index = -1;
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
            else if (index < maxIndex)
                ChangePanel(index, ++index);
        }
    }

    //override해서 사용하시면 됩니다.
    public virtual void Select()
    {
        if (Input.GetKeyDown("e") || Input.GetKeyDown(KeyCode.Return))  // e, enter 입력 가이드입니다.
        {
            //튜토리얼 조건 추가 예정
            if (index == -1)
                AgainButTutorial();
            else
                selectActionList[index]();
        }
    }

    //color는 나중에 이미지로 바꿀 예정입니다.
    public void ChangePanel(int preIndex, int index)
    {
        if (panelList[preIndex] != null) panelList[preIndex].color = new Color(0.23f, 0.23f, 0.7f); //unselectedimage
        if (panelList[preIndex] != null) panelList[index].color = new Color(1, 1, 1);    //selectedimage
    }

    //스프라이트 변환
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
