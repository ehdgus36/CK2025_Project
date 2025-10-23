using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ShopData
{
    public readonly string Item_ID;

    public readonly int Price;
    public readonly string Rank;

    public ShopData(Dictionary<string, object> data)
    {
        Item_ID = data["Item_ID"].ToString();
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

        Debug.Log("¼öÄ¡ :" + CardDataIndex);

        for (int i = 0; i < CardDataIndex; i++)
        {
            string key = CSVReader.Read(ItemDataTable)[i]["Item_ID"].ToString();

            ShopData data = new ShopData(CSVReader.Read(ItemDataTable)[i]);


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
