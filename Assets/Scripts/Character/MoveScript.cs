using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveScript : MonoBehaviour
{
    [SerializeField]
    protected float playerMaxSpeed;               // 플레이어가 최대로 내는 속도
    [SerializeField]
    protected float shiftMultiplyRate;            //쉬프트 누르면 몇배로 빨라지는가?
    public Animator playerAnimationController; //걸음걸이 애니메이션 컨트롤러
    protected ScreenCoordinateCorrector corrector; //좌표 보정용
    protected GameObject interactor; //상호작용 범위 콜라이더
    protected SpriteRenderer spriteRenderer;

    protected float previousSpeedX = 0.0f;//이전 가로 속도
    protected float previousSpeedY = 0.0f;//이전 가로 속도
    protected bool isShiftPressed = false; //만약 달리는 도중 특정 행동을 할 수 없을 때에 대비
    protected float spriteWidthInUnit;
    protected float spriteHeightInUnit;
    [SerializeField] protected int playerState = 0; // 애니메이션에 넣기 위한 플레이어 움직임 상태
    protected Vector3 speed;                      // 플레이어 현재속도

    protected float directionX = 0;                  // 가로 움직이는 방향
    protected float directionY = 0;               //세로 움직이는 방향
    [SerializeField] bool flipX;

    [SerializeField]
    protected RuntimeAnimatorController[] playerAnimationontrollerList; //상화좌우 애니메이션

    public void ActiveMove(float axisHorizontal, float axisVertical)
    {
        Move(axisHorizontal, axisVertical);
        UpdateAnimationParameter();

        if (axisHorizontal > 0)
            spriteRenderer.flipX = flipX;
        else if (axisHorizontal < 0)
            spriteRenderer.flipX = !flipX;

        float absAxisHorizontal = Mathf.Abs(axisHorizontal);
        float absAxisVertical = Mathf.Abs(axisVertical);

        DistinguishRotate(axisHorizontal, axisVertical);

        previousSpeedX = axisHorizontal;
        previousSpeedY = axisVertical;
    }

    //입력 받아서 움직이는 함수 (속도 좌표에 더하는 방식으로 할것)
    protected void Move(float x, float y)
    {
        directionX = x;
        directionY = y;

        //새로운 속도 계산
        Vector3 newSpeed = new Vector3(directionX, directionY, 0);

        //속도 계산한것 가지고 state 결정, 아직 속도가 변화 안함
        UpdateState(speed.x, newSpeed.x, speed.y, newSpeed.y);

        //캐릭터 속도 반영
        if (isShiftPressed)
            transform.position += newSpeed * playerMaxSpeed * Time.deltaTime * (Convert.ToInt16(isShiftPressed) * shiftMultiplyRate);
        else
            transform.position += newSpeed * playerMaxSpeed * Time.deltaTime;

        //기존 속도 업데이트
        speed = newSpeed;
    }

    //전역변수에 있는 state를 애니메이션 컨트롤러 파라미터로 전달하는 함수
    protected void UpdateAnimationParameter()
    {
        playerAnimationController.SetInteger("state", playerState);
    }

    protected void DistinguishRotate(float axisHorizontal, float axisVertical)
    {
        if (Mathf.Abs(axisHorizontal) > 0)
        {
            if (axisVertical >= 0)
                RotateHorizontal(Convert.ToInt32(axisHorizontal / Mathf.Abs(axisHorizontal)));
            else
                RotateVertical(Convert.ToInt32(axisVertical / Mathf.Abs(axisVertical)));
        }
        else if (Mathf.Abs(axisVertical) > 0)
            RotateVertical(Convert.ToInt32(axisVertical / Mathf.Abs(axisVertical)));
    }

    protected void RotateVertical(int direction)//플레이어 움직이는 방향에 따라 회전해줄 함수
    {
        //캐릭터 회전
        int index = (int)(0.5 * direction + 0.5f);
        playerAnimationController.runtimeAnimatorController = playerAnimationontrollerList[index];
        //콜라이더 회전
        if (interactor != null)
            interactor.transform.localPosition = new Vector3(0, (spriteHeightInUnit / 2 + interactor.transform.localScale.y) * direction, 0);
    }

    protected void RotateHorizontal(int direction)
    {
        int index = (int)(2 + 0.5 * direction + 0.5f);
        playerAnimationController.runtimeAnimatorController = playerAnimationontrollerList[2];
        if (interactor != null)
            interactor.transform.localPosition = new Vector3((spriteWidthInUnit / 2 + interactor.transform.localScale.x) * direction, 0, 0);
    }

    //속도 계산해서 달리는지, 멈추는 중인지, 멈추는지 상태 표시
    protected void UpdateState(float previousSpeedX, float newSpeedX, float previousSpeedY, float newSpeedY)
    {
        float deltaSpeedX = Mathf.Abs(newSpeedX) - Mathf.Abs(previousSpeedX); //속도 크기 차이
        float deltaSpeedY = Mathf.Abs(newSpeedY) - Mathf.Abs(previousSpeedX);

        if (newSpeedX == 0 && newSpeedY == 0)
        {
            playerState = 0;
        }
        else if (newSpeedX > 0.0f && newSpeedX < 1.0f
              && newSpeedY > 0.0f && newSpeedY < 1.0f
            && (deltaSpeedX < 0 || deltaSpeedY < 0))
        {
            if (deltaSpeedY * deltaSpeedX == 0)
                playerState = 2;
        }
        else //deltaSpeed > 0
        {
            if (isShiftPressed)
                playerState = 3;
            else
                playerState = 1;
        }
    }
}