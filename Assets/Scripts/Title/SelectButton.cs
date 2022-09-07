using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : SelecteMoveScript
{
    public GameObject start;
    public GameObject load;
    public GameObject quit;
    public GameObject quitAnim;
    public Animator startingAnim;
    // Update is called once per frame

    private void Start()
    {
        maxindex = 2;
    }

    public override void Move(int maxIndex)
    {
        if (Input.GetKeyDown("w"))
        {
            if (index == -1)
            {
                index = 2;
                ChangeSprite(index, index);
            }
            else if (index > 0)
                ChangeSprite(index, --index);
            else
            {
                index = 2;
                ChangeSprite(0, index);
            }
        }
        if (Input.GetKeyDown("s"))
        {
            if (index == -1)
            {
                index = 0;
                ChangeSprite(index, index);
            }
            else if (index < maxIndex)
                ChangeSprite(index, ++index);
            else
            {
                index = 0;
                ChangeSprite(2, index);
            }
        }
        start.GetComponent<Image>().sprite =currentImageList[0];
        load.GetComponent<Image>().sprite = currentImageList[1];
        quit.GetComponent<Image>().sprite = currentImageList[2];
    }

    public override void Select()
    {
        if (Input.GetKeyDown("e") || Input.GetKeyDown(KeyCode.Return))
        {
            if (index == 0)
            {
                Debug.Log("게임 실행");
                startingAnim = GetComponent<Animator>();
                startingAnim.SetBool("StartLoad", true);
                start.SetActive(false);
                load.SetActive(false);
                quit.SetActive(false);
                quitAnim.SetActive(false);
                
            }
            else if (index == 1)
            {
                Debug.Log("게임 로드");
            }
            else
            {
                Debug.Log("게임 종료");
                Application.Quit();
            }
        }
    }
}
