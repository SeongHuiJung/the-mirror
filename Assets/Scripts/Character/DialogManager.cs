using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    //인스펙터 수정 가능 변수들
    [SerializeField] int id;    //시작할 대사 index
    [SerializeField] int index;

    [SerializeField]
    string Characterid; //캐릭터 id

    [SerializeField]
    GameObject speech_bubble_prefab; //말풍선 prefab
    [SerializeField] GameObject selected_Prefab;
    string path = "Assets\\script.CSV";

    GameObject speech_bubble_object;
    GameObject selectedObject;
    static float axis_celibration = 0.015625f; // 1 / ppu

    SpriteRenderer renderer; //캐릭터 스프라이트
    bool isTalking = false;
    bool isConversationCourintRunning = false;
    bool isTalkFaster = false;
    public bool bedSettutorialindex;
    public bool isDeleteSelect;

    CSVReader reader;
    const float bubbleContentHeight = 40; //말풍선 텍스트 부분 가로크기
    const float bubbleHeight = 60; //말풍선 세로 크기
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
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + (renderer.sprite.rect.size.y * gameObject.transform.localScale.y / 2 + 50) * axis_celibration, 0); //말풍선 높이 설정
        Vector3 rot = new Vector3(0, 0, 0);
        speech_bubble_object = Instantiate(speech_bubble_prefab, pos, Quaternion.Euler(rot), null);
    }

    public void BuildSpeechBubbleObject(GameObject talker)
    {
        renderer = talker.GetComponent<SpriteRenderer>();
        Vector3 pos = new Vector3(talker.transform.position.x, talker.transform.position.y + (renderer.sprite.rect.size.y * talker.transform.localScale.y / 2 + 50) * axis_celibration, 0); //말풍선 높이 설정
        speech_bubble_object.transform.position = pos;
    }

    public void StartConversation()
    {
        if (isTalking)
            return;
        isTalking = true;
        //말풍선 생성
        BuildSpeechBubbleObject();

        //Conversation 코루틴 호출
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

        RectTransform rectTransform = speech_bubble_object.GetComponent<RectTransform>(); //말풍선 transform

        //대사 출력 수행
        TextMeshProUGUI textMesh = speech_bubble_object.transform.GetChild(2).GetComponent<TextMeshProUGUI>(); //말풍선속 텍스트상자
        index = id - 1; //ID부터 Conversation 시작, 실제로는 첫번째 원소가 ID 1 이므로 id에서 하나 빼서 저장할 예정

        string script = reader.GetContent(index);
        string dialogNo = reader.GetDialogNo(index);
        string characterid = reader.GetCharacterid(index);


        while (script != "" && reader.GetDialogNo(index) == dialogNo)   //대화 id 달라질 때까지
        {
            dialogNo = reader.GetDialogNo(index);

            if (!characterid.Equals(reader.GetCharacterid(index)))  //캐릭터 위치로 말풍선 이동
                BuildSpeechBubbleObject(GameObject.FindWithTag(GetCharacter(Convert.ToInt32(reader.GetCharacterid(index)))));
            characterid = reader.GetCharacterid(index);

            rectTransform.sizeDelta = new Vector2(CalculateSizeInPixel(script), bubbleHeight) * axis_celibration; //말풍선 계산 수행
            for (int i = 0; i < speech_bubble_object.transform.childCount; i++)
            {
                Transform child = speech_bubble_object.transform.GetChild(i); //자식 오브젝트 한개
                                                                              //말풍선 세모모양 부분은 별도처리

                if (i == 0)
                {
                    continue;
                }
                TextMeshProUGUI textbox = speech_bubble_object.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
                if (textbox) //텍스트 상자가 들어있는 오브젝트일때
                {
                    child.GetComponent<RectTransform>().sizeDelta = new Vector2(CalculateSizeInPixel(script) - 30, bubbleContentHeight) * axis_celibration; //양쪽 여백 15이어야 하므로 30 차감
                }
                else
                {
                    child.GetComponent<RectTransform>().sizeDelta = new Vector2(CalculateSizeInPixel(script), bubbleContentHeight) * axis_celibration; //말풍선 계산 수행
                }
            }

            textMesh.text = "";
            textSpeed = 0.1f;
            isTalkFaster = false;
            yield return new WaitForSeconds(0.05f);
            for (int i = 0; i < script.Length; i++) //대사 타자처럼 출력
            {
                textMesh.text += script[i];
                if (reader.GetSelected(index).Equals("")) // 선택지가 없는 대화만 빠르게
                    isTalkFaster = true;
                yield return new WaitForSecondsRealtime(textSpeed);
            }

            //대화 종류에 따라 여기서 분기

            if (selected_Prefab != null && !reader.GetSelected(index).Equals(""))   //선택지 생성
            {
                string[] selectDialog = reader.GetSelected(index).Split("&");
                selectedObject = Instantiate(selected_Prefab, new Vector3(transform.position.x + 2f, transform.position.y + 2.15f, 0), Quaternion.identity);

                for (int i = 0; i < selectDialog.Length; i++)
                    selectedObject.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = " " + (i + 1) + ") " + selectDialog[i];
            }

            yield return new WaitForSeconds(0.5f); //대사 2개 한번에 넘어가는거 방지
            while (!Input.GetKeyDown(KeyCode.E) && !Input.GetKeyDown(KeyCode.Return)) //버튼 눌릴때까지 기다림
            {
                yield return new WaitForSeconds(Time.deltaTime);
            }

            if (isDeleteSelect) // 선택지 삭제
            {
                Destroy(selectedObject);
                isDeleteSelect = false;
            }

            TutorialBed();

            if (!reader.GetChangeId(index).Equals(""))  //인덱스 변경
                index = Convert.ToInt32(reader.GetChangeId(index)) - 2;

            index++;
            script = reader.GetContent(index);
        }

        //분기문은 필요하면 나중에 구현 예정

        //말풍선 삭제
        EndConversation();
        isConversationCourintRunning = false;
        FindObjectOfType<PlayerControllerScript>().isImpossibleMove = false;
    }

    //계산 수행하는 함수 필요
    int CalculateSizeInPixel(string s)
    {
        int size = 0;
        char[] values = s.ToCharArray();
        size += 50;//앞+뒤 여백
        foreach (char c in values)
        {

            int value = Convert.ToInt32(c);
            if (value >= 0x80)
                size += 17;
            else
                size += 10;

        }
        return size;
    }

    void TutorialBed()
    {
        if (bedSettutorialindex)
        {
            index = -1;
            bedSettutorialindex = false;
            Destroy(selectedObject);
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
                return "formal";
            case 3:
                return "headmaster";
            case 4:
                return "normal";
            default:
                return "";
        }
    }
}