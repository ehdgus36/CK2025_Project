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

    public EnemyAIBehavior EnemyAI;

    public EnemyTableData(Dictionary<string, object> data)
    {
        Enemy_ID = data["Enemy_ID"].ToString();
        HP = (int)data["HP"];

        Start_Barrier = (int)data["Start_Barrier"];

        SkillPoint = (int)data["SkillPoint"];

        Damage = (int)data["Damage"];

        Skill1 = data["Skill1"].ToString();
        Skill2 = data["Skill2"].ToString();
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

            case "DMG _Gold":
                Skill1_State = new EnemySkill_DMG_Gold_State(1, 3, 5); // 공격 횟수, 코인 횟수 , 데미지
                break;

            case "DMG _Gold_Boss":
                Skill1_State = new EnemySkill_DMG_Gold_State(1, 3, 5); // 공격 횟수, 코인 횟수 , 데미지
                break;

            case "Heal_HP":
                Skill1_State = new EnemySkill_AttackRecoverHP_State(1); // 공격횟수, 현재 자신의 체력의 20%회복
                break;

            case "Buff_Burnup":
                Skill1_State = new EnemySkill_Buff_Burnup_State(1, 2); // 공격 회수 ,턴수
                break;

            case "Buff_Burnout":
                Skill1_State = new EnemySkill_Buff_Burnout_State(1, 2);// 공격 회수 ,턴수
                break;

            case "HP_Volumeup":
                Skill1_State = new EnemySkill_HP_Volumeup_State(1, 0.2f, 5);// 공격 회수 , 소모할 체력 비례 0 ~1 , 볼륨업 중첩수
                break;

            case "Heal_Lowest":
                Skill1_State = new EnemySkill_Heal_Lowest_State(1, 5);// 공격 회수 , 데미지
                break;

            case "Poison_ATK":
                Skill1_State = new EnemySkill_PoisonAttack_State(1, 4);// 공격 회수 , 턴수
                break;

            case "Poison_ATK_Boss":
                Skill1_State = new EnemySkill_PoisonAttack_State(1, 4);// 공격 회수 , 턴수
                break;

            case "Double_Damage":
                Skill1_State = new EnemySkill_MultiAttack_State(2, 5); //공격 회수, 데미지
                break;

            case "Self_Volumeup":
                Skill1_State = new EnemySkill_Self_Volumeup_State(2, 2, 5); //공격 회수, 볼륨업 중첩 ,데미지
                break;

            case "Barrier_DMG":
                Skill1_State = new EnemySkill_BarrierAttack_State(5, 5, 5); //공격 회수, 베리어 , 데미지
                break;

            case "All_Volumeup":
                Skill1_State = new EnemySkill_JAZZBOSS_ALL_VolumeUp_State(1, 4, 5); //공격 회수 , 베리어 ,볼륨업된 데미지를 입력
                break;

            case "Confuse_PC":
                Skill1_State = new EnemySkill_RhythmReverse_State(2, 0); // 턴수 , 0이면 기본 공격력
                break;

            case "Heal_All":
                Skill1_State = new EnemySkill_AllEnemyRecoverHP_State(1); // 공격 횟수
                break;

            case "Spike_Enemy":
                Skill1_State = new EnemySkill_AllBarbedArmor_State(1); // 공격 횟수
                break;

            case "Confuse_Heal":
                Skill1_State = new EnemySkill_HpRecover_ReversRhythm_State(1); // 턴수
                break;

            case "Confuse_DMG":
                Skill1_State = new EnemySkill_RhythmReverse_State(2, 5); //턴수 데미지
                break;

            case "Self_Spike_Barrier":
                Skill1_State = new EnemySkill_BarbedArmor_Barrier_PlayerVolumeUp_State(1, 2, 2, 5); //공격 횟수 , 베리어 , 가시 턴수 , 볼륨업 중첩
                break;

        }


      
        switch (Skill2)
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

            case "DMG _Gold":
                Skill2_State = new EnemySkill_DMG_Gold_State(1, 3 ,5); // 공격 횟수, 코인 횟수 , 데미지
                break;

            case "DMG _Gold_Boss":
                Skill2_State = new EnemySkill_DMG_Gold_State(1, 3, 5); // 공격 횟수, 코인 횟수 , 데미지
                break;

            case "Heal_HP":
                Skill2_State = new EnemySkill_AttackRecoverHP_State(1); // 공격횟수, 현재 자신의 체력의 20%회복
                break;

            case "Buff_Burnup":
                Skill2_State = new EnemySkill_Buff_Burnup_State(1 , 2); // 공격 회수 ,턴수
                break;

            case "Buff_Burnout":
                Skill2_State = new EnemySkill_Buff_Burnout_State(1, 2);// 공격 회수 ,턴수
                break;

            case "HP_Volumeup":
                Skill2_State = new EnemySkill_HP_Volumeup_State(1, 0.2f, 5);// 공격 회수 , 소모할 체력 비례 0 ~1 , 볼륨업 중첩수
                break;

            case "Heal_Lowest":
                Skill2_State = new EnemySkill_Heal_Lowest_State(1 , 5);// 공격 회수 , 데미지
                break;

            case "Poison_ATK":
                Skill2_State = new EnemySkill_PoisonAttack_State(1 , 4);// 공격 회수 , 턴수
                break;

            case "Poison_ATK_Boss":
                Skill2_State = new EnemySkill_PoisonAttack_State(1, 4);// 공격 회수 , 턴수
                break;

            case "Double_Damage":
                Skill2_State = new EnemySkill_MultiAttack_State(2 , 5); //공격 회수, 데미지
                break;

            case "Self_Volumeup":
                Skill2_State = new EnemySkill_Self_Volumeup_State(2, 2 , 5); //공격 회수, 볼륨업 중첩 ,데미지
                break;

            case "Barrier_DMG":
                Skill2_State = new EnemySkill_BarrierAttack_State(5, 5, 5); //공격 회수, 베리어 , 데미지
                break;

            case "All_Volumeup":
                Skill2_State = new EnemySkill_JAZZBOSS_ALL_VolumeUp_State(1, 4, 5); //공격 회수 , 베리어 ,볼륨업된 데미지를 입력
                break;

            case "Confuse_PC":
                Skill2_State = new EnemySkill_RhythmReverse_State(2, 0); // 턴수 , 0이면 기본 공격력
                break;

            case "Heal_All":
                Skill2_State = new EnemySkill_AllEnemyRecoverHP_State(1); // 공격 횟수
                break;

            case "Spike_Enemy":
                Skill2_State = new EnemySkill_AllBarbedArmor_State(1); // 공격 횟수
                break;

            case "Confuse_Heal":
                Skill2_State = new EnemySkill_HpRecover_ReversRhythm_State(1); // 턴수
                break;

            case "Confuse_DMG":
                Skill2_State = new EnemySkill_RhythmReverse_State(2, 5); //턴수 데미지
                break;

            case "Self_Spike_Barrier":
                Skill2_State = new EnemySkill_BarbedArmor_Barrier_PlayerVolumeUp_State(1 , 2 , 2 , 5); //공격 횟수 , 베리어 , 가시 턴수 , 볼륨업 중첩
                break;
        }

        if (Skill1_State != null)
        {
            if (Skill2_State != null)
            {
                return new EnemyAI_CustomSkill2_Behavior(Skill1_State, Skill2_State);
            }
            
            
            return new EnemyAI_Custom_Behavior(Skill1_State);
            
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
        List<Dictionary<string, object>> csvData = CSVReader.Read(EnemyDataTable);


        for (int i = 0; i < EnemyDataIndex; i++)
        {
            string key = csvData[i]["Enemy_ID"].ToString();

            EnemyTableData data = new EnemyTableData(csvData[i]);

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

