using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


//Card_ID	Card_Rank	Card_Num	Card_Level	Card_Name_KR	Card_Name_EN	Card_Drag	Target_Type	Attack_Count	
//Attack_DMG	Barrier_Get	HP_Recover	Buff_VolumeUp	Buff_Buzz	Buff_Mute	Buff_BurnUp	Buff_BurnOut	Buff_Confusion	Char_SkillPoint_Get	SpecialAbility_Desc	Move_Type	Ani_Code	
//Effect_Code	Sound_Code	Card_Im	Card_Des	Buff_Ex1	Buff_Ex2	Card_Ex														
[System.Serializable]
public struct CardData
{
    public string Card_ID;
    public int Card_Rank;
    public int Card_Level;
    public string Card_Name_KR;
    public string Card_Name_EN;
    public string Card_Drag;
    public string Target_Type;
    public string Attack_Count;
    public int Attack_DMG;
    public int Barrier_Get;
    public int HP_Recover;
    public int Buff_VolumeUp;
    public int Buff_Buzz;
    public int Buff_Mute;
    public int Buff_BurnUp;
    public int Buff_BurnOut;
    public int Buff_Confusion;
    public int Char_SkillPoint_Get;
    public string SpecialAbility_Desc;
    public string Move_Type;
    public string Ani_Code;
    public string Effect_Code;
    public string Sound_Code;
    public string Card_Im;
    public string Card_Des;
    string DefaultCard_Des;


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
        Card_Des = null;
        DefaultCard_Des = data["Card_Des"].ToString();

        Buff_Ex1 = data["Buff_Ex1"].ToString();

        Buff_Ex2 = data["Buff_Ex2"].ToString();
        Card_Ex = data["Card_Ex"].ToString();

        CardBuff = null;

        CardBuff = GetBuff();


        Card_Des = CardDescDamageReplace(Attack_DMG.ToString());


        Card_Des = CardDescRcoverReplace(HP_Recover.ToString());
    }



    // 1: fire , 2: Eletric , 3: Captivate , 4: Curse

    Buff GetBuff()
    {

        if (Buff_Buzz != 0) // 공격력 20% 다운
        {
            return new AttackDamageDownBuff(BuffType.Start, Buff_Buzz, 25);
        }

        if (Buff_Mute != 0) // 공격력 100% 다운
        {
            return new AttackDamageDownBuff_Mute(BuffType.Start, Buff_Mute, 100);
        }

        if (Buff_BurnUp != 0) // 화상 도트 2 데미지
        {
            return new FireBuff(BuffType.Start, Buff_BurnUp, 4);
        }

        if (Buff_BurnOut != 0)// 화상 도트 12 데미지
        {
            return new FireBuffBrunOut(BuffType.Start, Buff_BurnOut, 12);
        }

        if (Buff_Confusion != 0) // 리듬게임 반대로
        {
            return new RhythmDebuff(BuffType.End, Buff_Confusion);
        }


        return null;
    }



    public string CardDescDamageReplace(string currentDamage)
    {
        string result = DefaultCard_Des.Replace("@", currentDamage);

        result = result.Replace("\"", ""); // 모든 따옴표 제거
       
        return result;
    }


    public string CardDescRcoverReplace(string currentRecover)
    {
        string result = Card_Des.Replace("$", currentRecover);

        return result;
    }


    public CardData Clone()
    {
        CardData copy = this; // 값 타입/문자열은 그대로 복사해도 OK
        if (CardBuff != null)
            copy.CardBuff = CardBuff.Clone(); // Buff는 깊은 복사
        return copy;
    }

}



public struct CardStatusData
{
    public CardStatusData(Dictionary<string, object> data)
    {
        Status_Code = data["Status_Code"].ToString();
        Status_Dm = data["Status_Dm"].ToString();

        Ex = data["Ex"].ToString();
    }

    public readonly string Status_Code;
    public readonly string Status_Dm;

    public readonly string Ex;
}


public class CardDataBase
{


    Dictionary<string, CardData> CommonCardDatas = new Dictionary<string, CardData>();

    TextAsset CardDataTableTextData;
    TextAsset CardStatusDataTableTextData;


    public CardDataBase(TextAsset CardStatusDataTable, TextAsset CardDataTable)
    {

        CardDataTableTextData = CardDataTable;
        CardStatusDataTableTextData = CardStatusDataTable;

        ResetTable();
    }

    public void ResetTable()
    {
        int CardDataIndex = CSVReader.Read(CardDataTableTextData).Count;

        List<Dictionary<string, object>> csvData = CSVReader.Read(CardDataTableTextData);

        for (int i = 0; i < CardDataIndex; i++)
        {
            string key = csvData[i]["Card_ID"].ToString();

            CardData data = new CardData(csvData[i]);

            CommonCardDatas[key] = data;
        }
    }


    public void AddValueDamage(int amount, List<Card> ReflashCard)
    {
        List<string> keys = CommonCardDatas.Keys.ToList();

        int volumeUpItem = 0;

        if (GameManager.instance.ItemDataLoader.stringData.Buff_Type == "Buff_Strength")
        {
            volumeUpItem = GameManager.instance.ItemDataLoader.stringData.Buff_Value_Gain;
        }


        for (int i = 0; i < keys.Count; i++)
        {
            CardData card = CommonCardDatas[keys[i]];

            if (card.Attack_DMG > 0)
            {
                card.Attack_DMG = card.Attack_DMG + amount + volumeUpItem;
                card.Card_Des = card.CardDescDamageReplace(card.Attack_DMG.ToString());
                Debug.Log(card.Card_Des);
                Debug.Log(card.Attack_DMG);

                CommonCardDatas[keys[i]] = card;
            }
        }

        for (int i = 0; i < ReflashCard.Count; i++)
        {
            ReflashCard[i].ReflashCardData();
        }
    }

    public void PlayerBuzz(float percent, List<Card> ReflashCard)
    {
        List<string> keys = CommonCardDatas.Keys.ToList();




        for (int i = 0; i < keys.Count; i++)
        {
            CardData card = CommonCardDatas[keys[i]];

            if (card.Attack_DMG > 0)
            {
                card.Attack_DMG = Mathf.CeilToInt( (float)card.Attack_DMG * percent);
                card.Card_Des = card.CardDescDamageReplace(card.Attack_DMG.ToString());
               

                CommonCardDatas[keys[i]] = card;
            }
        }

        for (int i = 0; i < ReflashCard.Count; i++)
        {
            ReflashCard[i].ReflashCardData();
        }
    }

    public void LossValueDamage(int amount, List<Card> ReflashCard)
    {
        List<string> keys = CommonCardDatas.Keys.ToList();




        for (int i = 0; i < keys.Count; i++)
        {
            CardData card = CommonCardDatas[keys[i]];

            if (card.Attack_DMG > 0)
            {
                card.Attack_DMG = card.Attack_DMG + amount;
                card.Card_Des = card.CardDescDamageReplace(card.Attack_DMG.ToString());
               

                CommonCardDatas[keys[i]] = card;
            }
        }

        for (int i = 0; i < ReflashCard.Count; i++)
        {
            ReflashCard[i].ReflashCardData();
        }
    }


    public void LossValueRecoverHP(int amount, List<Card> ReflashCard)
    {
        List<string> keys = CommonCardDatas.Keys.ToList();

        int volumeUpItem = 0;


        for (int i = 0; i < keys.Count; i++)
        {
            CardData card = CommonCardDatas[keys[i]];

            if (card.HP_Recover > 0)
            {
                card.HP_Recover = card.HP_Recover + amount;
                //card.Card_Des = card.CardDescDamageReplace(card.Attack_DMG.ToString());

                CommonCardDatas[keys[i]] = card;
            }
        }

        for (int i = 0; i < ReflashCard.Count; i++)
        {
            ReflashCard[i].ReflashCardData();
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



        return isData;
    }


    public bool SearchData(string CardCode)
    {
        bool isData = false;

        if (CommonCardDatas.ContainsKey(CardCode)) isData = true;


        return isData;
    }

    public string RandomCard()
    {
        List<string> keys = new List<string>(CommonCardDatas.Keys);

        string code = keys[Random.Range(0, keys.Count)];

        if (code == "SKILL" || code == "C2102" || code == "C2101" || code == "C3031" || code == "C3032") code = "C1031";

        return code;


    }

}
