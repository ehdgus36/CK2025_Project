using System.Collections.Generic;
using UnityEngine;





public class NoteDataBase 
{
    Dictionary<string, CardStatusData> CardStatusDatas = new Dictionary<string, CardStatusData>();
    Dictionary<string, CardData> CommonCardDatas = new Dictionary<string, CardData>();


    public NoteDataBase(TextAsset NoteDataTable, TextAsset NoteGroupDataTable)
    {

        int CardDataIndex = CSVReader.Read(NoteDataTable).Count;
        int CardStatusIndex = CSVReader.Read(NoteGroupDataTable).Count;



        for (int i = 0; i < CardDataIndex; i++)
        {
            string key = CSVReader.Read(NoteDataTable)[i]["Card_ID"].ToString();

            CardData data = new CardData(CSVReader.Read(NoteDataTable)[i]);


            CommonCardDatas.Add(key, data);


        }


        for (int i = 0; i < CardStatusIndex; i++)
        {
            string key = CSVReader.Read(NoteGroupDataTable)[i]["Status_Code"].ToString();
            CardStatusData data = new CardStatusData(CSVReader.Read(NoteGroupDataTable)[i]);
            CardStatusDatas.Add(key, data);
        }
    }


    public bool SearchData(string cardCode, out object get_cardData)
    {
        bool isData = false;

        get_cardData = null;

        if (CommonCardDatas.ContainsKey(cardCode))
        {
            isData = true;
            get_cardData = CommonCardDatas[cardCode];
        }


        if (CardStatusDatas.ContainsKey(cardCode))
        {
            isData = true;
            get_cardData = CardStatusDatas[cardCode];
        }

        return isData;
    }


    public bool SearchData(string CardCode)
    {
        bool isData = false;

        if (CommonCardDatas.ContainsKey(CardCode)) isData = true;
        if (CardStatusDatas.ContainsKey(CardCode)) isData = true;

        return isData;
    }

    public string RandomCard()
    {
        List<string> keys = new List<string>(CommonCardDatas.Keys);

        string code = keys[Random.Range(0, keys.Count)];

        if (code == "SKILL" || code == "C2102" || code == "C2101" || code == "C3031" || code == "C3032") code = "C1031";

        return code;


    }
}
