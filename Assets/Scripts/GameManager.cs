using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject inventory;
    public int day;

    public static GameObject player;
    public static PlayerControllerScript playerControllerScript;
    public static DialogManager dialogManager;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerControllerScript = player.GetComponent<PlayerControllerScript>();
        dialogManager = FindObjectOfType<DialogManager>();

        SelecteMoveScript.impossibleindex = -1;

        DontDestroyOnLoad(this.gameObject.transform.parent);
    }

    void Update()
    {
        if (inventory != null && Input.GetKeyDown("i"))
            inventory.SetActive(!inventory.activeSelf);
    }
}
