using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : AutoAction
{
    [SerializeField]
    Vector3 destination;
    [SerializeField]
    float remainTime;
    [SerializeField]
    float moveTime;
    GameObject camera;
    float distance; //�Ÿ�
    Vector3 cameraPosition;
    Vector3 dirVector; //����
    bool isMoving = false;
    public override void Action()
    {
        Debug.Log("��");
        StartCoroutine(StartMove());
        

    }

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraPosition = camera.transform.position;
        distance = Vector3.Distance(destination, cameraPosition);
        dirVector = destination - cameraPosition; //ó����ġ->������
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Move(GameObject target, Vector3 dir, float d)
    {
        float tmpDistance = 0.0f; //���ݱ��� �̵��Ÿ�
        Debug.Log(d);
        while (tmpDistance < d)
        {
            
            float dt = Time.deltaTime;
            target.transform.position += dir / moveTime * dt;
            Debug.Log(dir);
            tmpDistance += dir.magnitude / moveTime * dt;
            yield return new WaitForSeconds(dt);
        }
        
    }
    IEnumerator StartMove() //�� �ڷ�Ƽ�� ���ÿ� ����Ǵ°� ����
    {
        StartCoroutine(Move(camera, dirVector, distance));
        yield return new WaitForSeconds(remainTime + moveTime);
        StartCoroutine(Move(camera, -dirVector, distance));
    }


}
