using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraScreenEffect : MonoBehaviour
{
    [SerializeField]
    protected int[] parameters; //ȿ�� �Ķ���� ���� ����
    [SerializeField]
    protected GameObject canvasPrefab;
    public CameraScreenEffect(int[] param, GameObject effectCanvasPrefab)
    {
        parameters = param;
        canvasPrefab = effectCanvasPrefab;
    }
    public abstract void Activate();
    public void SetParams(int[] param)
    {
        parameters = param;
    }
    public abstract void Deactive();

}
