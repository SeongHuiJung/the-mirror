using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class TitleScript : MonoBehaviour
{
    public Animator startingAnim;
    public GameObject start;
    public GameObject load;
    public GameObject quit;
    public GameObject quitAnim;
    public void StartGame()//���� ����
    {
        Debug.Log("���� ����");
        start.SetActive(false);
        load.SetActive(false);
        quit.SetActive(false);
        quitAnim.SetActive(false);
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
