using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVReader
{
    const string path = "Assets\\script.CSV";
    static bool isLoaded = false;  //파일 로딩 확인
    static int lines = 0;  //줄 수 기록
    static List<List<string>> dialogList = new List<List<string>>();

    static CSVReader()
    {
        StreamReader reader = new StreamReader(path);
        int itemCount = 8;  //csv 파일 항목 개수

        string line = reader.ReadLine(); //맨 윗줄 패스
        line = reader.ReadLine();
        while (line != null)
        {
            string[] items = line.Split("@");

            List<string> temp = new List<string>();
            for (int i = 0; i < itemCount; i++)
                temp.Add(items[i]);

            dialogList.Add(temp);

            line = reader.ReadLine();   //이거 없어서 무한반복 발생;;
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

    public bool IsLoaded()
    {
        return isLoaded;
    }

    public List<string> GetLine(int index)
    {
        if (CheckInvalidIndex(index))
            return null;
        return dialogList[index];
    }
}