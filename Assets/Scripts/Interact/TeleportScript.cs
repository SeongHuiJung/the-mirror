using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TeleportScript : InteractObjectScript
{
    [SerializeField] public Vector3 sponSpot;
    [SerializeField] public string sponMap;

    public override void Interact()
    {
        if (!sponMap.Equals(""))
            Debug.Log("∏  ¿Ãµø");
        else
            GameManager.playerControllerScript.transform.position = sponSpot;
    }
}