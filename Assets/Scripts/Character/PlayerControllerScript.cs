using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MoveScript
{
    [SerializeField]
    float playerAcceleration;           // 민첩도? 플레이어 이동 가속도
    
    [SerializeField]
    float breakThreshold;               // 이 속도 이하면 아예 멈춤
    [SerializeField]
    float animationBreakThreshold;      //에니매이션에 해당

    float axisHorizontal;//가로입력
    float axisVertical;//세로입력
    
    InteractManageer interactManageer;//상호작용 스크립트

    public bool isImpossibleMove;

    private void Awake()
    {
        corrector = new ScreenCoordinateCorrector();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidthInUnit = corrector.convertToUnit(spriteRenderer.sprite.rect.size.x); 
        spriteHeightInUnit = corrector.convertToUnit(spriteRenderer.sprite.rect.size.y);
        interactor = transform.GetChild(0).gameObject;
        interactManageer = interactor.GetComponent<InteractManageer>();
        playerAnimationController = GetComponent<Animator>();
        speed = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
            ProcessInteractEvent();
        //isShiftPressed = Input.GetKey(KeyCode.LeftShift);
    }
    
    //외부에서 자동 컨트롤을 위해 외부에서 axis 값 입력받는 함수들
    public void SetHorizontalAxis(float axis)
    {
        axisHorizontal = axis;
    }
    public void SetVerticalAxis(float axis)
    {
        axisVertical = axis;
    }
    public void SetShiftPressed(bool b)
    {
        isShiftPressed = b;
    }
    public void ProcessInteractEvent()
    {
        if (!isImpossibleMove)
        {
            interactManageer.Interact(0);
            playerAnimationController.SetInteger("state", 0);
            isImpossibleMove = true;
        }
    }

    void FixedUpdate()
    {
        if (!isImpossibleMove)
            ActiveMove(axisHorizontal, axisVertical);
    }

    private void DistinguishRotate(float axisHorizontal, float axisVertical)
    {
        if ((Input.GetKey("w") || Input.GetKey("s")) && (Input.GetKey("a") || Input.GetKey("d")) && axisVertical != 0)
        {
            if (axisVertical > 0)
                RotateHorizontal(Convert.ToInt32(axisHorizontal / Mathf.Abs(axisHorizontal)));
            else
                RotateVertical(Convert.ToInt32(axisVertical / Mathf.Abs(axisVertical)));
        }
        else if ((Input.GetKey("a") || Input.GetKey("d")) && axisHorizontal != 0)
            RotateHorizontal(Convert.ToInt32(axisHorizontal / Mathf.Abs(axisHorizontal)));
        else if ((Input.GetKey("w") || Input.GetKey("s")) && axisVertical != 0)
            RotateVertical(Convert.ToInt32(axisVertical / Mathf.Abs(axisVertical)));
    }
}