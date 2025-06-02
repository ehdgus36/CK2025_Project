using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;





public struct CardData
{
    public readonly string Card_ID;
    public readonly int    CardType;

    public readonly int Range_Type;
    public readonly int Attack_Count;

    public readonly int Damage;
    public readonly int Status_Type;
    public readonly int Status_Turn;

    public readonly int Damage_Buff;
    public readonly int Recover_HP;

    public readonly string Ani_Code;
    public readonly string Effect_Code;



    public CardData(Dictionary<string, object> data)
    {
        Card_ID = data["Card_ID"].ToString();
        CardType = (int)data["Card_ID"];

        Range_Type = (int)data["Card_ID"];
        Attack_Count = (int)data["Card_ID"];

        Damage = (int)data["Card_ID"];
        Status_Type = (int)data["Card_ID"];
        Status_Turn = (int)data["Card_ID"];

        Damage_Buff = (int)data["Card_ID"];
        Recover_HP = (int)data["Card_ID"];

        Ani_Code = data["Card_ID"].ToString();
        Effect_Code = data["Card_ID"].ToString();
    }



}

public struct CommonCardData
{
    public CommonCardData(Dictionary<string, object> data)
    {
        Card_Code     =       data["Card_Code"].ToString();
        Card_Name_EN  =       data["Card_Name_EN"].ToString();
        Card_Name_KR  =       data["Card_Name_KR"].ToString();
        Card_Level    =  (int)data["Card_Level"];
       
        Base_Damage_1 =  (int)data["Base_Damage_1"];
        Base_Damage_2 =  (int)data["Base_Damage_2"];

        Recover_HP    =  (int)data["Recover_HP"];

        Explain       = data["Explain"].ToString();
    }

    public readonly string  Card_Code;
    public readonly string  Card_Name_EN;
    public readonly string  Card_Name_KR;
    public readonly int     Card_Level;
  
    public readonly int     Base_Damage_1;
    public readonly int     Base_Damage_2;

    public readonly int     Recover_HP;

    public readonly string  Explain;
}

public struct SpecialCardData
{
    public SpecialCardData(Dictionary<string, object> data)
    {
        Card_Code    =      data["Card_Code"].ToString();
        Card_Name_EN =      data["Card_Name_EN"].ToString();
        Card_Name_KR =      data["Card_Name_KR"].ToString();
        Card_Level   = (int)data["Card_Level"];

        Status_Type  = (int)data["Status_Type"];
        Gain_Damage  = (int)data["Gain_Damage"];
        Status_Turn  = (int)data["Status_Turn"];

        Explain      =      data["Explain"].ToString();
       
    }

    public readonly string  Card_Code;
    public readonly string  Card_Name_EN;
    public readonly string  Card_Name_KR;
    public readonly int     Card_Level;

    public readonly int     Status_Type;
    public readonly int     Gain_Damage;
    public readonly int     Status_Turn;

    public readonly string  Explain;
   
}


public struct TargetCardData
{
    public TargetCardData(Dictionary<string, object> data)
    {
        Card_Code    = data["Card_Code"].ToString();
        Card_Name_EN = data["Card_Name_EN"].ToString();
        Card_Name_KR = data["Card_Name_KR"].ToString();
       
        Explain      = data["Explain"].ToString();
        
    }

    public readonly string Card_Code;
    public readonly string Card_Name_EN;
    public readonly string Card_Name_KR;
   
    public readonly string Explain;
   
}

public struct CardStatusData
{
    public CardStatusData(Dictionary<string, object> data)
    {
        Status_Code = data["Status_Code"].ToString();
        Status_Dm   = data["Status_Dm"].ToString();

        Ex = data["Ex"].ToString();
    }

    public readonly string Status_Code;
    public readonly string Status_Dm;

    public readonly string Ex;
}


public class CardDataBase 
{
    

    Dictionary<string, CardStatusData> CardStatusDatas = new Dictionary<string, CardStatusData>();
    Dictionary<string, CommonCardData> CommonCardDatas = new Dictionary<string, CommonCardData>();
    public CardDataBase( TextAsset CardStatusDataTable)
    { 
   
        int CardStatusIndex = CSVReader.Read(CardStatusDataTable).Count;

        for (int i = 0; i < CardStatusIndex; i++)
        {
            string key = CSVReader.Read(CardStatusDataTable)[i]["Status_Code"].ToString();
            CardStatusData data = new CardStatusData(CSVReader.Read(CardStatusDataTable)[i]);
            CardStatusDatas.Add(key, data);
        }
    }


    public bool SearchData(string cardCode, out object get_cardData)
    {
        bool isData = false;
        
        get_cardData = null;
        if (CommonCardDatas.Count == 0) return false;
        if (CardStatusDatas.Count == 0) return false;

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

}
