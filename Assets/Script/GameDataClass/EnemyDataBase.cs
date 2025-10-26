using System.Collections.Generic;
using UnityEngine;

public struct EnemyTableData
{

    //Enemy_ID		HP	Start_Barrier	SkillPoint	Damage	Skill1	Skill2
    public readonly string Enemy_ID;
    public readonly int HP;

    public readonly int Start_Barrier;

    public readonly int SkillPoint;

    public readonly int Damage;


    public readonly string Skill1;
    public readonly string Skill2;

    EnemyAIBehavior EnemyAI;

    public EnemyTableData(Dictionary<string, object> data)
    {
        Enemy_ID = data["Enemy_ID"].ToString();
        HP = (int)data["HP"];

        Start_Barrier = (int)data["Start_Barrier"];

        SkillPoint = (int)data["SkillPoint"];

        Damage = (int)data["Damage"];

        Skill1 = data["Enemy_ID"].ToString();
        Skill2 = data["Enemy_ID"].ToString();
        EnemyAI = null;
        EnemyAI = GetAI();
    }

    EnemyAIBehavior GetAI()
    {
        BaseAIState Skill1_State = null;
        BaseAIState Skill2_State = null;

        switch (Skill1)
        {
            case "DoubleAttack":
                Skill1_State = new EnemySkill_MultiAttack_State(2);
                break;

            case "TripleAttack":
                Skill1_State = new EnemySkill_MultiAttack_State(3);
                break;
            case "HPRecover_1":
                Skill1_State = new EnemySkill_AttackRecoverHP_State(1);
                break;
            case "HPRecover_2":
                Skill1_State = new EnemySkill_AttackRecoverHP_State(2);
                break;

            case "Curse":
                Skill1_State = new EnemySkill_RhythmReverse_State();
                break;

            case "HPHelp":
                Skill1_State = new EnemySkill_AllEnemyRecoverHP_State(1);
                break;

            case "BarbedArmourBuff":
                Skill1_State = new EnemySkill_BarbedArmor_State(1);
                break;

            case "DeckAttack":
                Skill1_State = new EnemySkill_DackAttack_State();
                break;
        }

        switch (Skill1)
        {
            case "DoubleAttack":
                Skill2_State = new EnemySkill_MultiAttack_State(2);
                break;

            case "TripleAttack":
                Skill2_State = new EnemySkill_MultiAttack_State(3);
                break;
            case "HPRecover_1":
                Skill2_State = new EnemySkill_AttackRecoverHP_State(1);
                break;
            case "HPRecover_2":
                Skill2_State = new EnemySkill_AttackRecoverHP_State(2);
                break;

            case "Curse":
                Skill2_State = new EnemySkill_RhythmReverse_State();
                break;

            case "HPHelp":
                Skill2_State = new EnemySkill_AllEnemyRecoverHP_State(1);
                break;

            case "BarbedArmourBuff":
                Skill2_State = new EnemySkill_BarbedArmor_State(1);
                break;

            case "DeckAttack":
                Skill2_State = new EnemySkill_DackAttack_State();
                break;
        }

        if (Skill1_State != null)
        {
            if (Skill2_State != null)
            {
                return new EnemyAI_CustomSkill2_Behavior(Skill1_State, Skill2_State);
            }
            else
            {
                return new EnemyAI_Custom_Behavior(Skill1_State);
            }
        }


        return null;
    }

}


public class EnemyDataBase
{
    Dictionary<string, EnemyTableData> EnemyDatas = new Dictionary<string, EnemyTableData>();



    public EnemyDataBase(TextAsset EnemyDataTable)
    {

        int EnemyDataIndex = CSVReader.Read(EnemyDataTable).Count;

        for (int i = 0; i < EnemyDataIndex; i++)
        {
            string key = CSVReader.Read(EnemyDataTable)[i]["Enemy_ID"].ToString();

            EnemyTableData data = new EnemyTableData(CSVReader.Read(EnemyDataTable)[i]);

            EnemyDatas.Add(key, data);
        }

    }


    public bool SearchData(string cardCode, out object get_cardData)
    {
        bool isData = false;

        get_cardData = null;


        if (EnemyDatas.ContainsKey(cardCode))
        {
            isData = true;
            get_cardData = EnemyDatas[cardCode];
        }

        return isData;
    }


    public bool SearchData(string CardCode)
    {
        bool isData = false;


        if (EnemyDatas.ContainsKey(CardCode)) isData = true;

        return isData;
    }


}
