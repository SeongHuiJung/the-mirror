using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : AutoAction
{
    [SerializeField]
    GameObject target;
    [SerializeField]
    bool InteractAfterEnd;

    public override void Action()
    {
        StartCoroutine(MoveToward(gameObject.transform.position));
    }

    IEnumerator MoveToward(Vector3 destination)
    {
        MoveScript moveScript = target.GetComponent<MoveScript>();
        Vector3 startPosition = target.transform.position;
        float diffX = destination.x - startPosition.x;
        float diffY = destination.y - startPosition.y;
        if (diffX == 0 || diffY == 0)//0을 0으로 나누는 현상 방지
        {
            StopCoroutine(MoveToward(destination));//빠져나감
        }
        float dirX = diffX / Mathf.Abs(diffX);
        float dirY = diffY / Mathf.Abs(diffY);

        float distanceX = Mathf.Abs(diffX);
        float distanceY = Mathf.Abs(diffY);

        while ( Mathf.Abs(target.transform.position.x - startPosition.x) < distanceX)
        {
            moveScript.ActiveMove(dirX, 0);

            yield return new WaitForSeconds(Time.deltaTime / 2);
        }
        while (Mathf.Abs(target.transform.position.y - startPosition.y) < distanceY)
        {
            moveScript.ActiveMove(0, dirY);
            yield return new WaitForSeconds(Time.deltaTime / 2);
        }

        if (InteractAfterEnd)
        {
            InteractAndFinish();
        }
        yield return new WaitForSeconds(1.0f);
    }

    void InteractAndFinish() //이동 다 마치고 interact
    {
        GameManager.dialogManager.StartConversation();
    }
}
