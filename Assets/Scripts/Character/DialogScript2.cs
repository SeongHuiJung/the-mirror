using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
public class CSVReader
{

    const string path = "Assets\\script.CSV";
    bool isLoaded = false;//���� �ε� Ȯ��
    int lines = 0;//�� �� ���
    //ID�� �׳� csv ���Ͽ��� ���, �ҷ������� ���� ����
    List<string> names;//ĳ���� �̸� ���
    List<string> contents;//��� ���


    public CSVReader()
    {
        StreamReader reader = new StreamReader(path);

        //�ν��Ͻ� ����
        names = new List<string>();
        contents = new List<string>();

        string line = reader.ReadLine(); //�� ���� �н�
        line = reader.ReadLine();
        while (line != null)
        {
            string[] items = line.Split(",");
            names.Add(items[1]);
            contents.Add(items[2]);
            line = reader.ReadLine();//�̰� ��� ���ѹݺ� �߻�;;
            lines++;
        }
        isLoaded = true;

    }

    public int GetLine()
    {
        return lines;
    }
    public bool CheckInvalidIndex(int index)
    {
        if (lines <= index || index < 0)
        {
            return true;
        }
        else
            return false;
    }
    public string GetName(int index)
    {
        if (CheckInvalidIndex(index))
            return "";
        return names[index];
    }
    public string GetContent(int index)
    {
        if (CheckInvalidIndex(index))
            return "";
        return contents[index];
    }
    public bool IsLoaded()
    {
        return isLoaded;
    }


}


public class DialogScript2 : MonoBehaviour
{
    //�ν����� ���� ���� ������
    [SerializeField]
    int id; //������ ��� ID

    [SerializeField]
    string name;//ĳ���� �̸�

    [SerializeField]
    GameObject speech_bubble_prefab; //��ǳ�� prefab
    GameObject speech_bubble_object;

    SpriteRenderer renderer; //ĳ���� ��������Ʈ
    bool isTalking = false;
    bool isConversationCourintRunning = false;


    CSVReader reader;
    // Start is called before the first frame update
    void Start()
    {
        reader = new CSVReader();
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void BuildSpeechBubbleObject()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + renderer.sprite.rect.size.y * transform.localScale.y / 2 + 50, 0); //��ǳ�� ���� ����
        Vector3 rot = new Vector3(0, 0, 0);
        speech_bubble_object = Instantiate(speech_bubble_prefab, pos, Quaternion.Euler(rot), null);
    }
    void StartConversation()
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
    void EndConversation()
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
    IEnumerator Conversation(GameObject speech_bubble_object)
    {
        isConversationCourintRunning = true;

        RectTransform rectTransform = speech_bubble_object.GetComponent<RectTransform>(); //��ǳ�� transform


        //��� ��� ����
        TextMeshProUGUI textMesh = speech_bubble_object.transform.GetChild(2).GetComponent<TextMeshProUGUI>(); //��ǳ���� �ؽ�Ʈ����
        int index = id; //ID���� Conversation ����

        index -= 1; //�����δ� ù��° ���Ұ� ID 1 �̹Ƿ� id���� �ϳ� ���� ������ ����

        string script = reader.GetContent(index);


        while (script != "" && reader.GetName(index) == name)
        {
            //ĳ���� �̸��� �޶��� �� ���� ����
            textMesh.text = script; //��� ���
            rectTransform.sizeDelta = new Vector2(CalculateSize(script), 50); //��ǳ�� ��� ����
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
                    child.GetComponent<RectTransform>().sizeDelta = new Vector2(CalculateSize(script) - 30, 50); //���� ���� 15�̾�� �ϹǷ� 30 ����
                }
                else
                {
                    child.GetComponent<RectTransform>().sizeDelta = new Vector2(CalculateSize(script), 50); //��ǳ�� ��� ����
                }
            }
            yield return new WaitForSeconds(0.2f); //��� 2�� �ѹ��� �Ѿ�°� ����
            while (!Input.GetMouseButtonDown(0)) //��ư ���������� ��ٸ�
            {
                yield return new WaitForSeconds(Time.deltaTime);
            }
            index++;
            script = reader.GetContent(index);
        }

        //�б⹮�� �ʿ��ϸ� ���߿� ���� ����

        //��ǳ�� ����

        EndConversation();
        isConversationCourintRunning = false;
    }

    //��� �����ϴ� �Լ� �ʿ�
    int CalculateSize(string s)
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartConversation();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EndConversation();
        }
    }

}
