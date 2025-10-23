using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//Card_ID	Card_Rank	Card_Num	Card_Level	Card_Name_KR	Card_Name_EN	Card_Drag	Target_Type	Attack_Count	
//Attack_DMG	Barrier_Get	HP_Recover	Buff_VolumeUp	Buff_Buzz	Buff_Mute	Buff_BurnUp	Buff_BurnOut	Buff_Confusion	Char_SkillPoint_Get	SpecialAbility_Desc	Move_Type	Ani_Code	
//Effect_Code	Sound_Code	Card_Im	Card_Des	Buff_Ex1	Buff_Ex2	Card_Ex														
[System.Serializable]
public struct CardData
{
    public string Card_ID;
    public int    Card_Rank;
    public int    Card_Level;
    public string Card_Name_KR;
    public string Card_Name_EN;
    public string Card_Drag;
    public string Target_Type;
    public string Attack_Count;
    public int    Attack_DMG;
    public int    Barrier_Get;
    public int    HP_Recover;
    public int    Buff_VolumeUp;
    public int    Buff_Buzz;
    public int    Buff_Mute;
    public int    Buff_BurnUp;
    public int    Buff_BurnOut;
    public int    Buff_Confusion;
    public int    Char_SkillPoint_Get;
    public string SpecialAbility_Desc;
    public string Move_Type;
    public string Ani_Code;
    public string Effect_Code;
    public string Sound_Code;
    public string Card_Im;
    public string Card_Des;
    public string Buff_Ex1;

    public string Buff_Ex2;
    public string Card_Ex;


    public Buff CardBuff { get; private set; }

    public CardData(Dictionary<string, object> data)
    {
        Card_ID = data["Card_ID"].ToString();
        Card_Rank = (int)data["Card_Rank"];
        Card_Level = (int)data["Card_Level"];
        Card_Name_KR = data["Card_Name_KR"].ToString();
        Card_Name_EN = data["Card_Name_EN"].ToString();
        Card_Drag = data["Card_Drag"].ToString();
        Target_Type = data["Target_Type"].ToString();
        Attack_Count = data["Attack_Count"].ToString();
        Attack_DMG = (int)data["Attack_DMG"];
        Barrier_Get = (int)data["Barrier_Get"];
        HP_Recover = (int)data["HP_Recover"];
        Buff_VolumeUp = (int)data["Buff_VolumeUp"];
        Buff_Buzz = (int)data["Buff_Buzz"];
        Buff_Mute = (int)data["Buff_Mute"];
        Buff_BurnUp = (int)data["Buff_BurnUp"];
        Buff_BurnOut = (int)data["Buff_BurnOut"];
        Buff_Confusion = (int)data["Buff_Confusion"];
        Char_SkillPoint_Get = (int)data["Char_SkillPoint_Get"];
        SpecialAbility_Desc = data["SpecialAbility_Desc"].ToString();
        Move_Type = data["Move_Type"].ToString();
        Ani_Code = data["Ani_Code"].ToString();
        Effect_Code = data["Effect_Code"].ToString();
        Sound_Code = data["Sound_Code"].ToString();
        Card_Im = data["Card_Im"].ToString();
        Card_Des = data["Card_Des"].ToString();
        Buff_Ex1 = data["Buff_Ex1"].ToString();

        Buff_Ex2 = data["Buff_Ex2"].ToString();
        Card_Ex = data["Card_Ex"].ToString();

        CardBuff = null;
    }


   
    // 1: fire , 2: Eletric , 3: Captivate , 4: Curse
  

    string DefaultDescription(string textData)
    {
        string result = textData.Replace("@", Attack_DMG.ToString());

      

        return result;

    }

    public string CardDescDamageReplace(string currentDamage)
    {
        string result = "";// Card_CurrentDesc.Replace("@", currentDamage);

        return result;
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

    TextAsset CardDataTableTextData;
    TextAsset CardStatusDataTableTextData;


    public CardDataBase(TextAsset CardStatusDataTable , TextAsset CardDataTable)
    {

        CardDataTableTextData = CardDataTable;
        CardStatusDataTableTextData = CardStatusDataTable;

        ResetTable();
    }

    public void ResetTable()
    {
        int CardDataIndex = CSVReader.Read(CardDataTableTextData).Count;
        int CardStatusIndex = CSVReader.Read(CardStatusDataTableTextData).Count;

        for (int i = 0; i < CardDataIndex; i++)
        {
            string key = CSVReader.Read(CardDataTableTextData)[i]["Card_ID"].ToString();

            CardData data = new CardData(CSVReader.Read(CardDataTableTextData)[i]);

            CommonCardDatas.Add(key, data);
        }


        for (int i = 0; i < CardStatusIndex; i++)
        {
            string key = CSVReader.Read(CardStatusDataTableTextData)[i]["Status_Code"].ToString();
            CardStatusData data = new CardStatusData(CSVReader.Read(CardStatusDataTableTextData)[i]);
            CardStatusDatas.Add(key, data);
        }
    }


    public void AddValueDamage(int amount)
    {
        List<string> keys = CommonCardDatas.Keys.ToList();

        for (int i = 0; i < keys.Count; i++)
        {
            CardData card = CommonCardDatas[keys[i]];
            card.Attack_DMG = Mathf.Clamp(card.Attack_DMG + amount,0, 100);
            card.Card_Des = card.CardDescDamageReplace(card.Attack_DMG.ToString());
            CommonCardDatas[keys[i]] = card;
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

        if (code == "SKILL" || code == "C2102" || code == "C2101" || code == "C3031" || code == "C3032" ) code = "C1031";

        return code;


    }

}
