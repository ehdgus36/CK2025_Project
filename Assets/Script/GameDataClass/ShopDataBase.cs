using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ShopData
{
    public readonly string Item_ID;


    public readonly string Type;
    public readonly string Rank;
    public readonly int Price;
   

    public ShopData(Dictionary<string, object> data)
    {
        Item_ID = data["Card_ID"].ToString();
        Type = data["Type"].ToString();
        Price = (int)data["Price"];
        Rank = data["Rank"].ToString();
    }
}

public class ShopDataBase 
{
    Dictionary<string, ShopData> ShopDatas = new Dictionary<string, ShopData>();
   
    public ShopDataBase(TextAsset ItemDataTable)
    {
        int CardDataIndex = CSVReader.Read(ItemDataTable).Count;
        List<Dictionary<string, object>> csvData = CSVReader.Read(ItemDataTable);


        for (int i = 0; i < CardDataIndex; i++)
        {
            string key = csvData[i]["Card_ID"].ToString();

            ShopData data = new ShopData(csvData[i]);


            ShopDatas.Add(key, data);         
        }
    }


    public bool SearchData(string cardCode, out ShopData get_cardData)
    {
        bool isData = false;

        get_cardData = new ShopData();

        if (ShopDatas.ContainsKey(cardCode))
        {
            isData = true;
            get_cardData = ShopDatas[cardCode];
        }


        return isData;
    }


    public bool SearchData(string CardCode)
    {
        bool isData = false;
        if (ShopDatas.ContainsKey(CardCode)) isData = true;
        return isData;
    }
}
