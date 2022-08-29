using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveScript : MonoBehaviour
{
    [SerializeField]
    protected float playerMaxSpeed;               // �÷��̾ �ִ�� ���� �ӵ�
    [SerializeField]
    protected float shiftMultiplyRate;            //����Ʈ ������ ���� �������°�?
    protected Animator playerAnimationController; //�������� �ִϸ��̼� ��Ʈ�ѷ�
    protected ScreenCoordinateCorrector corrector; //��ǥ ������
    protected GameObject interactor; //��ȣ�ۿ� ���� �ݶ��̴�
    protected SpriteRenderer spriteRenderer;

    protected float previousSpeedX = 0.0f;//���� ���� �ӵ�
    protected float previousSpeedY = 0.0f;//���� ���� �ӵ�
    protected bool isShiftPressed = false; //���� �޸��� ���� Ư�� �ൿ�� �� �� ���� ���� ���
    protected float spriteWidthInUnit;
    protected float spriteHeightInUnit;
    [SerializeField] protected int playerState = 0; // �ִϸ��̼ǿ� �ֱ� ���� �÷��̾� ������ ����
    protected Vector3 speed;                      // �÷��̾� ����ӵ�

    protected float directionX = 0;                  // ���� �����̴� ����
    protected float directionY = 0;               //���� �����̴� ����

    [SerializeField]
    protected RuntimeAnimatorController[] playerAnimationontrollerList; //��ȭ�¿� �ִϸ��̼�

    public void ActiveMove(float axisHorizontal, float axisVertical, bool flip)
    {
        Move(axisHorizontal, axisVertical);
        UpdateAnimationParameter();

        if (axisHorizontal > 0)
            spriteRenderer.flipX = flip;
        else if (axisHorizontal < 0)
            spriteRenderer.flipX = !flip;

        float absAxisHorizontal = Mathf.Abs(axisHorizontal);
        float absAxisVertical = Mathf.Abs(axisVertical);

        DistinguishRotate(axisHorizontal, axisVertical);

        previousSpeedX = axisHorizontal;
        previousSpeedY = axisVertical;
    }

    //�Է� �޾Ƽ� �÷��̾� �����̴� �Լ� (�ӵ� ��ǥ�� ���ϴ� ������� �Ұ�)
    protected void Move(float x, float y)
    {
        directionX = x;
        directionY = y;

        //���ο� �ӵ� ���
        Vector3 newSpeed = new Vector3(directionX, directionY, 0);

        //�ӵ� ����Ѱ� ������ state ����, ���� �ӵ��� ��ȭ ����
        UpdateState(speed.x, newSpeed.x, speed.y, newSpeed.y);

        //ĳ���� �ӵ� �ݿ�
        if (isShiftPressed)
            transform.position += newSpeed * playerMaxSpeed * Time.deltaTime * (Convert.ToInt16(isShiftPressed) * shiftMultiplyRate);
        else
            transform.position += newSpeed * playerMaxSpeed * Time.deltaTime;
        /*
        if (Mathf.Abs(newSpeed) >= breakThreshold) 
            gameObject.transform.position += (speed * Time.deltaTime);
        */

        //���� �ӵ� ������Ʈ
        speed = newSpeed;
    }

    //���������� �ִ� state�� �ִϸ��̼� ��Ʈ�ѷ� �Ķ���ͷ� �����ϴ� �Լ�
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

    protected void RotateVertical(int direction)//�÷��̾� �����̴� ���⿡ ���� ȸ������ �Լ�
    {
        //ĳ���� ȸ��
        int index = (int)(0.5 * direction + 0.5f);
        playerAnimationController.runtimeAnimatorController = playerAnimationontrollerList[index];
        //�ݶ��̴� ȸ��
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

    //�ӵ� ����ؼ� �޸�����, ���ߴ� ������, ���ߴ��� ���� ǥ��
    protected void UpdateState(float previousSpeedX, float newSpeedX, float previousSpeedY, float newSpeedY)
    {
        float deltaSpeedX = Mathf.Abs(newSpeedX) - Mathf.Abs(previousSpeedX); //�ӵ� ũ�� ����
        float deltaSpeedY = Mathf.Abs(newSpeedY) - Mathf.Abs(previousSpeedX);
        //if (speed.x <= animationBreakThreshold && speed.x >= -animationBreakThreshold)
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
            //���⿡ ���� ĳ���� �¿� ����
            //if (Input.GetAxis("Horizontal") != 0)
            //    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, speed.x < 0 ? 180f : 0, 0));
        }
    }
}