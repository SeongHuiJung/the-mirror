using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public List<GameObject> slotList, selectedList;
    public List<Item> itemList;
    public GameObject selected3, selected2, hp;
    public int moveIndex, selectedIndex, pageIndex = 0;
    public Sprite unmovedImage, movedImage, unselectedImage, selectedImage;
    public bool selectedItem, pageSelected;

    private void OnEnable()
    {
        ChagngeMovedImage(slotList, (moveIndex % 12), 0, unmovedImage, movedImage);
        moveIndex = 0;
        pageIndex = 0;
        PageItemShow();
    }

    private void Start()
    {
        hp.GetComponent<Transform>().SetAsFirstSibling();
    }

    private void Update()
    {
        if (!pageSelected)
        {
            Selected();
            if(!selectedItem)
                ItemMoved();
            else
                SelectMoved();
        }
        else
            PageMoved();
    }

    //������ ĭ / select ĭ �̵�
    void ItemMoved()
    {
        if (Input.GetKeyDown("w"))
        {
            if (moveIndex <= 3)
            {
                pageSelected = true;
                slotList[moveIndex].GetComponent<Image>().sprite = unmovedImage;
                //������ �̹��� ����
            }
            else
            {
                ChagngeMovedImage(slotList, (moveIndex % 12), (moveIndex - 4) % 12, unmovedImage, movedImage);
                moveIndex -= 4;
                ChangePageItem();
            }
        }
        else if (Input.GetKeyDown("a") && moveIndex > 0)
        {
            ChagngeMovedImage(slotList, moveIndex % 12, (moveIndex - 1) % 12, unmovedImage, movedImage);
            moveIndex--;
            ChangePageItem();
        }
        else if (Input.GetKeyDown("s") && 24 > moveIndex + 4)
        {
            ChagngeMovedImage(slotList, moveIndex % 12, (moveIndex + 4) % 12, unmovedImage, movedImage);
            moveIndex += 4;
            ChangePageItem();
        }
        else if (Input.GetKeyDown("d") && 24 > moveIndex + 1)
        {
            ChagngeMovedImage(slotList, moveIndex % 12, (moveIndex + 1) % 12, unmovedImage, movedImage);
            moveIndex++;
            ChangePageItem();
        }
    }

    //������ ��ȣ�ۿ� â �̵�
    void SelectMoved()
    {
        if (Input.GetKeyDown("w") && selectedIndex != 0)
        {
            selectedIndex--;
            ChagngeMovedImage(selectedList, selectedIndex + 1, selectedIndex, unselectedImage, selectedImage);
        }
        else if (Input.GetKeyDown("s") && selectedIndex != 2)
        {
            selectedIndex++;
            ChagngeMovedImage(selectedList, selectedIndex - 1, selectedIndex, unselectedImage, selectedImage);
        }
    }

    //������ �̵�
    void PageMoved()
    {
        if (Input.GetKeyDown("a") && pageIndex > 0)
        {
            //������ �̹��� ����
            pageIndex--;
            PageItemShow();
        }
        else if (Input.GetKeyDown("d") && pageIndex == 0)
        {
            //������ �̹��� ����
            pageIndex++;
            PageItemShow();
        }
        else if (Input.GetKeyDown("s"))
        {
            //������ �̹��� ����
            pageSelected = false;
            moveIndex = pageIndex * 12;
            slotList[moveIndex % 12].GetComponent<Image>().sprite = movedImage;
        }
    }

    //�κ��丮 ��ȣ�ۿ�
    void Selected()
    {
        if (Input.GetKeyDown("e"))
        {
            if (!selectedItem)
                ActiveSelected();
            else if (selectedIndex == 2)
                DeactiveSelected();
        }
    }

    //select â ����
    void ActiveSelected()
    {
        selected3.SetActive(true);
        selected3.transform.position = new Vector2(slotList[moveIndex].transform.position.x + 215, slotList[moveIndex].transform.position.y - 65);
        selectedItem = true;
        selectedList[0].GetComponent<Image>().sprite = selectedImage;
    }

    //select â ����
    void DeactiveSelected()
    {
        selectedList[selectedIndex].GetComponent<Image>().sprite = unselectedImage;
        selectedIndex = 0;
        selectedItem = false;
        selected3.SetActive(false);
    }

    //ĭ �̵��� �� �̹��� ����
    void ChagngeMovedImage(List<GameObject> list, int preIndex, int nextIndex, Sprite unImage, Sprite doImage)
    {
        list[preIndex].GetComponent<Image>().sprite = unImage;
        list[nextIndex].GetComponent<Image>().sprite = doImage;
    }

    //������ ���� Ȯ��
    void ChangePageItem()
    {
        if(pageIndex != moveIndex / 12)
        {
            pageIndex = moveIndex / 12;
            PageItemShow();
        }
    }


    //�������� ������ �����ֱ�
    void PageItemShow()
    {
        int i, j;
        for (i = pageIndex * 12, j = 0; i < pageIndex * 12 + 12 && i < itemList.Count; i++, j++)
            slotList[j].transform.GetChild(0).GetComponent<InventorySlot>().item = itemList[i];

        for (; j < 12; j++)
            slotList[j].transform.GetChild(0).GetComponent<InventorySlot>().item = null;
    }
}
