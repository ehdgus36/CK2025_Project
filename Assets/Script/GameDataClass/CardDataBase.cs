using System.Collections.Generic;
using UnityEngine;



public struct CommonCardData
{
    public CommonCardData(Dictionary<string, object> data)
    {
        Card_Code    =        data["Card_Code"].ToString();
        Card_Name_EN =        data["Card_Name_EN"].ToString();
        Card_Name_KR =        data["Card_Name_KR"].ToString();
        Card_Level   =   (int)data["Card_Level"];
        Grade_Point  =   (int)data["Grade_Point"];
        Damage_Count =   (int)data["Damage_Count"];
        Base_Damage_1=   (int)data["Base_Damage_1"];
        Base_Damage_2=   (int)data["Base_Damage_2"];
        Other_Effect =   (int)data["Other_Effect"];

        Ex_Explain  = data["Ex_Explain"].ToString();
        Explain     = data["Explain"].ToString();
        Sub_Explain = data["Sub_Explain"].ToString();
    }

    public readonly string Card_Code;
    public readonly string Card_Name_EN;
    public readonly string Card_Name_KR;
    public readonly int    Card_Level;
    public readonly int    Grade_Point;
    public readonly int    Damage_Count;
    public readonly int    Base_Damage_1;
    public readonly int    Base_Damage_2;
    public readonly int    Other_Effect;

    public readonly string Ex_Explain;
    public readonly string Explain;
    public readonly string Sub_Explain;


}

public struct SpecialCardData
{
    public SpecialCardData(Dictionary<string, object> data)
    {
        Card_Code = data["Card_Code"].ToString();
        Card_Name_EN = data["Card_Name_EN"].ToString();
        Card_Name_KR = data["Card_Name_KR"].ToString();
        Card_Level = (int)data["Card_Level"];
        Grade_Point = (int)data["Grade_Point"];
       

      
        Explain = data["Explain"].ToString();
        Sub_Explain = data["Sub_Explain"].ToString();
    }

    public readonly string Card_Code;
    public readonly string Card_Name_EN;
    public readonly string Card_Name_KR;
    public readonly int Card_Level;
    public readonly int Grade_Point;
    public readonly string Explain;
    public readonly string Sub_Explain;
}


public struct TargetCardData
{
    public TargetCardData(Dictionary<string, object> data)
    {
        Card_Code = data["Card_Code"].ToString();
        Card_Name_EN = data["Card_Name_EN"].ToString();
        Card_Name_KR = data["Card_Name_KR"].ToString();
        Card_Level = (int)data["Card_Level"];
       

       
        Explain = data["Explain"].ToString();
        Sub_Explain = data["Sub_Explain"].ToString();
    }

    public readonly string Card_Code;
    public readonly string Card_Name_EN;
    public readonly string Card_Name_KR;
    public readonly int Card_Level;
    public readonly string Explain;
    public readonly string Sub_Explain;
}


public class CardDataBase 
{
    CommonCardData[] CommonCard_Data;
    SpecialCardData[] SpecialCard_Data;
    TargetCardData[] TargetCard_Data;

    public CardDataBase(TextAsset CommonCardDataTable , TextAsset SpecialCardDataTable , TextAsset Target_CardDataTable)
    { 
        CommonCard_Data = new CommonCardData[CSVReader.Read(CommonCardDataTable).Count];
        SpecialCard_Data = new SpecialCardData[CSVReader.Read(SpecialCardDataTable).Count];
        TargetCard_Data = new TargetCardData[CSVReader.Read(Target_CardDataTable).Count];

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
            TargetCard_Data[i] = new TargetCardData(CSVReader.Read(Target_CardDataTable)[i]);
        }
    }


    public bool SearchData(string recipeCode, ref object get_recipeData)
    {
       

        switch (get_recipeData)
        {
            case CommonCardData commonCardData:

                for (int i = 0; i < CommonCard_Data.Length; i++)
                {
                    if (CommonCard_Data[i].Card_Code == recipeCode)
                    {
                        get_recipeData = CommonCard_Data[i];
                        return true;
                    }
                }

                
                break;

            case SpecialCardData specialCardData:
                for (int i = 0; i < CommonCard_Data.Length; i++)
                {
                    if (CommonCard_Data[i].Card_Code == recipeCode)
                    {
                        get_recipeData = CommonCard_Data[i];
                        return true;
                    }
                }


                break;

            case TargetCardData targetCardData:
                for (int i = 0; i < CommonCard_Data.Length; i++)
                {
                    if (CommonCard_Data[i].Card_Code == recipeCode)
                    {
                        get_recipeData = CommonCard_Data[i];
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
