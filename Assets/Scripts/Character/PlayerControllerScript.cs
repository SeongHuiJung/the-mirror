using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    
    [SerializeField]
    float playerMaxSpeed;               // �÷��̾ �ִ�� ���� �ӵ�
    [SerializeField]
    float shiftMultiplyRate;            //����Ʈ ������ ���� �������°�?
    [SerializeField]
    float playerAcceleration;           // ��ø��? �÷��̾� �̵� ���ӵ�
    int playerState = 0;                // �ִϸ��̼ǿ� �ֱ� ���� �÷��̾� ������ ����
    Vector3 speed;                      // �÷��̾� ����ӵ�
    float directionX = 0;                  // ���� �����̴� ����
    float directionY = 0;               //���� �����̴� ����
    [SerializeField]
    RuntimeAnimatorController[] playerAnimationontrollerList; //��ȭ�¿� �ִϸ��̼�
    [SerializeField]
    float breakThreshold;               // �� �ӵ� ���ϸ� �ƿ� ����
    [SerializeField]
    float animationBreakThreshold;      //���ϸ��̼ǿ� �ش�
    Animator playerAnimationController; //�������� �ִϸ��̼� ��Ʈ�ѷ�
    ScreenCoordinateCorrector corrector; //��ǥ ������
    GameObject interactor; //��ȣ�ۿ� ���� �ݶ��̴�
    InteractManageer interactManageer;//��ȣ�ۿ� ��ũ��Ʈ
    float previousSpeedX = 0.0f;//���� ���� �ӵ�
    float previousSpeedY = 0.0f;//���� ���� �ӵ�
    bool isShiftPressed = false; //���� �޸��� ���� Ư�� �ൿ�� �� �� ���� ���� ���
    float spriteWidthInUnit;
    float spriteHeightInUnit;



    // Start is called before the first frame update
    private void Awake()
    {
        corrector = new ScreenCoordinateCorrector();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidthInUnit = corrector.convertToUnit(spriteRenderer.sprite.rect.size.x); 
        spriteHeightInUnit = corrector.convertToUnit(spriteRenderer.sprite.rect.size.y);
        interactor = transform.GetChild(0).gameObject;
        interactManageer = interactor.GetComponent<InteractManageer>();
    }
    void Start()
    {
        speed = new Vector3(0, 0, 0);
        playerAnimationController = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInteractEvent();
        isShiftPressed = Input.GetKey(KeyCode.LeftShift);
    }

    private void ProcessInteractEvent()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactManageer.Interact(0);
        }
    }

    private void FixedUpdate()
    {
        float axisHorizontal = Input.GetAxis("Horizontal");
        float axisVertical = Input.GetAxis("Vertical");
        Move(axisHorizontal, axisVertical);
        UpdateAnimationParameter();
        //rotation
        /*
        if(axisHorizontal * axisVertical == 0 || axisHorizontal * previousSpeedX <= 0 || axisHorizontal * previousSpeedX <= 0)
        {
            Debug.Log(axisHorizontal);
            if(axisHorizontal * previousSpeedX <= 0 && axisHorizontal != 0 || (axisVertical == 0 && axisHorizontal != 0))
            {

                RotateHorizontal(Convert.ToInt32(axisHorizontal / Mathf.Abs(axisHorizontal)));
            }
            if(axisHorizontal * previousSpeedX <= 0 && axisVertical != 0 || (axisHorizontal == 0 && axisVertical != 0))
            {
                RotateVertical(Convert.ToInt32(axisVertical / Mathf.Abs(axisVertical)));
            }
            //��ΰ� 0�϶��� �׳� �÷��̾� ȸ�� �״��
        }
        */
        
            float absAxisHorizontal = Mathf.Abs(axisHorizontal);
            float absAxisVertical = Mathf.Abs(axisVertical);
            if (absAxisHorizontal > absAxisVertical && axisHorizontal != 0)
            {
                Debug.Log("Updated2");
                RotateHorizontal(Convert.ToInt32(axisHorizontal / Mathf.Abs(axisHorizontal)));
            }
            if (absAxisVertical > absAxisHorizontal && axisVertical != 0)
            {
                Debug.Log("Updated");
                RotateVertical(Convert.ToInt32(axisVertical / Mathf.Abs(axisVertical)));

            }
        

        previousSpeedX = axisHorizontal;
        previousSpeedY = axisVertical;

    }
    //�Է� �޾Ƽ� �÷��̾� �����̴� �Լ� (�ӵ� ��ǥ�� ���ϴ� ������� �Ұ�)
    private void Move(float x, float y)
    {

        directionX = x;
        directionY = y;

        //���ο� �ӵ� ���
        Vector3 newSpeed = new Vector3(directionX,directionY,0);

        //�ӵ� ����Ѱ� ������ state ����, ���� �ӵ��� ��ȭ ����
        UpdateState(speed.x , newSpeed.x,speed.y, newSpeed.y); 

        //ĳ���� �ӵ� �ݿ�
        transform.position += newSpeed * playerMaxSpeed * Time.deltaTime;
        if (isShiftPressed)
            transform.position += newSpeed * playerMaxSpeed * Time.deltaTime * (Convert.ToInt16(isShiftPressed) * shiftMultiplyRate);

        /*
        if (Mathf.Abs(newSpeed) >= breakThreshold) 
            gameObject.transform.position += (speed * Time.deltaTime);
        */

            //���� �ӵ� ������Ʈ
        speed = newSpeed;
        
    }
    void RotateVertical(int direction)//�÷��̾� �����̴� ���⿡ ���� ȸ������ �Լ�
    {
        //ĳ���� ȸ��
        int index = (int)(0.5 * direction + 0.5f);
        playerAnimationController.runtimeAnimatorController = playerAnimationontrollerList[index];
        //�ݶ��̴� ȸ��
        interactor.transform.localPosition = new Vector3(0, (spriteHeightInUnit / 2  + interactor.transform.localScale.y)*direction , 0);
       
    }
    void RotateHorizontal(int direction)
    {
        int index = (int)(2 + 0.5 * direction + 0.5f);
        playerAnimationController.runtimeAnimatorController = playerAnimationontrollerList[index];
        interactor.transform.localPosition = new Vector3((spriteWidthInUnit / 2   + interactor.transform.localScale.x )*direction, 0, 0);
    }

    //�ӵ� ����ؼ� �޸�����, ���ߴ� ������, ���ߴ��� ���� ǥ��
    void UpdateState(float previousSpeedX, float newSpeedX, float previousSpeedY, float newSpeedY)
    {
        float deltaSpeedX = Mathf.Abs(newSpeedX) - Mathf.Abs(previousSpeedX); //�ӵ� ũ�� ����
        float deltaSpeedY = Mathf.Abs(newSpeedY) - Mathf.Abs(previousSpeedX);
        //if (speed.x <= animationBreakThreshold && speed.x >= -animationBreakThreshold)
        if(newSpeedX == 0 && newSpeedY == 0)
        {
            playerState = 0;
        }
        else if (newSpeedX > 0.0f && newSpeedX < 1.0f
              && newSpeedY > 0.0f && newSpeedY < 1.0f
            && (deltaSpeedX < 0 || deltaSpeedY < 0))
        {
            if( deltaSpeedY * deltaSpeedX == 0)
                playerState = 2;
            
        }
       else //deltaSpeed > 0
        {
            playerState = 1;
            //���⿡ ���� ĳ���� �¿� ����
            //if (Input.GetAxis("Horizontal") != 0)
            //    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, speed.x < 0 ? 180f : 0, 0));

        }

    }
    //���������� �ִ� state�� �ִϸ��̼� ��Ʈ�ѷ� �Ķ���ͷ� �����ϴ� �Լ�
    void UpdateAnimationParameter()
    {
        playerAnimationController.SetInteger("state", playerState);
    }
}
