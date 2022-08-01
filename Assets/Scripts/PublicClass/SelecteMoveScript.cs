using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelecteMoveScript : MonoBehaviour
{
    public int index;
    public List<Image> panelList;
    public Image unselectedImage, selectedImage;

    public void Start()
    {
        index = -1;
    }

    public void Move(int maxIndex)
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
        }
        if (Input.GetKeyDown("s"))
        {
            if (index == -1)
            {
                index = 0;
                ChangePanel(index, index);
            }
            else if (index < maxIndex)
                ChangePanel(index, ++index);
        }
    }

    //override�ؼ� ����Ͻø� �˴ϴ�.
    public virtual void Select()
    {
        if (Input.GetKeyDown("e") || Input.GetKeyDown(KeyCode.Return))  // e, enter �Է� ���̵��Դϴ�.
        {
        }
    }

    //color�� ���߿� �̹����� �ٲ� �����Դϴ�.
    public void ChangePanel(int preIndex, int index)
    {
        panelList[preIndex].color = new Color(0.23f, 0.23f, 0.7f); //unselectedimage
        panelList[index].color = new Color(1, 1, 1);    //selectedimage
    }
}
