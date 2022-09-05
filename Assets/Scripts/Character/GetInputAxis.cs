using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInputAxis : MonoBehaviour //�ܼ��� Ű���� �Է��� �÷��̾�� ����
{

    PlayerControllerScript playerControllerScript; //�÷��̾ �پ��ִ� ������Ʈ

    void Start()
    {
        playerControllerScript = GameManager.playerControllerScript;
    }

    // Update is called once per frame
    void Update()
    {
        playerControllerScript.SetHorizontalAxis(Input.GetAxis("Horizontal"));
        playerControllerScript.SetVerticalAxis(Input.GetAxis("Vertical"));
        playerControllerScript.SetShiftPressed(Input.GetKey(KeyCode.LeftShift));
    }
}
