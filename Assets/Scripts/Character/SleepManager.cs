using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SleepManager : SelecteMoveScript
{
    public GameObject dayImage_prefab;
    GameObject today;

    public List<Sprite> dayImageList;

    public int impossibleindex;

    DialogManager dialogManager;

    private void Awake()
    {
        dialogManager = GameManager.player.GetComponent<DialogManager>();

        selectActionList = new List<Action>();
        selectActionList.Add(YesSleep);
        selectActionList.Add(NoSleep);
    }

    void Update()
    {
        Move(1);
        Select();
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
        Destroy(this.gameObject);
    }
}
