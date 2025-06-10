using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemData
{
    public readonly string Item_ID;

    public readonly string Name;

    public readonly int Price;

    public readonly int PCMaxHP_UP;
    public readonly int FireDm_UP;

    public readonly int EnDm_Down;

    public readonly int EnDf_Down;

    public ItemData(Dictionary<string, object> data)
    {
        Item_ID = data["Item_ID"].ToString();

        Name = data["Name"].ToString();

        Price = (int)data["Price"];

        PCMaxHP_UP = (int)data["PCMaxHP_UP"];
        FireDm_UP  = (int)data["FireDm_UP"];

        EnDm_Down  = (int)data["EnDm_Down"];

        EnDf_Down  = (int)data["EnDf_Down"];

    }
}


    public class ItemDataBase 
{
    Dictionary<string, ItemData> ItemDatas = new Dictionary<string, ItemData>();
   
    public ItemDataBase(TextAsset ItemDataTable)
    {

        int CardDataIndex = CSVReader.Read(ItemDataTable).Count;
        

        Debug.Log("¼öÄ¡ :" + CardDataIndex);

        for (int i = 0; i < CardDataIndex; i++)
        {
            string key = CSVReader.Read(ItemDataTable)[i]["Item_ID"].ToString();

            ItemData data = new ItemData(CSVReader.Read(ItemDataTable)[i]);


            ItemDatas.Add(key, data);

            
        }


        
    }


    public bool SearchData(string cardCode, out ItemData get_cardData)
    {
        bool isData = false;

        get_cardData = new ItemData();

        if (ItemDatas.ContainsKey(cardCode))
        {
            isData = true;
            get_cardData = ItemDatas[cardCode];
        }


        return isData;
    }


    public bool SearchData(string CardCode)
    {
        bool isData = false;
        if (ItemDatas.ContainsKey(CardCode)) isData = true;
        return isData;
    }

}
