using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SleepManager : MonoBehaviour
{
    public GameObject dayImage_prefab;
    GameObject today;

    public List<Sprite> dayImageList;

    DialogManager dialogManager;
    SelecteMoveScript selecteMoveScript;

    public void ActivateSleep()
    {
        dialogManager = GameManager.dialogManager;
        selecteMoveScript = FindObjectOfType<SelecteMoveScript>();

        selecteMoveScript.selectActionList = new List<Action>();
        selecteMoveScript.selectActionList.Add(YesSleep);
        selecteMoveScript.selectActionList.Add(NoSleep);
        selecteMoveScript.maxindex = 1;
    }

    void YesSleep()
    {
        today = Instantiate(dayImage_prefab);

        dialogManager.DestroyBubble();
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);

        StartCoroutine(Fadeout());
    }

    void NoSleep()
    {
        dialogManager.isDeleteSelect = true;
    }

    IEnumerator Fadeout()
    {
        Image image = today.transform.GetChild(0).GetComponent<Image>();
        Color color = image.color;

        while (color.a < 1)
        {
            color.a += 0.1f;
            image.color = color;
            yield return new WaitForSeconds(0.1f);
        }

        image.sprite = dayImageList[++FindObjectOfType<GameManager>().day - 2];
        image.color = new Color(1, 1, 1, 1);

        StartCoroutine(DeleteDayImage());
    }

    IEnumerator DeleteDayImage()
    {
        yield return new WaitForSecondsRealtime(1f);

        Destroy(today);
    }
}