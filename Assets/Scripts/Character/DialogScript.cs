using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;


public class DialogScript : MonoBehaviour
{
    //�ν����� ���� ���� ������
    [SerializeField]
    int id; //������ ��� ID
    string path = "Assets\\script.CSV";

    [SerializeField]
    string name;//ĳ���� �̸�

    [SerializeField]
    GameObject speech_bubble_prefab; //��ǳ�� prefab

    SpriteRenderer renderer; //ĳ���� ��������Ʈ

    float axis_celibration = 0.01f; //��ǥ ������


    CSVReader reader;
    // Start is called before the first frame update
    void Start()
    {
        reader = new CSVReader(path);
        renderer = gameObject.GetComponent<SpriteRenderer>();
        StartConversation();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void StartConversation()
    {
        //��ǳ�� ����
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + renderer.sprite.rect.size.y/2 * axis_celibration + 50 * axis_celibration, 0);
        Vector3 rot = new Vector3(0, 0, 0);
        GameObject speech_bubble_object = Instantiate(speech_bubble_prefab, pos, Quaternion.Euler(rot), null);
       
        //Conversation �ڷ�ƾ ȣ��
        StartCoroutine(Conversation(speech_bubble_object));
        
    }
    void EndConversation(GameObject speech_bubble_object)
    {
        Destroy(speech_bubble_object);

    }
    IEnumerator Conversation(GameObject speech_bubble_object){
        
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
            rectTransform.sizeDelta = new Vector2(CalculateSize(script), 50  * axis_celibration); //��ǳ�� ��� ����
            for(int i=0; i<speech_bubble_object.transform.childCount; i++)
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
                    child.GetComponent<RectTransform>().sizeDelta = new Vector2((CalculateSize(script) - 30) * axis_celibration, 50 * axis_celibration); //���� ���� 15�̾�� �ϹǷ� 30 ����
                }
                else
                {
                    child.GetComponent<RectTransform>().sizeDelta = new Vector2(CalculateSize(script) * axis_celibration, 50 * axis_celibration); //��ǳ�� ��� ����
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
        EndConversation(speech_bubble_object);

    }

    //��� �����ϴ� �Լ� �ʿ�
    int CalculateSize(string s) //�ȼ� ���� ���
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


}
