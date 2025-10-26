using System.Collections.Generic;
using UnityEngine;

public struct NoteData
{
    public readonly string ID;
    public readonly string ChapterID;

    public readonly string NoteCode;

    public readonly int BeatCount;

    public NoteData(Dictionary<string, object> data)
    {
        ID = data["ID"].ToString();
        ChapterID = data["ChapterID"].ToString();

        NoteCode = data["NoteCode"].ToString();
        BeatCount = (int)data["BeatCount"];
    }
        
}

public struct NoteGroupData
{
    public readonly string GroupID;
    public readonly string ChapterID;
    public readonly string Note_ID1;
    public readonly string Note_ID2;
    public readonly string Note_ID3;

    public NoteGroupData(Dictionary<string, object> data)
    {
        GroupID = data["GroupID"].ToString();
        ChapterID = data["ChapterID"].ToString(); 
        Note_ID1 = data["Note_ID1"].ToString(); 
        Note_ID2 = data["Note_ID2"].ToString(); 
        Note_ID3 = data["Note_ID3"].ToString(); 
    }
}




public class NoteDataBase 
{
    Dictionary<string, NoteData> NoteDatas = new Dictionary<string, NoteData>();
    Dictionary<string, NoteGroupData> NoteGroupDatas = new Dictionary<string, NoteGroupData>();


    public NoteDataBase(TextAsset NoteDataTable, TextAsset NoteGroupDataTable)
    {

        int CardDataIndex = CSVReader.Read(NoteDataTable).Count;
        int CardStatusIndex = CSVReader.Read(NoteGroupDataTable).Count;


        List<Dictionary<string, object>> csvData = CSVReader.Read(NoteDataTable);

        for (int i = 0; i < CardDataIndex; i++)
        {
            string key = csvData[i]["ID"].ToString();

            NoteData data = new NoteData(csvData[i]);

            NoteDatas.Add(key, data);


        }

        csvData = CSVReader.Read(NoteGroupDataTable);
        for (int i = 0; i < CardStatusIndex; i++)
        {
            string key = csvData[i]["GroupID"].ToString();
            NoteGroupData data = new NoteGroupData(csvData[i]);
            NoteGroupDatas.Add(key, data);
        }
    }


    public bool SearchData(string cardCode, out object get_cardData)
    {
        bool isData = false;

        get_cardData = null;

        if (NoteGroupDatas.ContainsKey(cardCode))
        {
            isData = true;
            get_cardData = NoteGroupDatas[cardCode];
        }


        if (NoteDatas.ContainsKey(cardCode))
        {
            isData = true;
            get_cardData = NoteDatas[cardCode];
        }

        return isData;
    }


    public bool SearchData(string CardCode)
    {
        bool isData = false;

        if (NoteGroupDatas.ContainsKey(CardCode)) isData = true;
        if (NoteDatas.ContainsKey(CardCode)) isData = true;

        return isData;
    }

    
}
