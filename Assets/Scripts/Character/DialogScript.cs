using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        while(line != null)
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


public class DialogScript : MonoBehaviour
{
    //�ν����� ���� ���� ������
    [SerializeField]
    int id; //������ ��� ID
    
    [SerializeField]
    string name;//ĳ���� �̸�

    /*
     index == -1 : StartConversation() �߰� �����ϰ� ù ������ ����(index = id�� �ٲٰ� Conversation())
     getContent == null�̰ų� �ι� ����ġ ���� ���� : ��ǳ�� ���ְ� index = -1
    ������ : Conversation()
    Conversation() : ��ǳ���� ��ȭ ����ϰ� index++

     */
    CSVReader reader;
    // Start is called before the first frame update
    void Start()
    {
        reader = new CSVReader();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void StartConversation()
    {

        //ID���� Conversation ����
        
        //�ʱ� �ε��� id�� �ʱ�ȭ
        int index = id;
        //�����δ� ù��° ���Ұ� ID 1 �̹Ƿ� id���� �ϳ� ���� ������ ����
        index--;
        string script = reader.GetContent(index);
        while (script != "" && reader.GetName(index) == name)
        {
            //ĳ���� �̸��� �޶��� �� ���� ����
            Debug.Log(script);
            index++;
            script = reader.GetContent(index);
        }

        //�б⹮�� �ʿ��ϸ� ���߿� ���� ����
    }
}
