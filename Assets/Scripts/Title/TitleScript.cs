using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    void Start()
    {

    }
    public void StartGame()//���� ����
    {
        Debug.Log("���� ����");
    }
    public void ExitGame()//���� ����
    {
        Debug.Log("���� ����");
        Application.Quit();
    } 
    public void DeleteFirstStartData()
    {
        PlayerPrefs.DeleteKey("FirstStart");
        Debug.Log("�׽�Ʈ��:���� ù ���� ������ ����");
    }
}
