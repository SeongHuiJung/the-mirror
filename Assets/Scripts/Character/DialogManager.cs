using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    //�ν����� ���� ���� ������
    [SerializeField]
    int id; //������ ��� ID
    [SerializeField] int index;

    [SerializeField]
    string name;//ĳ���� �̸�

    [SerializeField]
    GameObject speech_bubble_prefab; //��ǳ�� prefab
    [SerializeField] GameObject selected_Prefab;
    string path = "Assets\\script.CSV";

    GameObject speech_bubble_object;
    static float axis_celibration = 0.015625f; // 1 / ppu

    SpriteRenderer renderer; //ĳ���� ��������Ʈ
    bool isTalking = false;
    bool isConversationCourintRunning = false;
    public bool bedSettutorialindex;

    CSVReader reader;
    const float bubbleContentHeight = 40; //��ǳ�� �ؽ�Ʈ �κ� ����ũ��
    const float bubbleHeight = 60; //��ǳ�� ���� ũ��

    public void Awake()
    {
        reader = new CSVReader(path);

    }
    public void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }



    public void BuildSpeechBubbleObject()
    {
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + (renderer.sprite.rect.size.y * gameObject.transform.localScale.y / 2 + 50) * axis_celibration, 0); //��ǳ�� ���� ����
        Vector3 rot = new Vector3(0, 0, 0);
        speech_bubble_object = Instantiate(speech_bubble_prefab, pos, Quaternion.Euler(rot), null);
    }

    public void StartConversation()
    {
        if (isTalking)
            return;
        isTalking = true;
        //��ǳ�� ����
        BuildSpeechBubbleObject();

        //Conversation �ڷ�ƾ ȣ��
        if (!isConversationCourintRunning)
            StartCoroutine(Conversation(speech_bubble_object));

    }
    public void EndConversation()
    {
        if (!isTalking)
            return;


        if (isConversationCourintRunning)
        {
            StopCoroutine(Conversation(speech_bubble_object));
            isConversationCourintRunning = false;
        }
        Destroy(speech_bubble_object);
        isTalking = false;

    }
    public IEnumerator Conversation(GameObject speech_bubble_object)
    {
        isConversationCourintRunning = true;

        RectTransform rectTransform = speech_bubble_object.GetComponent<RectTransform>(); //��ǳ�� transform

        //��� ��� ����
        TextMeshProUGUI textMesh = speech_bubble_object.transform.GetChild(2).GetComponent<TextMeshProUGUI>(); //��ǳ���� �ؽ�Ʈ����
        index = id - 1; //ID���� Conversation ����, �����δ� ù��° ���Ұ� ID 1 �̹Ƿ� id���� �ϳ� ���� ������ ����

        string script = reader.GetContent(index);

        while (script != "" && reader.GetName(index) == name)
        {
            //ĳ���� �̸��� �޶��� �� ���� ����
            textMesh.text = script; //��� ���
            rectTransform.sizeDelta = new Vector2(CalculateSizeInPixel(script), bubbleHeight) * axis_celibration; //��ǳ�� ��� ����

            for (int i = 0; i < speech_bubble_object.transform.childCount; i++)
            {
                Transform child = speech_bubble_object.transform.GetChild(i); //�ڽ� ������Ʈ �Ѱ�
                                                                              //��ǳ�� ������ �κ��� ����ó��

                if (i == 0)
                {
                    continue;
                }
                Debug.Log(i);
                TextMeshProUGUI textbox = speech_bubble_object.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
                if (textbox) //�ؽ�Ʈ ���ڰ� ����ִ� ������Ʈ�϶�
                {
                    child.GetComponent<RectTransform>().sizeDelta = new Vector2(CalculateSizeInPixel(script) - 30, bubbleContentHeight) * axis_celibration; //���� ���� 15�̾�� �ϹǷ� 30 ����
                }
                else
                {
                    child.GetComponent<RectTransform>().sizeDelta = new Vector2(CalculateSizeInPixel(script), bubbleContentHeight) * axis_celibration; //��ǳ�� ��� ����
                }
            }
            //��ȭ ������ ���� ���⼭ �б�
            if (selected_Prefab != null && index == 5)  //ħ�� ���� â ����
                Instantiate(selected_Prefab, new Vector3(transform.position.x + 2.12578f, transform.position.y + 2, 0), Quaternion.identity);

            yield return new WaitForSeconds(0.05f); //��� 2�� �ѹ��� �Ѿ�°� ����
            while (!Input.GetKeyDown(KeyCode.E) && !Input.GetKeyDown(KeyCode.Return)) //��ư ���������� ��ٸ�
            {
                yield return new WaitForSeconds(Time.deltaTime);
            }

            TutorialBed();

            index++;
            script = reader.GetContent(index);
        }

        //�б⹮�� �ʿ��ϸ� ���߿� ���� ����

        //��ǳ�� ����
        EndConversation();
        isConversationCourintRunning = false;
        FindObjectOfType<PlayerControllerScript>().isImpossibleMove = false;
    }

    //��� �����ϴ� �Լ� �ʿ�
    int CalculateSizeInPixel(string s)
    {
        int size = 0;
        char[] values = s.ToCharArray();
        size += 50;//��+�� ����
        foreach (char c in values)
        {

            int value = Convert.ToInt32(c);
            if (value >= 0x80)
                size += 20;
            else
            {
                size += 7;
            }

        }
        return size;
    }

    void TutorialBed()
    {
        if (bedSettutorialindex)
        {
            index = 3;
            bedSettutorialindex = false;
        }
    }

    public void SetIndex(int _index)
    {
        index = _index;
    }

    public void SetId(int _id)
    {
        id = _id;
    }
    
    public void DestroyBubble()
    {
        Destroy(speech_bubble_object);
    }
}