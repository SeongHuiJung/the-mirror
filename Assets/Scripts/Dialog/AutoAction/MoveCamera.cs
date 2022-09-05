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
    float distance; //거리
    Vector3 cameraPosition;
    Vector3 dirVector; //벡터
    bool isMoving = false;

    public override void Action()
    {
        Debug.Log("얍");
        StartCoroutine(StartMove());
    }

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraPosition = camera.transform.position;
        distance = Vector3.Distance(destination, cameraPosition);
        dirVector = destination - cameraPosition; //처음위치->목적지
    }

    IEnumerator Move(GameObject target, Vector3 dir, float d)
    {
        float tmpDistance = 0.0f; //지금까지 이동거리
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

    IEnumerator StartMove() //두 코루티이 동시에 실행되는거 방지
    {
        StartCoroutine(Move(camera, dirVector, distance));
        yield return new WaitForSeconds(remainTime + moveTime);
        StartCoroutine(Move(camera, -dirVector, distance));
    }
}
