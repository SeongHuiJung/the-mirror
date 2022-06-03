using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    [SerializeField]
    float playerMaxSpeed;               // �÷��̾ �ִ�� ���� �ӵ�
    [SerializeField]
    float playerAcceleration;           // ��ø��? �÷��̾� �̵� ���ӵ�
    int playerState = 0;                // �ִϸ��̼ǿ� �ֱ� ���� �÷��̾� ������ ����
    Vector3 speed;                      // �÷��̾� ����ӵ�
    int direction = 0;                  // �����̴� ����
    [SerializeField]
    float breakThreshold;               // �� �ӵ� ���ϸ� �ƿ� ����
    [SerializeField]
    float animationBreakThreshold;      //���ϸ��̼ǿ� �ش�
    Animator playerAnimationController; //�������� �ִϸ��̼� ��Ʈ�ѷ�



    // Start is called before the first frame update
    void Start()
    {
        speed = new Vector3(0, 0, 0);
        playerAnimationController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        UpdateAnimationParameter();
    }
    //�Է� �޾Ƽ� �÷��̾� �����̴� �Լ� (��������ó�� �ӵ� ��ǥ�� ���ϴ� ������� �Ұ�)
    private void Move()
    {
        //������ �̵�
        if (Input.GetKeyDown(KeyCode.D))
        {
            direction += 1;
        } else if (Input.GetKeyUp(KeyCode.D))
        {
            direction -= 1;
        }

        //���� �̵�
        if (Input.GetKeyDown(KeyCode.A))
        {
            direction -= 1;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            direction += 1;
        }
        //�ٷ� ���� �ӵ� �� ����
        float previousSpeed = speed.x * Time.deltaTime;
        
        //�ӵ� ������ ��ȭ���� �ִ� ���
        float beforeSpeed = 0; //�ִ� �ּڰ� ��� ���� ������ �ӵ� ��
        if (direction == 0)
        {
            beforeSpeed = speed.x +  ((speed.x < 0 ? 1 : -1) * playerAcceleration) * Time.deltaTime; //�ӵ��� 0�϶� ����ؼ� �ݴ� �������� ����, ���� �÷��̾ �̵����� �ӵ��� �ݴ� �������� ����
            
            if (beforeSpeed > 0)
                speed.x = Mathf.Clamp(beforeSpeed, 0, playerMaxSpeed); //���� ���������� �̵����̾����� ���� �������δ� ������
            else
                speed.x = Mathf.Clamp(beforeSpeed, -playerMaxSpeed, 0); //���� �������� �̵����̾����� ���������� �̵� ���ϰ�
        }
        else
        {
            beforeSpeed = speed.x +  (direction * playerAcceleration) * Time.deltaTime; //�̵� ���� �״�� ����
            speed.x = Mathf.Clamp(beforeSpeed, -playerMaxSpeed, playerMaxSpeed);
        }
        float newSpeed = (speed.x * Time.deltaTime); //������ �� �̼��� ���� ���� �뵵
        UpdateState(previousSpeed , newSpeed); //�ӵ� ����Ѱ� ������ state ����, ���� �ӵ��� ��ȭ ����
        if (Mathf.Abs(newSpeed) >= breakThreshold) 
            gameObject.transform.position += (speed * Time.deltaTime);
        
    }

    //�ӵ� ����ؼ� �޸�����, ���ߴ� ������, ���ߴ��� ���� ǥ��
    void UpdateState(float previousSpeed, float newSpeed)
    {
        float deltaSpeed = Mathf.Abs(newSpeed) - Mathf.Abs(previousSpeed); //�ӵ� ũ�� ����
        if (speed.x <= animationBreakThreshold && speed.x >= -animationBreakThreshold)
        {
            playerState = 0;
        }
        else if (deltaSpeed < 0)
        {
            playerState = 2;
            
        }
       else //deltaSpeed > 0
        {
            playerState = 1;
            
        }

    }
    //���������� �ִ� state�� �ִϸ��̼� ��Ʈ�ѷ� �Ķ���ͷ� �����ϴ� �Լ�
    void UpdateAnimationParameter()
    {
        playerAnimationController.SetInteger("state", playerState);
    }
}
