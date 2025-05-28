using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;



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
    CommonCardData [] CommonCard_Data;
    SpecialCardData[] SpecialCard_Data;
    TargetCardData [] TargetCard_Data;

    Dictionary<string, CardStatusData> CardStatusDatas;
    public CardDataBase(TextAsset CommonCardDataTable , TextAsset SpecialCardDataTable , TextAsset TargetCardDataTable , TextAsset CardStatusDataTable)
    { 
        CommonCard_Data  = new CommonCardData  [CSVReader.Read(CommonCardDataTable).Count];
        SpecialCard_Data = new SpecialCardData [CSVReader.Read(SpecialCardDataTable).Count];
        TargetCard_Data  = new TargetCardData  [CSVReader.Read(TargetCardDataTable).Count];

        int CardStatusIndex = CSVReader.Read(TargetCardDataTable).Count;

        for (int i = 0; i < CommonCard_Data.Length; i++)
        {
            CommonCard_Data[i] = new CommonCardData(CSVReader.Read(CommonCardDataTable)[i]);
        }

        for (int i = 0; i < SpecialCard_Data.Length; i++)
        {
            SpecialCard_Data[i] = new SpecialCardData(CSVReader.Read(SpecialCardDataTable)[i]);
        }

        for (int i = 0; i < TargetCard_Data.Length; i++)
        {
            TargetCard_Data[i] = new TargetCardData(CSVReader.Read(TargetCardDataTable)[i]);
        }

        for (int i = 0; i < CardStatusIndex; i++)
        {
            string key = CSVReader.Read(TargetCardDataTable)[i]["Status_Code"].ToString();
            CardStatusData data = new CardStatusData(CSVReader.Read(TargetCardDataTable)[i]);
            CardStatusDatas.Add(key, data);
        }
    }


    public bool SearchData(string cardCode, ref object get_cardData)
    {
        switch (get_cardData)
        {
            case CommonCardData commonCardData:

                for (int i = 0; i < CommonCard_Data.Length; i++)
                {
                    if (CommonCard_Data[i].Card_Code == cardCode)
                    {
                        get_cardData = CommonCard_Data[i];
                        return true;
                    }
                }

                
                break;

            case SpecialCardData specialCardData:
                for (int i = 0; i < SpecialCard_Data.Length; i++)
                {
                    if (SpecialCard_Data[i].Card_Code == cardCode)
                    {
                        get_cardData = SpecialCard_Data[i];
                        return true;
                    }
                }


                break;

            case TargetCardData targetCardData:
                for (int i = 0; i < TargetCard_Data.Length; i++)
                {
                    if (TargetCard_Data[i].Card_Code == cardCode)
                    {
                        get_cardData = TargetCard_Data[i];
                        return true;
                    }
                }


                break;
        }


        return false;
    }


    public bool SearchData(string CardCode)
    {
        bool isData = false;

        for (int i = 0; i < CommonCard_Data.Length; i++)
        {
            if (CommonCard_Data[i].Card_Code == CardCode)
            {

                isData = true;
            }
        }


        for (int i = 0; i < SpecialCard_Data.Length; i++)
        {
            if (SpecialCard_Data[i].Card_Code == CardCode)
            {

                isData = true;
            }
        }

        for (int i = 0; i < TargetCard_Data.Length; i++)
        {
            if (TargetCard_Data[i].Card_Code == CardCode)
            {

                isData = true;
            }
        }

        return isData;
    }

}
