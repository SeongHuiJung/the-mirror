using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TeleportEventReceiver : InteractionEvent
    
{
    string locationCSVFile = "Assets\\location.csv";
    GameObject player;
    PlayerControllerScript playerControllerScript;

    public override void Approach(GameObject player)
    {
    }

    public override void Interact(GameObject player)
    {
        player = GameManager.player;
        playerControllerScript = GameManager.playerControllerScript;
        StartCoroutine(Teleport(player));
    }

    public override void StopInteract(GameObject player)
    {
    }

    IEnumerator Teleport(GameObject player)
    {
        if (playerControllerScript.isImpossibleMove)
            playerControllerScript.isImpossibleMove = true;
        GameObject mainCameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        CameraScreenEffectManager cameraScreenEffectManager = mainCameraObject.GetComponent<CameraScreenEffectManager>();
        cameraScreenEffectManager.ActivateEffect(0);
        StreamReader reader = new StreamReader(locationCSVFile);
        for (int i = 0; i < 1; i++)
            reader.ReadLine();
        string line = reader.ReadLine(); //id�� 1������ ����
        int x = Convert.ToInt32(line.Split(',')[0]);
        int y = Convert.ToInt32(line.Split(',')[1]);
        yield return new WaitForSeconds(0.5f); //�̰� ȿ�� �ð��̶� �����ؾ���.
        player.transform.position = new Vector2(x, y);
        playerControllerScript.isImpossibleMove = false;
    }
}
