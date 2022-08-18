using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    //�ν����� ���� ���� ������
    [SerializeField] int id;    //������ ��� index
    [SerializeField] int index;
    int preIndex = 0; // �ǵ��ư� ��� index

    [SerializeField]
    string Characterid; //ĳ���� id

    [SerializeField]
    GameObject speech_bubble_prefab; //��ǳ�� prefab
    [SerializeField] GameObject selected_Prefab;
    string path = "Assets\\script.CSV";

    GameObject speech_bubble_object;
    GameObject selectedObject;
    static float axis_celibration = 0.015625f; // 1 / ppu

    SpriteRenderer renderer; //ĳ���� ��������Ʈ
    bool isTalking = false;
    bool isConversationCourintRunning = false;
    bool isTalkFaster = false;
    public bool bedSettutorialindex;
    public bool isDeleteSelect;

    CSVReader reader;
    const float bubbleContentHeight = 40; //��ǳ�� �ؽ�Ʈ �κ� ����ũ��
    const float bubbleHeight = 60; //��ǳ�� ���� ũ��
    float textSpeed;

    List<int> impossibleFaster = new List<int>() { 5 };

    public void Awake()
    {
        reader = new CSVReader();

    }
    public void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isTalkFaster && Input.GetKeyDown("e"))
            textSpeed = 0.01f;
    }

    public void BuildSpeechBubbleObject()
    {
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + (renderer.sprite.rect.size.y * gameObject.transform.localScale.y / 2 + 50) * axis_celibration, 0); //��ǳ�� ���� ����
        Vector3 rot = new Vector3(0, 0, 0);
        speech_bubble_object = Instantiate(speech_bubble_prefab, pos, Quaternion.Euler(rot), null);
    }

    public void BuildSpeechBubbleObject(GameObject talker)
    {
        renderer = talker.GetComponent<SpriteRenderer>();
        Vector3 pos = new Vector3(talker.transform.position.x, talker.transform.position.y + (renderer.sprite.rect.size.y * talker.transform.localScale.y / 2 + 50) * axis_celibration, 0); //��ǳ�� ���� ����
        speech_bubble_object.transform.position = pos;
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
        string dialogNo = reader.GetDialogNo(index);
        string characterid = reader.GetCharacterid(index);

        while (script != "" && (reader.GetDialogNo(index) == dialogNo || reader.GetDialogNo(index) == "100" || preIndex + 1 == index))  //��ȭ id �޶��� ������
        {
            dialogNo = reader.GetDialogNo(index);

            if (!characterid.Equals(reader.GetCharacterid(index)))  //ĳ���� ��ġ�� ��ǳ�� �̵�
                BuildSpeechBubbleObject(GameObject.FindWithTag(GetCharacter(Convert.ToInt32(reader.GetCharacterid(index)))));
            characterid = reader.GetCharacterid(index);

            rectTransform.sizeDelta = new Vector2(CalculateSizeInPixel(script), bubbleHeight) * axis_celibration; //��ǳ�� ��� ����
            for (int i = 1; i < speech_bubble_object.transform.childCount; i++)
            {
                Transform child = speech_bubble_object.transform.GetChild(i); //�ڽ� ������Ʈ �Ѱ�
                                                                              //��ǳ�� ������ �κ��� ����ó��

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

            textMesh.text = "";
            textSpeed = 0.1f;
            isTalkFaster = false;
            yield return new WaitForSeconds(0.05f);
            for (int i = 0; i < script.Length; i++) //��� Ÿ��ó�� ���
            {
                textMesh.text += script[i];
                if (reader.GetSelected(index).Equals("")) // �������� ���� ��ȭ�� ������
                    isTalkFaster = true;
                yield return new WaitForSecondsRealtime(textSpeed);
            }

            //��ȭ ������ ���� ���⼭ �б�

            if (selected_Prefab != null && !reader.GetSelected(index).Equals(""))   //������ ����
            {
                string[] selectDialog = reader.GetSelected(index).Split("&");
                int maxlength = 0;
                for (int i = 0; i < selectDialog.Length; i++)
                    maxlength = Math.Max(maxlength, CalculateSizeInPixel(selectDialog[i]));

                //��ǳ�� �̹��� ũ�� �߰�����
                selectedObject = Instantiate(selected_Prefab, new Vector3(rectTransform.position.x + rectTransform.sizeDelta.x / 2 + 1f, speech_bubble_object.transform.position.y + 0.1f, 0), Quaternion.identity);

                if (!reader.GetImpossibleIndex(index).Equals("")) // ������ �Ұ� ���
                    selectedObject.GetComponent<SleepManager>().impossibleindex = Convert.ToInt32(reader.GetImpossibleIndex(index));

                for (int i = 0; i < selectDialog.Length; i++)
                {
                    selectedObject.transform.GetChild(i).GetComponent<RectTransform>().sizeDelta = new Vector2((maxlength - 40) * axis_celibration, 0.3f);
                    selectedObject.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = selectDialog[i];
                }
            }

            yield return new WaitForSeconds(0.5f); //��� 2�� �ѹ��� �Ѿ�°� ����
            while (!Input.GetKeyDown(KeyCode.E) && !Input.GetKeyDown(KeyCode.Return)) //��ư ���������� ��ٸ�
            {
                yield return new WaitForSeconds(Time.deltaTime);
            }

            TutorialBed();

            if (isDeleteSelect) // ������ ����
            {
                Destroy(selectedObject);
                isDeleteSelect = false;
            }

            if (!reader.GetChangeId(index).Equals(""))  //�ε��� ����
                index = Convert.ToInt32(reader.GetChangeId(index)) - 2;

            if (reader.GetDialogNo(index) == "100") //��� �ǵ��ư���
                index = preIndex;

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
                size += 17;
            else
                size += 8;

        }
        return size;
    }

    void TutorialBed()
    {
        if (bedSettutorialindex)
        {
            preIndex = index - 1;
            index = -1;
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

    String GetCharacter(int id)
    {
        switch (id % 4)
        {
            case 1:
                return "Player";
            case 2:
                return "Formal";
            case 3:
                return "Headmaster";
            case 4:
                return "Normal";
            default:
                return "";
        }
    }
}