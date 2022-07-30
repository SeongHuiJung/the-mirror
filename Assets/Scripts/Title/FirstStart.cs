using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStart : MonoBehaviour
{

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject warning;
    [SerializeField] private GameObject warningMessage;
    [SerializeField] private GameObject fadeOut;
    [SerializeField] private GameObject fadeIn;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("FirstStart") == 0) //���� ù ����� ��� ���
        {
            PlayerPrefs.SetInt("FirstStart", 1);
            Debug.Log("��� ���");

            Invoke("stopWarning",18f); //corontine ���� ���� �ʿ�
            PlayerPrefs.Save();
        }
        else //���� ���� �� ��° ���ʹ� ��� ���x
        {
            Debug.Log("��� ��� x");
            canvas.GetComponent<Animator>().enabled = false;
            warning.SetActive(false);
            warningMessage.SetActive(false);
            fadeOut.SetActive(false);
            fadeIn.SetActive(false);

        }
    }
    void stopWarning()
    {
        Debug.Log("����");
        warning.SetActive(false);
        warningMessage.SetActive(false);
        fadeOut.SetActive(false);
        fadeIn.SetActive(false);
        CancelInvoke("stopWarning");
    }
}
