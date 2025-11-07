using UnityEngine;
using System.Collections.Generic;
public struct StickerItemData
{

    public readonly string ItemCode;
    public readonly string ItemNameKR;
    public readonly string Card_Bring;
    public readonly int CardCount; // 채워야하는 게이지의 양
    public readonly string ItemDes;
    public readonly string ItemImage;


    public StickerItemData(Dictionary<string, object> data)
    {
        ItemCode = data["ItemCode"].ToString();
        ItemNameKR = data["ItemNameKR"].ToString();
        Card_Bring = data["Card_Bring"].ToString();
        CardCount = (int)data["CardCount"];
        ItemDes = data["ItemDes"].ToString();
        ItemImage = data["ItemImage"].ToString();
    }

    public StickerItemData(int num)
    {
        ItemCode = "0";
        ItemNameKR = "0";
        Card_Bring = "0";
        CardCount = 0;
        ItemDes = "0";
        ItemImage = "0";
    }
}

public struct StrapItemData
{
    public readonly string ItemCode;
    public readonly string ItemNameKR;
    public readonly int PC_Mana;
    public readonly int PC_HP;
    public readonly int Shop_Sale;
    public readonly int Card_Damage;
    public readonly int Card_HP_Recover;
    public readonly int Reroll_Cost;
    public readonly string ItemDes;
    public readonly string ItemImage;



    public StrapItemData(Dictionary<string, object> data)
    {
        ItemCode = data["ItemCode"].ToString();
        ItemNameKR = data["ItemNameKR"].ToString();
        PC_Mana = (int)data["PC_Mana"];
        PC_HP = (int)data["PC_HP"];
        Shop_Sale = (int)data["Shop_Sale"];
        Card_Damage = (int)data["Card_Damage"];
        Card_HP_Recover = (int)data["Card_HP_Recover"];
        Reroll_Cost = (int)data["Reroll_Cost"];
        ItemDes = data["ItemDes"].ToString();
        ItemImage = data["ItemImage"].ToString();
    }

    public StrapItemData(int data)
    {
        ItemCode = "0";
        ItemNameKR = "0";
        PC_Mana = 0;
        PC_HP = 0;
        Shop_Sale = 0;
        Card_Damage = 0;
        Card_HP_Recover = 0;
        Reroll_Cost = 0;
        ItemDes = "0";
        ItemImage = "0";
    }
}

public struct StringItemData
{
    public readonly string ItemCode;
    public readonly string ItemNameKR;
    public readonly string Buff_Type;
    public readonly int    Buff_Value_Gain;
    public readonly string ItemDes;
    public readonly string ItemImage;

    public StringItemData(Dictionary<string, object> data)
    {
        ItemCode = data["ItemCode"].ToString();
        ItemNameKR = data["ItemNameKR"].ToString();
        Buff_Type = data["Buff_Type"].ToString();
        Buff_Value_Gain = (int)data["Buff_Value_Gain"];
        ItemDes = data["ItemDes"].ToString();
        ItemImage = data["ItemImage"].ToString();
    }


    public StringItemData(int data)
    {
        ItemCode = "0";
        ItemNameKR = "0";
        Buff_Type = "0";
        Buff_Value_Gain = 0;
        ItemDes = "0";
        ItemImage = "0";
    }
}

public class ItemDataBase 
{
    Dictionary<string, StickerItemData> StickerItemDatas = new Dictionary<string, StickerItemData>();
    Dictionary<string, StrapItemData> StrapItemDatas = new Dictionary<string, StrapItemData>();
    Dictionary<string, StringItemData> StringItemDatas = new Dictionary<string, StringItemData>();

    public ItemDataBase(TextAsset StickerItemDataTable , TextAsset StrapItemDataTable, TextAsset StringItemDataTable)
    {
       
        for (int i = 0; i < CSVReader.Read(StickerItemDataTable).Count; i++)
        {
            string key = CSVReader.Read(StickerItemDataTable)[i]["ItemCode"].ToString();

            StickerItemData data = new StickerItemData(CSVReader.Read(StickerItemDataTable)[i]);


            StickerItemDatas.Add(key, data);
        }

        for (int i = 0; i < CSVReader.Read(StrapItemDataTable).Count; i++)
        {
            string key = CSVReader.Read(StrapItemDataTable)[i]["ItemCode"].ToString();

            StrapItemData data = new StrapItemData(CSVReader.Read(StrapItemDataTable)[i]);


            StrapItemDatas.Add(key, data);
        }

        for (int i = 0; i < CSVReader.Read(StringItemDataTable).Count; i++)
        {
            string key = CSVReader.Read(StringItemDataTable)[i]["ItemCode"].ToString();

            StringItemData data = new StringItemData(CSVReader.Read(StringItemDataTable)[i]);


            StringItemDatas.Add(key, data);
        }
    }


    public bool SearchData(string cardCode, out object get_cardData)
    {
        bool isData = false;

        get_cardData = new object();

        if (StickerItemDatas.ContainsKey(cardCode))
        {
            isData = true;
            get_cardData = StickerItemDatas[cardCode];
        }

        if (StrapItemDatas.ContainsKey(cardCode))
        {
            isData = true;
            get_cardData = StrapItemDatas[cardCode];
        }

        if (StringItemDatas.ContainsKey(cardCode))
        {
            isData = true;
            get_cardData = StringItemDatas[cardCode];
        }


        return isData;
    }


    public bool SearchData(string CardCode)
    {
        bool isData = false;
        if (StickerItemDatas.ContainsKey(CardCode)) isData = true;
        if (StrapItemDatas.ContainsKey(CardCode)) isData = true;
        if (StringItemDatas.ContainsKey(CardCode)) isData = true;

        return isData;
    }
}
