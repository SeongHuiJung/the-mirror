using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class TitleScript : MonoBehaviour
{
    public Animator startingAnim;
    public void StartGame()//���� ����
    {
        Debug.Log("���� ����");
        startingAnim = GetComponent<Animator>();
        startingAnim.SetBool("StartLoad", true);
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
