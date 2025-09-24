using System.Collections.Generic;
using System.Linq;
using UnityEngine;



//Card_ID	Card_Rank	Card_Level	MoveType	Card_Name_KR	
//Card_Name_EN	
//Cost_Type	Ability_Con1	Ability_Con2	Ability_Act1	Ability_Act2	
//Range_Type	Attack_Count	Damage	Status_Type	Status_Turn	Damage_Buff	HP_Recover	HP_Loss	Barrier_Get	
//Barrier_Loss	Ani_Code	Effect_Code	Effect_Pos	Sound_Code	Card_Des

[System.Serializable]
public struct CardData
{
    public  string Card_ID;
    public  int    Card_Rank;
    public  int    Card_Level;
    public  string MoveType;
    public  string Card_Name_KR;
    public  string Card_Name_EN;

    public  int    Cost_Type;
    public  string Ability_Type;
    public  string Ability_Con1;
    public  string Ability_Con2;
    public  string Ability_Act1;
    public  string Ability_Act2;




    public  int Range_Type;
    public  int Attack_Count;

    public  int Damage;

    public  int Status_Type;
    public  int Status_Turn;

    public  int Damage_Buff;
    public  int Recover_HP;

    public  int HP_Loss;

    public  int Barrier_Get;
    public  int Barrier_Loss;


    public  string Ani_Code;
   
    public  string Effect_Code;

    public  string Effect_Pos;
    public  string Sound_Code;

    public  string Card_Des;

    private string Card_CurrentDesc; // 플레이어에 버프나 상태에따라 유동적으로 변하는 카드 설명

    public  string Buff_Ex;
    public  string Buff_Ex2;

    public  string Card_Im;

    public Buff CardBuff { get; private set; }

    public CardData(Dictionary<string, object> data)
    {
        Card_ID  = data["Card_ID"].ToString();
        Card_Rank = (int)data["Card_Rank"];
        Card_Level = (int)data["Card_Level"];
        MoveType = data["MoveType"].ToString(); // M : 이동함 , P : 이동안함

        Card_Name_KR = data["Card_Name_KR"].ToString();
        Card_Name_EN = data["Card_Name_EN"].ToString();

        Cost_Type = (int)data["Cost_Type"];
        Ability_Type = data["Ability_Type"].ToString();
        Ability_Con1 = data["Ability_Con1"].ToString();
        Ability_Con2 = data["Ability_Con2"].ToString();
        Ability_Act1 = data["Ability_Act1"].ToString();
        Ability_Act2 = data["Ability_Act2"].ToString();


        Range_Type   = (int)data["Range_Type"];
        Attack_Count = (int)data["Attack_Count"];

        Damage      = (int)data["Damage"];
        
        Status_Type = (int)data["Status_Type"];
        Status_Turn = (int)data["Status_Turn"];

        Damage_Buff = (int)data["Damage_Buff"];
        Recover_HP  = (int)data["HP_Recover"];

        HP_Loss = (int)data["HP_Loss"];

        Barrier_Get = (int)data["Barrier_Get"];
        Barrier_Loss = (int)data["Barrier_Loss"];

        Ani_Code    = data["Ani_Code"].ToString();      
        Effect_Code = data["Effect_Code"].ToString();
        Effect_Pos = data["Effect_Pos"].ToString();
        Sound_Code = data["Sound_Code"].ToString();

        Card_Des = null;
        Card_CurrentDesc = data["Card_Des"].ToString();

        Buff_Ex = data["Buff_Ex"].ToString();
        Buff_Ex2 = data["Buff_Ex2"].ToString(); ;

        Card_Im = data["Card_Im"].ToString();

        CardBuff = null;
        
        CardBuff = GetBuff();
        Card_Des = DefaultDescription(Card_CurrentDesc);
    }


   
    // 1: fire , 2: Eletric , 3: Captivate , 4: Curse
    Buff GetBuff()
    { 
        Buff buff = null;

        switch (Status_Type)
        {
            case 3:
                buff = new FireBuff(BuffType.End, Status_Turn, 2);
                break;
            case 1:
                buff = new DefenseDebuff(BuffType.End, Status_Turn, 2);
                break;
            case 5:
                buff = new CaptivBuff(BuffType.Start, Status_Turn, 2);
                break;
            case 2:
                buff = new AttackDamageDownBuff(BuffType.Start, Status_Turn, 2);
                break;
        }


        return buff;
    }

    string DefaultDescription(string textData)
    {
        string result = textData.Replace("@", Damage.ToString());

      

        return result;

    }

    public string CardDescDamageReplace(string currentDamage)
    {
        string result = Card_CurrentDesc.Replace("@", currentDamage);

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
            card.Damage = Mathf.Clamp(card.Damage + amount,0, 100);
            card.Card_Des = card.CardDescDamageReplace(card.Damage.ToString());
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
