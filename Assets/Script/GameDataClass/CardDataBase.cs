using System.Collections.Generic;
using UnityEngine;





[System.Serializable]
public struct CardData
{
    public readonly string Card_ID;
    public readonly int    Card_Type;
    public readonly string MoveType;

    public readonly int Range_Type;
    public readonly int Attack_Count;

    public readonly int Damage;

    public readonly int Status_Type;
    public readonly int Status_Turn;

    public readonly int Damage_Buff;
    public readonly int Recover_HP;

    public readonly string Ani_Code;
    public readonly string Sound_Code;
    public readonly string Effect_Code;

    public readonly string Effect_Pos;

    public Buff CardBuff { get; private set; }

    public CardData(Dictionary<string, object> data)
    {
        Card_ID  = data["Card_ID"].ToString();
        Card_Type = (int)data["Card_Type"];
        MoveType = data["MoveType"].ToString(); // M : 이동함 , P : 이동안함

        Range_Type   = (int)data["Range_Type"];
        Attack_Count = (int)data["Attack_Count"];

        Damage      = (int)data["Damage"];
        Status_Type = (int)data["Status_Type"];


        Status_Turn = (int)data["Status_Turn"];

        Damage_Buff = (int)data["Damage_Buff"];
        Recover_HP  = (int)data["Recover_HP"];

        Ani_Code    = data["Ani_Code"].ToString();
        Sound_Code = data["Sound_Code"].ToString();
        Effect_Code = data["Effect_Code"].ToString();

        Effect_Pos = data["Effect_Pos"].ToString();

        CardBuff = null;
        CardBuff = GetBuff();
    }


   
    // 1: fire , 2: Eletric , 3: Captivate , 4: Curse
    Buff GetBuff()
    { 
        Buff buff = null;

        switch (Status_Type)
        {
            case 1:
                buff = new FireBuff(BuffType.End, Status_Turn, 2);
                break;
            case 2:
                buff = new ElecBuff(BuffType.End, Status_Turn, 2);
                break;
            case 3:
                buff = new CaptivBuff(BuffType.Start, Status_Turn, 2);
                break;
            case 4:
                buff = new CurseBuff(BuffType.Start, 3, 2);
                break;
        }


        return buff;
    }

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
    Dictionary<string, CardData> CommonCardDatas = new Dictionary<string, CardData>();
    public CardDataBase(TextAsset CardStatusDataTable , TextAsset CardDataTable)
    { 
   
        int CardDataIndex = CSVReader.Read(CardDataTable).Count;
        int CardStatusIndex = CSVReader.Read(CardStatusDataTable).Count;

       

        for (int i = 0; i < CardDataIndex; i++)
        {
            string key = CSVReader.Read(CardDataTable)[i]["Card_ID"].ToString();
            
            CardData data = new CardData(CSVReader.Read(CardDataTable)[i]);
           

            CommonCardDatas.Add(key, data);

            
        }


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
