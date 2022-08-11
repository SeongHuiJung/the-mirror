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
    List<string> isSelected; //������ ����
    
    public CSVReader()
    {
        StreamReader reader = new StreamReader(path);

        //�ν��Ͻ� ����
        names = new List<string>();
        contents = new List<string>();
        isSelected = new List<string>();

        string line = reader.ReadLine(); //�� ���� �н�
        line = reader.ReadLine();
        while (line != null)
        {
            string[] items = line.Split(",");
            names.Add(items[1]);
            contents.Add(items[2]);
            isSelected.Add(items[3]);
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

    public string GetSelected(int index)
    {
        if (CheckInvalidIndex(index))
            return "";
        return isSelected[index];
    }

    public bool IsLoaded()
    {
        return isLoaded;
    }


}